﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpGL;

namespace ShadowMapping
{
    /// <summary>
    /// render a teapot with shadow.
    /// </summary>
    public partial class ShadowMappingNode
    {
        private const string blinnPhongVert = @"// Blinn-Phong-WorldSpace.vert
#version 150

in vec3 inPosition;
in vec3 inNormal;

// Declare an interface block.
out VS_OUT {
    vec3 position;
	vec3 normal;
    vec4 shadow_coord;
} vs_out;

uniform mat4 mvpMat;
uniform mat4 modelMat;
uniform mat4 normalMat; // transpose(inverse(modelMat));
uniform mat4 shadow_matrix;

void main() {
    gl_Position = mvpMat * vec4(inPosition, 1.0);
    vec4 worldPos = modelMat * vec4(inPosition, 1.0);
	vs_out.position = worldPos.xyz;
	vs_out.normal = (normalMat * vec4(inNormal, 0)).xyz;
    vs_out.shadow_coord = shadow_matrix * worldPos;
}
";
        private const string blinnPhongFrag = @"// Blinn-Phong-WorldSpace.frag
#version 150

struct Light {
    vec3 position;   // for directional light, meaningless.
    vec3 diffuse;
    vec3 specular;
    float constant;  // Attenuation.constant.
    float linear;    // Attenuation.linear.
    float quadratic; // Attenuation.quadratic.
	// direction from outer space to light source.
	vec3 direction;  // for point light, meaningless.
	// Note: We assume that spot light's angle ranges from 0 to 180 degrees.
	float cutOff;    // for spot light, cutOff. for others, meaningless.
};

struct Material {
    vec3 diffuse;
    vec3 specular;
    float shiness;
};

uniform Light light;
uniform int lightUpRoutine; // 0: point light; 1: directional light; 2: spot light.

uniform Material material;

uniform sampler2DShadow depth_texture;

uniform vec3 eyePos;

uniform bool blinn = true;

in VS_OUT {
    vec3 position;
	vec3 normal;
    vec4 shadow_coord;
} fs_in;

void PointLightUp(Light light, out float diffuse, out float specular) {
    vec3 Distance = light.position - fs_in.position;
	vec3 lightDir = normalize(Distance);
	vec3 normal = normalize(fs_in.normal); 
	float distance = length(Distance);
	float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * distance * distance);
	
	// Diffuse color
    diffuse = max(dot(lightDir, normal), 0) * attenuation;

	// Specular color
	vec3 eyeDir = normalize(eyePos - fs_in.position);
	float spec;
	if (blinn) {
	    vec3 halfwayDir = normalize(lightDir + eyeDir);
		spec = pow(max(dot(normal, halfwayDir), 0.0), material.shiness);
	}
	else {
	    vec3 reflectDir = reflect(-lightDir, normal);
		spec = pow(max(dot(eyeDir, reflectDir), 0.0), material.shiness);
	}
    specular = spec * attenuation;
}

void DirectionalLightUp(Light light, out float diffuse, out float specular) {
	vec3 lightDir = normalize(light.direction);
	vec3 normal = normalize(fs_in.normal); 
	
	// Diffuse color
    diffuse = max(dot(lightDir, normal), 0);

	// Specular color
	vec3 eyeDir = normalize(eyePos - fs_in.position);
	float spec;
	if (blinn) {
	    vec3 halfwayDir = normalize(lightDir + eyeDir);
		spec = pow(max(dot(normal, halfwayDir), 0.0), material.shiness);
	}
	else {
	    vec3 reflectDir = reflect(-lightDir, normal);
		spec = pow(max(dot(eyeDir, reflectDir), 0.0), material.shiness);
	}
    specular = spec;
}

// Note: We assume that spot light's angle ranges from 0 to 180 degrees.
void SpotLightUp(Light light, out float diffuse, out float specular) {
    vec3 Distance = light.position - fs_in.position;
	vec3 lightDir = normalize(Distance);
	vec3 centerDir = normalize(light.direction);
	float c = dot(lightDir, centerDir);// cut off at this point.
	if (c < 0 // current point is behind the spot light.
	    || 2 * c * c - 1 < light.cutOff) { // current point is outside of the cut off edge. 
		diffuse = 0; specular = 0;
	}
	else {
		vec3 normal = normalize(fs_in.normal); 
		float distance = length(Distance);
		float attenuation = 1.0 / (light.constant + light.linear * distance + light.quadratic * distance * distance);
	
		// Diffuse color
		diffuse = max(dot(lightDir, normal), 0) * attenuation;

		// Specular color
		vec3 eyeDir = normalize(eyePos - fs_in.position);
		float spec;
		if (blinn) {
			vec3 halfwayDir = normalize(lightDir + eyeDir);
			spec = pow(max(dot(normal, halfwayDir), 0.0), material.shiness);
		}
		else {
			vec3 reflectDir = reflect(-lightDir, normal);
			spec = pow(max(dot(eyeDir, reflectDir), 0.0), material.shiness);
		}
		specular = spec * attenuation;
	}
}


out vec4 fragColor;

void main() {
    float diffuse = 0;
    float specular = 0;
	if (lightUpRoutine == 0) { PointLightUp(light, diffuse, specular); }
	else if (lightUpRoutine == 1) { DirectionalLightUp(light, diffuse, specular); }
	else if (lightUpRoutine == 2) { SpotLightUp(light, diffuse, specular); }
    else { diffuse = 0; specular = 0; }
	float f = textureProj(depth_texture, fs_in.shadow_coord);
    //float f = 1;
	fragColor = vec4(f * diffuse * light.diffuse * material.diffuse + f * specular * light.specular * material.specular, 1.0);
}
";

        private const string ambientVert = @"#version 150

in vec3 inPosition;

uniform mat4 mvpMat;

void main() {
    gl_Position = mvpMat * vec4(inPosition, 1.0);
}
";

        private const string ambientFrag = @"#version 150

uniform vec3 ambientColor;

out vec4 fragColor;

void main() {
	fragColor = vec4(ambientColor, 1.0);
}
";

    }
}
