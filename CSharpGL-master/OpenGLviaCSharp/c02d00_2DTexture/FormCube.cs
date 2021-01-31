﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace c02d00_2DTexture
{
    public partial class FormCube : Form
    {
        private Scene scene;
        private ActionList actionList;
        private CubeNode cubeNode;

        public FormCube()
        {
            InitializeComponent();

            // init resources.
            this.Load += FormCube_Load;
            // render event.
            this.winGLCanvas1.OpenGLDraw += winGLCanvas1_OpenGLDraw;
            // resize event.
            this.winGLCanvas1.Resize += winGLCanvas1_Resize;
        }

        private void FormCube_Load(object sender, EventArgs e)
        {
            var position = new vec3(5, 3, 4) * 0.3f;
            var center = new vec3(0, 0, 0);
            var up = new vec3(0, 1, 0);
            var camera = new Camera(position, center, up, CameraType.Perspective, this.winGLCanvas1.Width, this.winGLCanvas1.Height);
            Texture texture = GetTexture();
            this.cubeNode = CubeNode.Create(texture);
            var scene = new Scene(camera);
            scene.RootNode = cubeNode;
            this.scene = scene;

            var list = new ActionList();
            var transformAction = new TransformAction(scene);
            list.Add(transformAction);
            var renderAction = new RenderAction(scene);
            list.Add(renderAction);
            this.actionList = list;

            //// uncomment these lines to enable manipualter of camera!
            //var manipulater = new FirstPerspectiveManipulater();
            //manipulater.BindingMouseButtons = System.Windows.Forms.MouseButtons.Right;
            //manipulater.Bind(camera, this.winGLCanvas1);
        }

        private Texture GetTexture()
        {
            string folder = System.Windows.Forms.Application.StartupPath;
            var bmp = new Bitmap(System.IO.Path.Combine(folder, @"cloth.png"));
            TexStorageBase storage = new TexImageBitmap(bmp, GL.GL_RGBA, 1, true);
            var texture = new Texture(storage,
                new TexParameterfv(TexParameter.PropertyName.TextureBorderColor, 1, 0, 0),
                new TexParameteri(TexParameter.PropertyName.TextureWrapS, (int)GL.GL_CLAMP_TO_BORDER),
                new TexParameteri(TexParameter.PropertyName.TextureWrapT, (int)GL.GL_CLAMP_TO_BORDER),
                new TexParameteri(TexParameter.PropertyName.TextureWrapR, (int)GL.GL_CLAMP_TO_BORDER),
                new TexParameteri(TexParameter.PropertyName.TextureMinFilter, (int)GL.GL_LINEAR),
                new TexParameteri(TexParameter.PropertyName.TextureMagFilter, (int)GL.GL_LINEAR));
            texture.Initialize();
            bmp.Dispose();

            return texture;
        }

        private SceneNodeBase GetRootNode()
        {
            const float distance = 1;
            Texture texture = GetTexture();
            var groupNode = new GroupNode();
            var diff = new vec3(0, 0, 0);
            A(distance, texture, groupNode, diff);
            diff = new vec3(4, 0, 0);
            ____(distance, texture, groupNode, diff);
            diff = new vec3(0, 3, 0);
            L(distance, texture, groupNode, diff);
            diff = new vec3(4, 3, 0);
            O(distance, texture, groupNode, diff);
            return groupNode;
        }

        private void O(float distance, Texture texture, GroupNode groupNode, vec3 diff)
        {
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(distance * 0.5f, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(-distance * 0.5f, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(distance * 0.5f, distance, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(-distance * 0.5f, distance, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
        }

        private void L(float distance, Texture texture, GroupNode groupNode, vec3 diff)
        {
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(distance, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(0, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(-distance, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(distance, distance, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
        }

        private void ____(float distance, Texture texture, GroupNode groupNode, vec3 diff)
        {
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(distance * 1.5f, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(distance * 0.5f, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(-distance * 0.5f, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(-distance * 1.5f, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
        }

        private void A(float distance, Texture texture, GroupNode groupNode, vec3 diff)
        {
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(distance, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(0, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(-distance, 0, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
            {
                cubeNode = CubeNode.Create(texture);
                cubeNode.WorldPosition = new vec3(0, distance, 0) + diff;
                groupNode.Children.Add(cubeNode);
            }
        }

        private void winGLCanvas1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            ActionList list = this.actionList;
            if (list != null)
            {
                vec4 clearColor = this.scene.ClearColor;
                GL.Instance.ClearColor(clearColor.x, clearColor.y, clearColor.z, clearColor.w);
                GL.Instance.Clear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

                Viewport viewport = Viewport.GetCurrent();
                list.Act(new ActionParams(viewport));
            }
        }

        void winGLCanvas1_Resize(object sender, EventArgs e)
        {
            this.scene.Camera.AspectRatio = ((float)this.winGLCanvas1.Width) / ((float)this.winGLCanvas1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.cubeNode.RotationAxis = new vec3(0, 1, 0);
            this.cubeNode.RotationAngle += 1.3f;
        }
    }
}
