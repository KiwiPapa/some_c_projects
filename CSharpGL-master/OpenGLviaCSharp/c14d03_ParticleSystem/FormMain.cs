﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace c14d03_ParticleSystem
{
    public partial class FormMain : Form
    {
        private Scene scene;
        private ActionList actionList;

        public FormMain()
        {
            InitializeComponent();

            this.winGLCanvas1.TimerTriggerInterval = 1 + (int)(1000.0f / 60.0f);
            this.Load += FormMain_Load;
            this.winGLCanvas1.OpenGLDraw += winGLCanvas1_OpenGLDraw;
            this.winGLCanvas1.Resize += winGLCanvas1_Resize;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var position = new vec3(5, 3, 4) * 2f;
            var center = new vec3(0, 0, 0);
            var up = new vec3(0, 1, 0);
            var camera = new Camera(position, center, up, CameraType.Perspective, this.winGLCanvas1.Width, this.winGLCanvas1.Height);

            this.scene = new Scene(camera);
            this.scene.ClearColor = Color.Black.ToVec4();
            {
                var light = new PointLight(new vec3(1, 1, 1) * 30);
                this.scene.Lights.Add(light);
            }
            {
                var group = new GroupNode();
                {
                    var node = ParticleNode.Create(6000);
                    group.Children.Add(node);
                }

                {
                    string folder = System.Windows.Forms.Application.StartupPath;
                    string filename = System.IO.Path.Combine(folder + @"\..\..\..\..\Infrastructure\CSharpGL.Models", "floor.obj_");
                    var parser = new ObjVNFParser(true);
                    ObjVNFResult result = parser.Parse(filename);
                    if (result.Error != null)
                    {
                        MessageBox.Show(result.Error.ToString());
                    }
                    else
                    {
                        ObjVNFMesh mesh = result.Mesh;
                        var model = new ObjVNF(mesh);
                        var node = NoShadowNode.Create(model, ObjVNF.strPosition, ObjVNF.strNormal, model.GetSize());
                        node.WorldPosition = new vec3(0, -3, 0);
                        node.Color = new vec3(0, 1, 0);
                        node.Name = filename;
                        group.Children.Add(node);
                    }
                }

                this.scene.RootNode = group;
            }

            {
                var list = new ActionList();
                var transformAction = new TransformAction(scene);
                list.Add(transformAction);
                var renderAction = new RenderAction(scene);
                list.Add(renderAction);
                var blinnPhongAction = new BlinnPhongAction(scene);
                list.Add(blinnPhongAction);
                this.actionList = list;
            }

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
            //IWorldSpace node = this.scene.RootElement;
            //if (node != null)
            //{
            //    node.RotationAngle += 1;
            //}
        }
    }

}
