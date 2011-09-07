namespace Example5
{
    partial class FormExample5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExample5));
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.sceneControl1 = new SharpGL.SceneControl();
            this.checkBoxSceneDesignMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 394);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(692, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(551, 442);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(153, 13);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Like this code? Buy me a beer!";
            // 
            // sceneControl1
            // 
            this.sceneControl1.AutoSelect = true;
            this.sceneControl1.DrawRenderTime = false;
            this.sceneControl1.FrameRate = 10F;
            this.sceneControl1.GDIEnabled = false;
            this.sceneControl1.Location = new System.Drawing.Point(12, 12);
            this.sceneControl1.Mouse = SharpGL.SceneGraph.MouseOperation.Translate;
            this.sceneControl1.Name = "sceneControl1";
            this.sceneControl1.SceneDesignMode = true;
            this.sceneControl1.ShowHandOnHover = true;
            this.sceneControl1.Size = new System.Drawing.Size(692, 379);
            this.sceneControl1.TabIndex = 5;
            // 
            // checkBoxSceneDesignMode
            // 
            this.checkBoxSceneDesignMode.AutoSize = true;
            this.checkBoxSceneDesignMode.Checked = true;
            this.checkBoxSceneDesignMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSceneDesignMode.Location = new System.Drawing.Point(15, 442);
            this.checkBoxSceneDesignMode.Name = "checkBoxSceneDesignMode";
            this.checkBoxSceneDesignMode.Size = new System.Drawing.Size(122, 17);
            this.checkBoxSceneDesignMode.TabIndex = 6;
            this.checkBoxSceneDesignMode.Text = "Scene Design mode";
            this.checkBoxSceneDesignMode.UseVisualStyleBackColor = true;
            this.checkBoxSceneDesignMode.CheckedChanged += new System.EventHandler(this.checkBoxSceneDesignMode_CheckedChanged);
            // 
            // FormExample5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 464);
            this.Controls.Add(this.checkBoxSceneDesignMode);
            this.Controls.Add(this.sceneControl1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(724, 498);
            this.MinimumSize = new System.Drawing.Size(724, 498);
            this.Name = "FormExample5";
            this.Text = "Example 5";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private SharpGL.SceneControl sceneControl1;
        private System.Windows.Forms.CheckBox checkBoxSceneDesignMode;

    }
}

