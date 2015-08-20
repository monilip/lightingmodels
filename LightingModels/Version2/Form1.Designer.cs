namespace Version2
{
    partial class Form1
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
            this.simpleOpenGlControl1 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.shadersPanel = new System.Windows.Forms.Panel();
            this.shaderLabel = new System.Windows.Forms.Label();
            this.shadersList = new System.Windows.Forms.ComboBox();
            this.lightsLabel = new System.Windows.Forms.Label();
            this.lightsList = new System.Windows.Forms.ComboBox();
            this.objectsLabel = new System.Windows.Forms.Label();
            this.objectsList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // simpleOpenGlControl1
            // 
            this.simpleOpenGlControl1.AccumBits = ((byte)(0));
            this.simpleOpenGlControl1.AutoCheckErrors = false;
            this.simpleOpenGlControl1.AutoFinish = false;
            this.simpleOpenGlControl1.AutoMakeCurrent = true;
            this.simpleOpenGlControl1.AutoSwapBuffers = true;
            this.simpleOpenGlControl1.BackColor = System.Drawing.Color.Black;
            this.simpleOpenGlControl1.ColorBits = ((byte)(32));
            this.simpleOpenGlControl1.DepthBits = ((byte)(16));
            this.simpleOpenGlControl1.Location = new System.Drawing.Point(12, 63);
            this.simpleOpenGlControl1.Name = "simpleOpenGlControl1";
            this.simpleOpenGlControl1.Size = new System.Drawing.Size(700, 400);
            this.simpleOpenGlControl1.StencilBits = ((byte)(0));
            this.simpleOpenGlControl1.TabIndex = 0;
            this.simpleOpenGlControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            this.simpleOpenGlControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.simpleOpenGlControl1_KeyDown);
            this.simpleOpenGlControl1.Resize += new System.EventHandler(this.OnResize);
            // 
            // shadersPanel
            // 
            this.shadersPanel.Location = new System.Drawing.Point(729, 100);
            this.shadersPanel.Name = "shadersPanel";
            this.shadersPanel.Size = new System.Drawing.Size(258, 258);
            this.shadersPanel.TabIndex = 32;
            // 
            // shaderLabel
            // 
            this.shaderLabel.AutoSize = true;
            this.shaderLabel.Location = new System.Drawing.Point(833, 47);
            this.shaderLabel.Name = "shaderLabel";
            this.shaderLabel.Size = new System.Drawing.Size(46, 13);
            this.shaderLabel.TabIndex = 31;
            this.shaderLabel.Text = "Shaders";
            // 
            // shadersList
            // 
            this.shadersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shadersList.FormattingEnabled = true;
            this.shadersList.Location = new System.Drawing.Point(729, 63);
            this.shadersList.Name = "shadersList";
            this.shadersList.Size = new System.Drawing.Size(258, 21);
            this.shadersList.TabIndex = 30;
            this.shadersList.SelectedIndexChanged += new System.EventHandler(this.shadersList_SelectedIndexChanged);
            // 
            // lightsLabel
            // 
            this.lightsLabel.AutoSize = true;
            this.lightsLabel.Location = new System.Drawing.Point(833, 7);
            this.lightsLabel.Name = "lightsLabel";
            this.lightsLabel.Size = new System.Drawing.Size(35, 13);
            this.lightsLabel.TabIndex = 34;
            this.lightsLabel.Text = "Lights";
            // 
            // lightsList
            // 
            this.lightsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lightsList.FormattingEnabled = true;
            this.lightsList.Location = new System.Drawing.Point(729, 23);
            this.lightsList.Name = "lightsList";
            this.lightsList.Size = new System.Drawing.Size(258, 21);
            this.lightsList.TabIndex = 33;
            this.lightsList.SelectedIndexChanged += new System.EventHandler(this.lightsList_SelectedIndexChanged);
            // 
            // objectsLabel
            // 
            this.objectsLabel.AutoSize = true;
            this.objectsLabel.Location = new System.Drawing.Point(110, 7);
            this.objectsLabel.Name = "objectsLabel";
            this.objectsLabel.Size = new System.Drawing.Size(43, 13);
            this.objectsLabel.TabIndex = 36;
            this.objectsLabel.Text = "Objects";
            // 
            // objectsList
            // 
            this.objectsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectsList.FormattingEnabled = true;
            this.objectsList.Location = new System.Drawing.Point(12, 23);
            this.objectsList.Name = "objectsList";
            this.objectsList.Size = new System.Drawing.Size(258, 21);
            this.objectsList.TabIndex = 35;
            this.objectsList.SelectedIndexChanged += new System.EventHandler(this.objectsList_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.objectsLabel);
            this.Controls.Add(this.objectsList);
            this.Controls.Add(this.lightsLabel);
            this.Controls.Add(this.lightsList);
            this.Controls.Add(this.shadersPanel);
            this.Controls.Add(this.shaderLabel);
            this.Controls.Add(this.shadersList);
            this.Controls.Add(this.simpleOpenGlControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl1;
        private System.Windows.Forms.Panel shadersPanel;
        private System.Windows.Forms.Label shaderLabel;
        private System.Windows.Forms.ComboBox shadersList;
        private System.Windows.Forms.Label lightsLabel;
        private System.Windows.Forms.ComboBox lightsList;
        private System.Windows.Forms.Label objectsLabel;
        private System.Windows.Forms.ComboBox objectsList;

    }
}

