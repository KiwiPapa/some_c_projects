﻿namespace ColorCodedPicking
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkRenderWireframe = new System.Windows.Forms.CheckBox();
            this.chkRenderBody = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.winGLCanvas1 = new CSharpGL.WinGLCanvas();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.rdoRotating = new System.Windows.Forms.RadioButton();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.winGLCanvas1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkRenderWireframe
            // 
            this.chkRenderWireframe.AutoSize = true;
            this.chkRenderWireframe.Checked = true;
            this.chkRenderWireframe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRenderWireframe.Font = new System.Drawing.Font("宋体", 12F);
            this.chkRenderWireframe.Location = new System.Drawing.Point(12, 12);
            this.chkRenderWireframe.Name = "chkRenderWireframe";
            this.chkRenderWireframe.Size = new System.Drawing.Size(155, 20);
            this.chkRenderWireframe.TabIndex = 1;
            this.chkRenderWireframe.Text = "Render Wireframe";
            this.chkRenderWireframe.UseVisualStyleBackColor = true;
            this.chkRenderWireframe.CheckedChanged += new System.EventHandler(this.chkRenderWireframe_CheckedChanged);
            // 
            // chkRenderBody
            // 
            this.chkRenderBody.AutoSize = true;
            this.chkRenderBody.Checked = true;
            this.chkRenderBody.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRenderBody.Font = new System.Drawing.Font("宋体", 12F);
            this.chkRenderBody.Location = new System.Drawing.Point(173, 12);
            this.chkRenderBody.Name = "chkRenderBody";
            this.chkRenderBody.Size = new System.Drawing.Size(115, 20);
            this.chkRenderBody.TabIndex = 2;
            this.chkRenderBody.Text = "Render Body";
            this.chkRenderBody.UseVisualStyleBackColor = true;
            this.chkRenderBody.CheckedChanged += new System.EventHandler(this.chkRenderBody_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 555);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(985, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // winGLCanvas1
            // 
            this.winGLCanvas1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.winGLCanvas1.Location = new System.Drawing.Point(12, 38);
            this.winGLCanvas1.Name = "winGLCanvas1";
            this.winGLCanvas1.RenderTrigger = CSharpGL.RenderTrigger.TimerBased;
            this.winGLCanvas1.Size = new System.Drawing.Size(961, 514);
            this.winGLCanvas1.TabIndex = 0;
            this.winGLCanvas1.TimerTriggerInterval = 40;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.radioButton1.Location = new System.Drawing.Point(378, 11);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(138, 20);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Picking&Draging";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.rdoPickingDraging_CheckedChanged);
            // 
            // rdoRotating
            // 
            this.rdoRotating.AutoSize = true;
            this.rdoRotating.Font = new System.Drawing.Font("宋体", 12F);
            this.rdoRotating.Location = new System.Drawing.Point(522, 11);
            this.rdoRotating.Name = "rdoRotating";
            this.rdoRotating.Size = new System.Drawing.Size(90, 20);
            this.rdoRotating.TabIndex = 4;
            this.rdoRotating.Text = "Rotating";
            this.rdoRotating.UseVisualStyleBackColor = true;
            this.rdoRotating.CheckedChanged += new System.EventHandler(this.rdoRotating_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 577);
            this.Controls.Add(this.rdoRotating);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chkRenderBody);
            this.Controls.Add(this.chkRenderWireframe);
            this.Controls.Add(this.winGLCanvas1);
            this.Name = "FormMain";
            this.Text = "Color Coded Picking - CSharpGL";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.winGLCanvas1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CSharpGL.WinGLCanvas winGLCanvas1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkRenderWireframe;
        private System.Windows.Forms.CheckBox chkRenderBody;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton rdoRotating;
    }
}