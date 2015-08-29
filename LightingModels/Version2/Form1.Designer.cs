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
            this.PositionLabel = new System.Windows.Forms.Label();
            this.xyzlabel1 = new System.Windows.Forms.Label();
            this.posX = new System.Windows.Forms.TextBox();
            this.posY = new System.Windows.Forms.TextBox();
            this.posZ = new System.Windows.Forms.TextBox();
            this.rotZ = new System.Windows.Forms.TextBox();
            this.rotY = new System.Windows.Forms.TextBox();
            this.rotX = new System.Windows.Forms.TextBox();
            this.xyzLabel2 = new System.Windows.Forms.Label();
            this.RotationLabel = new System.Windows.Forms.Label();
            this.scaleZ = new System.Windows.Forms.TextBox();
            this.scaleY = new System.Windows.Forms.TextBox();
            this.scaleX = new System.Windows.Forms.TextBox();
            this.xyzlabel3 = new System.Windows.Forms.Label();
            this.scaleLabel = new System.Windows.Forms.Label();
            this.renderScene = new System.Windows.Forms.Button();
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
            // PositionLabel
            // 
            this.PositionLabel.AutoSize = true;
            this.PositionLabel.Location = new System.Drawing.Point(312, 7);
            this.PositionLabel.Name = "PositionLabel";
            this.PositionLabel.Size = new System.Drawing.Size(44, 13);
            this.PositionLabel.TabIndex = 37;
            this.PositionLabel.Text = "Position";
            // 
            // xyzlabel1
            // 
            this.xyzlabel1.AutoSize = true;
            this.xyzlabel1.Location = new System.Drawing.Point(300, 46);
            this.xyzlabel1.Name = "xyzlabel1";
            this.xyzlabel1.Size = new System.Drawing.Size(82, 13);
            this.xyzlabel1.TabIndex = 38;
            this.xyzlabel1.Text = "x          y          z";
            // 
            // posX
            // 
            this.posX.Location = new System.Drawing.Point(292, 25);
            this.posX.Name = "posX";
            this.posX.Size = new System.Drawing.Size(29, 20);
            this.posX.TabIndex = 39;
            this.posX.TextChanged += new System.EventHandler(this.UpdateObjectPosition);
            // 
            // posY
            // 
            this.posY.Location = new System.Drawing.Point(327, 25);
            this.posY.Name = "posY";
            this.posY.Size = new System.Drawing.Size(29, 20);
            this.posY.TabIndex = 40;
            this.posY.TextChanged += new System.EventHandler(this.UpdateObjectPosition);
            // 
            // posZ
            // 
            this.posZ.Location = new System.Drawing.Point(362, 25);
            this.posZ.Name = "posZ";
            this.posZ.Size = new System.Drawing.Size(29, 20);
            this.posZ.TabIndex = 41;
            this.posZ.TextChanged += new System.EventHandler(this.UpdateObjectPosition);
            // 
            // rotZ
            // 
            this.rotZ.Location = new System.Drawing.Point(494, 25);
            this.rotZ.Name = "rotZ";
            this.rotZ.Size = new System.Drawing.Size(29, 20);
            this.rotZ.TabIndex = 46;
            this.rotZ.TextChanged += new System.EventHandler(this.UpdateObjectRotation);
            // 
            // rotY
            // 
            this.rotY.Location = new System.Drawing.Point(459, 25);
            this.rotY.Name = "rotY";
            this.rotY.Size = new System.Drawing.Size(29, 20);
            this.rotY.TabIndex = 45;
            this.rotY.TextChanged += new System.EventHandler(this.UpdateObjectRotation);
            // 
            // rotX
            // 
            this.rotX.Location = new System.Drawing.Point(424, 25);
            this.rotX.Name = "rotX";
            this.rotX.Size = new System.Drawing.Size(29, 20);
            this.rotX.TabIndex = 44;
            this.rotX.TextChanged += new System.EventHandler(this.UpdateObjectRotation);
            // 
            // xyzLabel2
            // 
            this.xyzLabel2.AutoSize = true;
            this.xyzLabel2.Location = new System.Drawing.Point(432, 46);
            this.xyzLabel2.Name = "xyzLabel2";
            this.xyzLabel2.Size = new System.Drawing.Size(82, 13);
            this.xyzLabel2.TabIndex = 43;
            this.xyzLabel2.Text = "x          y          z";
            // 
            // RotationLabel
            // 
            this.RotationLabel.AutoSize = true;
            this.RotationLabel.Location = new System.Drawing.Point(444, 7);
            this.RotationLabel.Name = "RotationLabel";
            this.RotationLabel.Size = new System.Drawing.Size(47, 13);
            this.RotationLabel.TabIndex = 42;
            this.RotationLabel.Text = "Rotation";
            // 
            // scaleZ
            // 
            this.scaleZ.Location = new System.Drawing.Point(627, 25);
            this.scaleZ.Name = "scaleZ";
            this.scaleZ.Size = new System.Drawing.Size(29, 20);
            this.scaleZ.TabIndex = 51;
            this.scaleZ.TextChanged += new System.EventHandler(this.UpdateObjectScale);
            // 
            // scaleY
            // 
            this.scaleY.Location = new System.Drawing.Point(592, 25);
            this.scaleY.Name = "scaleY";
            this.scaleY.Size = new System.Drawing.Size(29, 20);
            this.scaleY.TabIndex = 50;
            this.scaleY.TextChanged += new System.EventHandler(this.UpdateObjectScale);
            // 
            // scaleX
            // 
            this.scaleX.Location = new System.Drawing.Point(557, 25);
            this.scaleX.Name = "scaleX";
            this.scaleX.Size = new System.Drawing.Size(29, 20);
            this.scaleX.TabIndex = 49;
            this.scaleX.TextChanged += new System.EventHandler(this.UpdateObjectScale);
            // 
            // xyzlabel3
            // 
            this.xyzlabel3.AutoSize = true;
            this.xyzlabel3.Location = new System.Drawing.Point(564, 46);
            this.xyzlabel3.Name = "xyzlabel3";
            this.xyzlabel3.Size = new System.Drawing.Size(82, 13);
            this.xyzlabel3.TabIndex = 48;
            this.xyzlabel3.Text = "x          y          z";
            // 
            // scaleLabel
            // 
            this.scaleLabel.AutoSize = true;
            this.scaleLabel.Location = new System.Drawing.Point(577, 6);
            this.scaleLabel.Name = "scaleLabel";
            this.scaleLabel.Size = new System.Drawing.Size(34, 13);
            this.scaleLabel.TabIndex = 47;
            this.scaleLabel.Text = "Scale";
            // 
            // renderScene
            // 
            this.renderScene.Location = new System.Drawing.Point(12, 484);
            this.renderScene.Name = "renderScene";
            this.renderScene.Size = new System.Drawing.Size(75, 23);
            this.renderScene.TabIndex = 52;
            this.renderScene.Text = "Render scene";
            this.renderScene.UseVisualStyleBackColor = true;
            this.renderScene.Click += new System.EventHandler(this.renderScene_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 561);
            this.Controls.Add(this.renderScene);
            this.Controls.Add(this.scaleZ);
            this.Controls.Add(this.scaleY);
            this.Controls.Add(this.scaleX);
            this.Controls.Add(this.xyzlabel3);
            this.Controls.Add(this.scaleLabel);
            this.Controls.Add(this.rotZ);
            this.Controls.Add(this.rotY);
            this.Controls.Add(this.rotX);
            this.Controls.Add(this.xyzLabel2);
            this.Controls.Add(this.RotationLabel);
            this.Controls.Add(this.posZ);
            this.Controls.Add(this.posY);
            this.Controls.Add(this.posX);
            this.Controls.Add(this.xyzlabel1);
            this.Controls.Add(this.PositionLabel);
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
        private System.Windows.Forms.Label PositionLabel;
        private System.Windows.Forms.Label xyzlabel1;
        private System.Windows.Forms.TextBox posX;
        private System.Windows.Forms.TextBox posY;
        private System.Windows.Forms.TextBox posZ;
        private System.Windows.Forms.TextBox rotZ;
        private System.Windows.Forms.TextBox rotY;
        private System.Windows.Forms.TextBox rotX;
        private System.Windows.Forms.Label xyzLabel2;
        private System.Windows.Forms.Label RotationLabel;
        private System.Windows.Forms.TextBox scaleZ;
        private System.Windows.Forms.TextBox scaleY;
        private System.Windows.Forms.TextBox scaleX;
        private System.Windows.Forms.Label xyzlabel3;
        private System.Windows.Forms.Label scaleLabel;
        private System.Windows.Forms.Button renderScene;

    }
}

