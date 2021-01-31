﻿using CSharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace c02d02_MultipleTextures
{
    public partial class Form1 : Form
    {
        private Scene scene;
        private ActionList actionList;
        private CubeNode cubeNode;

        public Form1()
        {
            InitializeComponent();

            // init resources.
            this.Load += FormMain_Load;
            // render event.
            this.winGLCanvas1.OpenGLDraw += winGLCanvas1_OpenGLDraw;
            // resize event.
            this.winGLCanvas1.Resize += winGLCanvas1_Resize;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            var position = new vec3(5, 3, 4) * 0.3f;
            var center = new vec3(0, 0, 0);
            var up = new vec3(0, 1, 0);
            var camera = new Camera(position, center, up, CameraType.Perspective, this.winGLCanvas1.Width, this.winGLCanvas1.Height);
            Texture texture0, texture1, texture2;
            GetTextures(out texture0, out texture1, out texture2);
            texture0.TextureUnitIndex = 0;// glActiveTexture(GL_TEXTURE0 + 0); glBindTexture(GL_TEXTURE_2D, texture0.TextureId);
            texture1.TextureUnitIndex = 0;// glActiveTexture(GL_TEXTURE0 + 0); glBindTexture(GL_TEXTURE_1D, texture1.TextureId);
            texture2.TextureUnitIndex = 1;// glActiveTexture(GL_TEXTURE0 + 1); glBindTexture(GL_TEXTURE_2D, texture2.TextureId);
            this.cubeNode = CubeNode.Create(texture0, texture1, texture2);
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

        private void GetTextures(out Texture texture0, out Texture texture1, out Texture texture2)
        {
            {
                string folder = System.Windows.Forms.Application.StartupPath;
                var bmp = new Bitmap(System.IO.Path.Combine(folder, @"base.png"));
                TexStorageBase storage = new TexImageBitmap(bmp);
                var texture = new Texture(storage,
                    new TexParameteri(TexParameter.PropertyName.TextureWrapS, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureWrapT, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureWrapR, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureMinFilter, (int)GL.GL_LINEAR),
                    new TexParameteri(TexParameter.PropertyName.TextureMagFilter, (int)GL.GL_LINEAR));
                texture.Initialize();
                bmp.Dispose();
                texture0 = texture;
            }
            {
                string folder = System.Windows.Forms.Application.StartupPath;
                var bmp = new Bitmap(System.IO.Path.Combine(folder, "Rainbow.png"));
                TexStorageBase storage = new TexImage1D(GL.GL_RGBA, bmp.Width, GL.GL_BGRA, GL.GL_UNSIGNED_BYTE, new ImageDataProvider(bmp));
                var texture = new Texture(storage,
                    new TexParameteri(TexParameter.PropertyName.TextureWrapS, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureWrapT, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureWrapR, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureMinFilter, (int)GL.GL_LINEAR),
                    new TexParameteri(TexParameter.PropertyName.TextureMagFilter, (int)GL.GL_LINEAR));
                texture.Initialize();
                bmp.Dispose();

                texture1 = texture;
            }
            {
                string folder = System.Windows.Forms.Application.StartupPath;
                var bmp = new Bitmap(System.IO.Path.Combine(folder, @"fish.png"));
                bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
                TexStorageBase storage = new TexImageBitmap(bmp);
                var texture = new Texture(storage,
                    new TexParameteri(TexParameter.PropertyName.TextureWrapS, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureWrapT, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureWrapR, (int)GL.GL_CLAMP_TO_EDGE),
                    new TexParameteri(TexParameter.PropertyName.TextureMinFilter, (int)GL.GL_LINEAR),
                    new TexParameteri(TexParameter.PropertyName.TextureMagFilter, (int)GL.GL_LINEAR));
                texture.Initialize();
                bmp.Dispose();
                texture2 = texture;
            }
        }

        private Texture GetTexture()
        {
            string folder = System.Windows.Forms.Application.StartupPath;
            var bmp = new Bitmap(System.IO.Path.Combine(folder, @"base.png"));
            TexStorageBase storage = new TexImageBitmap(bmp);
            var texture = new Texture(storage,
                new TexParameteri(TexParameter.PropertyName.TextureWrapS, (int)GL.GL_CLAMP_TO_EDGE),
                new TexParameteri(TexParameter.PropertyName.TextureWrapT, (int)GL.GL_CLAMP_TO_EDGE),
                new TexParameteri(TexParameter.PropertyName.TextureWrapR, (int)GL.GL_CLAMP_TO_EDGE),
                new TexParameteri(TexParameter.PropertyName.TextureMinFilter, (int)GL.GL_LINEAR),
                new TexParameteri(TexParameter.PropertyName.TextureMagFilter, (int)GL.GL_LINEAR));
            texture.Initialize();
            bmp.Dispose();

            return texture;
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
