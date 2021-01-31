﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpotLight
{
    public partial class FormMain : Form
    {
        private Scene scene;
        private ActionList actionList;
        private SpotLightNode node;
        private LightPostionNode lightNode;

        public FormMain()
        {
            InitializeComponent();

            this.Load += FormMain_Load;
            this.winGLCanvas1.OpenGLDraw += winGLCanvas1_OpenGLDraw;
            this.winGLCanvas1.Resize += winGLCanvas1_Resize;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var position = new vec3(1, 0.6f, 1) * 14;
            var center = new vec3(0, 0, 0);
            var up = new vec3(0, 1, 0);
            var camera = new Camera(position, center, up, CameraType.Perspective, this.winGLCanvas1.Width, this.winGLCanvas1.Height);
            this.scene = new Scene(camera)
;

            string folder = System.Windows.Forms.Application.StartupPath;
            string objFilename = System.IO.Path.Combine(folder + @"\..\..\..\..\Infrastructure\CSharpGL.Models", "vnfHanoiTower.obj_");
            var parser = new ObjVNFParser(false);
            ObjVNFResult result = parser.Parse(objFilename);
            if (result.Error != null)
            {
                MessageBox.Show(result.Error.ToString());
            }
            else
            {
                double radian = 120.0 / 180.0 * Math.PI / 2.0;
                var light = new CSharpGL.SpotLight(new vec3(1, 1, 1), new vec3(), (float)Math.Cos(radian));
                var model = new ObjVNF(result.Mesh);
                this.node = SpotLightNode.Create(light, model, ObjVNF.strPosition, ObjVNF.strNormal, model.GetSize());
                float max = node.ModelSize.max();
                this.node.Scale *= 16.0f / max;
                this.lightNode = LightPostionNode.Create();
                lightNode.SetLight(light);
                lightNode.WorldPosition = new vec3(1, 1, 1) * 4;
                var groupNode = new GroupNode(node, lightNode);
                this.scene.RootNode = groupNode;
                (new FormPropertyGrid(groupNode)).Show();
            }

            var list = new ActionList();
            var transformAction = new TransformAction(scene);
            list.Add(transformAction);
            var renderAction = new RenderAction(scene);
            list.Add(renderAction);
            this.actionList = list;

            var manipulater = new FirstPerspectiveManipulater();
            manipulater.Bind(camera, this.winGLCanvas1);
        }

        private void winGLCanvas1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            ActionList list = this.actionList;
            if (list != null)
            {
                vec4 clearColor = this.scene.ClearColor;
                GL.Instance.ClearColor(clearColor.x, clearColor.y, clearColor.z, clearColor.w);
                GL.Instance.Clear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

                list.Act(new ActionParams(Viewport.GetCurrent()));
            }
        }

        void winGLCanvas1_Resize(object sender, EventArgs e)
        {
            this.scene.Camera.AspectRatio = ((float)this.winGLCanvas1.Width) / ((float)this.winGLCanvas1.Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IWorldSpace node = this.node;
            if (node != null)
            {
                node.RotationAngle += 1;
            }
        }

        private void chkRotate_CheckedChanged(object sender, EventArgs e)
        {
            this.timer1.Enabled = this.chkRotate.Checked;
        }

        private void lblColorDisply_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Color color = this.colorDialog1.Color;
                var node = this.node as SpotLightNode;
                if (node != null)
                {
                    node.MaterialColor = color.ToVec3();
                    this.lblColor.Text = string.Format("{0}", color);
                    this.lblColorDisply.BackColor = color;
                }
            }
        }

        private void chkRotateLight_CheckedChanged(object sender, EventArgs e)
        {
            var node = this.lightNode;
            if (node != null)
            {
                this.lightNode.AutoRotate = this.chkRotateLight.Checked;
            }
        }

    }
}
