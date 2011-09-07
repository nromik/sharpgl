namespace Example6
{
    partial class FormExample6
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExample6));
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawRenderTime = true;
            this.openGLControl1.FrameRate = 20F;
            this.openGLControl1.GDIEnabled = true;
            this.openGLControl1.Location = new System.Drawing.Point(8, 8);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.Size = new System.Drawing.Size(728, 440);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_OpenGLDraw);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 456);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(728, 48);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 510);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.openGLControl1);
            this.Name = "Form1";
            this.Text = "Example 6 - Radial Blur";
            this.ResumeLayout(false);

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Label label1;
    }
}

