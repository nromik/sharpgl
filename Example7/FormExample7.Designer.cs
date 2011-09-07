namespace Example7
{
    partial class FormExample7
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
            this.openGLControl1 = new SharpGL.OpenGLControl();
            this.buttonBurst = new System.Windows.Forms.Button();
            this.checkBoxGravity = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openGLControl1
            // 
            this.openGLControl1.DrawRenderTime = false;
            this.openGLControl1.FrameRate = 25F;
            this.openGLControl1.GDIEnabled = false;
            this.openGLControl1.Location = new System.Drawing.Point(8, 8);
            this.openGLControl1.Name = "openGLControl1";
            this.openGLControl1.Size = new System.Drawing.Size(392, 344);
            this.openGLControl1.TabIndex = 0;
            this.openGLControl1.Resize += new System.EventHandler(this.openGLControl1_Resize);
            this.openGLControl1.OpenGLDraw += new System.Windows.Forms.PaintEventHandler(this.openGLControl1_OpenGLDraw);
            // 
            // buttonBurst
            // 
            this.buttonBurst.Location = new System.Drawing.Point(8, 376);
            this.buttonBurst.Name = "buttonBurst";
            this.buttonBurst.Size = new System.Drawing.Size(75, 23);
            this.buttonBurst.TabIndex = 1;
            this.buttonBurst.Text = "&Burst!";
            this.buttonBurst.UseVisualStyleBackColor = true;
            this.buttonBurst.Click += new System.EventHandler(this.buttonBurst_Click);
            // 
            // checkBoxGravity
            // 
            this.checkBoxGravity.AutoSize = true;
            this.checkBoxGravity.Location = new System.Drawing.Point(96, 384);
            this.checkBoxGravity.Name = "checkBoxGravity";
            this.checkBoxGravity.Size = new System.Drawing.Size(59, 17);
            this.checkBoxGravity.TabIndex = 2;
            this.checkBoxGravity.Text = "Gravity";
            this.checkBoxGravity.UseVisualStyleBackColor = true;
            this.checkBoxGravity.CheckedChanged += new System.EventHandler(this.checkBoxGravity_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Based on Jeff Molofee\'s great tutorial from NeHe!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 438);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxGravity);
            this.Controls.Add(this.buttonBurst);
            this.Controls.Add(this.openGLControl1);
            this.Name = "Form1";
            this.Text = "Example 7 - Particle Systems";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGLControl1;
        private System.Windows.Forms.Button buttonBurst;
        private System.Windows.Forms.CheckBox checkBoxGravity;
        private System.Windows.Forms.Label label1;
    }
}

