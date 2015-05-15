namespace LightingModels
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
            this.glControlMain = new OpenTK.GLControl();
            this.camMoveLeft = new System.Windows.Forms.Button();
            this.camMoveRight = new System.Windows.Forms.Button();
            this.camMoveUp = new System.Windows.Forms.Button();
            this.camMoveDown = new System.Windows.Forms.Button();
            this.cameraLabel = new System.Windows.Forms.Label();
            this.ObjectLabel = new System.Windows.Forms.Label();
            this.objRotateDown = new System.Windows.Forms.Button();
            this.objRotateUp = new System.Windows.Forms.Button();
            this.objRotateRight = new System.Windows.Forms.Button();
            this.objRotateLeft = new System.Windows.Forms.Button();
            this.camMoveFurther = new System.Windows.Forms.Button();
            this.camMoveCloser = new System.Windows.Forms.Button();
            this.shadersList = new System.Windows.Forms.ComboBox();
            this.chooseShaderLabel = new System.Windows.Forms.Label();
            this.objectsList = new System.Windows.Forms.ComboBox();
            this.chooseObjectLabel = new System.Windows.Forms.Label();
            this.objMoveFurther = new System.Windows.Forms.Button();
            this.objMoveCloser = new System.Windows.Forms.Button();
            this.objMoveDown = new System.Windows.Forms.Button();
            this.objMoveUp = new System.Windows.Forms.Button();
            this.objMoveRight = new System.Windows.Forms.Button();
            this.objMoveLeft = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // glControlMain
            // 
            this.glControlMain.BackColor = System.Drawing.Color.Black;
            this.glControlMain.Location = new System.Drawing.Point(12, 12);
            this.glControlMain.Name = "glControlMain";
            this.glControlMain.Size = new System.Drawing.Size(468, 411);
            this.glControlMain.TabIndex = 0;
            this.glControlMain.VSync = false;
            this.glControlMain.Load += new System.EventHandler(this.OnLoad);
            this.glControlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
            // 
            // camMoveLeft
            // 
            this.camMoveLeft.Location = new System.Drawing.Point(486, 29);
            this.camMoveLeft.Name = "camMoveLeft";
            this.camMoveLeft.Size = new System.Drawing.Size(80, 23);
            this.camMoveLeft.TabIndex = 1;
            this.camMoveLeft.Text = "move Left";
            this.camMoveLeft.UseVisualStyleBackColor = true;
            this.camMoveLeft.Click += new System.EventHandler(this.camMoveLeft_Click);
            // 
            // camMoveRight
            // 
            this.camMoveRight.Location = new System.Drawing.Point(572, 29);
            this.camMoveRight.Name = "camMoveRight";
            this.camMoveRight.Size = new System.Drawing.Size(81, 23);
            this.camMoveRight.TabIndex = 2;
            this.camMoveRight.Text = "move Right";
            this.camMoveRight.UseVisualStyleBackColor = true;
            this.camMoveRight.Click += new System.EventHandler(this.camMoveRight_Click);
            // 
            // camMoveUp
            // 
            this.camMoveUp.Location = new System.Drawing.Point(486, 58);
            this.camMoveUp.Name = "camMoveUp";
            this.camMoveUp.Size = new System.Drawing.Size(80, 23);
            this.camMoveUp.TabIndex = 3;
            this.camMoveUp.Text = "move Up";
            this.camMoveUp.UseVisualStyleBackColor = true;
            this.camMoveUp.Click += new System.EventHandler(this.camMoveUp_Click);
            // 
            // camMoveDown
            // 
            this.camMoveDown.Location = new System.Drawing.Point(572, 58);
            this.camMoveDown.Name = "camMoveDown";
            this.camMoveDown.Size = new System.Drawing.Size(81, 23);
            this.camMoveDown.TabIndex = 4;
            this.camMoveDown.Text = "move Down";
            this.camMoveDown.UseVisualStyleBackColor = true;
            this.camMoveDown.Click += new System.EventHandler(this.camMoveDown_Click);
            // 
            // cameraLabel
            // 
            this.cameraLabel.AutoSize = true;
            this.cameraLabel.Location = new System.Drawing.Point(516, 9);
            this.cameraLabel.Name = "cameraLabel";
            this.cameraLabel.Size = new System.Drawing.Size(43, 13);
            this.cameraLabel.TabIndex = 5;
            this.cameraLabel.Text = "Camera";
            // 
            // ObjectLabel
            // 
            this.ObjectLabel.AutoSize = true;
            this.ObjectLabel.Location = new System.Drawing.Point(521, 116);
            this.ObjectLabel.Name = "ObjectLabel";
            this.ObjectLabel.Size = new System.Drawing.Size(38, 13);
            this.ObjectLabel.TabIndex = 10;
            this.ObjectLabel.Text = "Object";
            // 
            // objRotateDown
            // 
            this.objRotateDown.Location = new System.Drawing.Point(570, 252);
            this.objRotateDown.Name = "objRotateDown";
            this.objRotateDown.Size = new System.Drawing.Size(81, 23);
            this.objRotateDown.TabIndex = 9;
            this.objRotateDown.Text = "rotate Down";
            this.objRotateDown.UseVisualStyleBackColor = true;
            this.objRotateDown.Click += new System.EventHandler(this.objRotateDown_Click);
            // 
            // objRotateUp
            // 
            this.objRotateUp.Location = new System.Drawing.Point(483, 252);
            this.objRotateUp.Name = "objRotateUp";
            this.objRotateUp.Size = new System.Drawing.Size(81, 23);
            this.objRotateUp.TabIndex = 8;
            this.objRotateUp.Text = "rotate Up";
            this.objRotateUp.UseVisualStyleBackColor = true;
            this.objRotateUp.Click += new System.EventHandler(this.objRotateUp_Click);
            // 
            // objRotateRight
            // 
            this.objRotateRight.Location = new System.Drawing.Point(570, 223);
            this.objRotateRight.Name = "objRotateRight";
            this.objRotateRight.Size = new System.Drawing.Size(81, 23);
            this.objRotateRight.TabIndex = 7;
            this.objRotateRight.Text = "rotate Right";
            this.objRotateRight.UseVisualStyleBackColor = true;
            this.objRotateRight.Click += new System.EventHandler(this.objRotateRight_Click);
            // 
            // objRotateLeft
            // 
            this.objRotateLeft.BackColor = System.Drawing.SystemColors.ControlLight;
            this.objRotateLeft.Location = new System.Drawing.Point(483, 223);
            this.objRotateLeft.Name = "objRotateLeft";
            this.objRotateLeft.Size = new System.Drawing.Size(80, 23);
            this.objRotateLeft.TabIndex = 6;
            this.objRotateLeft.Text = "rotate Left";
            this.objRotateLeft.UseVisualStyleBackColor = false;
            this.objRotateLeft.Click += new System.EventHandler(this.objRotateLeft_Click);
            // 
            // camMoveFurther
            // 
            this.camMoveFurther.Location = new System.Drawing.Point(572, 87);
            this.camMoveFurther.Name = "camMoveFurther";
            this.camMoveFurther.Size = new System.Drawing.Size(81, 23);
            this.camMoveFurther.TabIndex = 12;
            this.camMoveFurther.Text = "move Further";
            this.camMoveFurther.UseVisualStyleBackColor = true;
            this.camMoveFurther.Click += new System.EventHandler(this.camMoveFurther_Click);
            // 
            // camMoveCloser
            // 
            this.camMoveCloser.Location = new System.Drawing.Point(485, 87);
            this.camMoveCloser.Name = "camMoveCloser";
            this.camMoveCloser.Size = new System.Drawing.Size(81, 23);
            this.camMoveCloser.TabIndex = 11;
            this.camMoveCloser.Text = "move Closer";
            this.camMoveCloser.UseVisualStyleBackColor = true;
            this.camMoveCloser.Click += new System.EventHandler(this.camMoveCloser_Click);
            // 
            // shadersList
            // 
            this.shadersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shadersList.FormattingEnabled = true;
            this.shadersList.Location = new System.Drawing.Point(485, 196);
            this.shadersList.Name = "shadersList";
            this.shadersList.Size = new System.Drawing.Size(168, 21);
            this.shadersList.TabIndex = 18;
            this.shadersList.SelectedIndexChanged += new System.EventHandler(this.shadersList_SelectedIndexChanged);
            // 
            // chooseShaderLabel
            // 
            this.chooseShaderLabel.AutoSize = true;
            this.chooseShaderLabel.Location = new System.Drawing.Point(485, 180);
            this.chooseShaderLabel.Name = "chooseShaderLabel";
            this.chooseShaderLabel.Size = new System.Drawing.Size(81, 13);
            this.chooseShaderLabel.TabIndex = 17;
            this.chooseShaderLabel.Text = "Choose shader:";
            // 
            // objectsList
            // 
            this.objectsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.objectsList.FormattingEnabled = true;
            this.objectsList.Location = new System.Drawing.Point(483, 156);
            this.objectsList.Name = "objectsList";
            this.objectsList.Size = new System.Drawing.Size(168, 21);
            this.objectsList.TabIndex = 20;
            this.objectsList.SelectedIndexChanged += new System.EventHandler(this.objectsList_SelectedIndexChanged);
            // 
            // chooseObjectLabel
            // 
            this.chooseObjectLabel.AutoSize = true;
            this.chooseObjectLabel.Location = new System.Drawing.Point(483, 140);
            this.chooseObjectLabel.Name = "chooseObjectLabel";
            this.chooseObjectLabel.Size = new System.Drawing.Size(78, 13);
            this.chooseObjectLabel.TabIndex = 19;
            this.chooseObjectLabel.Text = "Choose object:";
            // 
            // objMoveFurther
            // 
            this.objMoveFurther.Location = new System.Drawing.Point(570, 340);
            this.objMoveFurther.Name = "objMoveFurther";
            this.objMoveFurther.Size = new System.Drawing.Size(81, 23);
            this.objMoveFurther.TabIndex = 26;
            this.objMoveFurther.Text = "move Further";
            this.objMoveFurther.UseVisualStyleBackColor = true;
            this.objMoveFurther.Click += new System.EventHandler(this.objMoveFurther_Click);
            // 
            // objMoveCloser
            // 
            this.objMoveCloser.Location = new System.Drawing.Point(483, 340);
            this.objMoveCloser.Name = "objMoveCloser";
            this.objMoveCloser.Size = new System.Drawing.Size(81, 23);
            this.objMoveCloser.TabIndex = 25;
            this.objMoveCloser.Text = "move Closer";
            this.objMoveCloser.UseVisualStyleBackColor = true;
            this.objMoveCloser.Click += new System.EventHandler(this.objMoveCloser_Click);
            // 
            // objMoveDown
            // 
            this.objMoveDown.Location = new System.Drawing.Point(570, 311);
            this.objMoveDown.Name = "objMoveDown";
            this.objMoveDown.Size = new System.Drawing.Size(81, 23);
            this.objMoveDown.TabIndex = 24;
            this.objMoveDown.Text = "move Down";
            this.objMoveDown.UseVisualStyleBackColor = true;
            this.objMoveDown.Click += new System.EventHandler(this.objMoveDown_Click);
            // 
            // objMoveUp
            // 
            this.objMoveUp.Location = new System.Drawing.Point(484, 311);
            this.objMoveUp.Name = "objMoveUp";
            this.objMoveUp.Size = new System.Drawing.Size(80, 23);
            this.objMoveUp.TabIndex = 23;
            this.objMoveUp.Text = "move Up";
            this.objMoveUp.UseVisualStyleBackColor = true;
            this.objMoveUp.Click += new System.EventHandler(this.objMoveUp_Click);
            // 
            // objMoveRight
            // 
            this.objMoveRight.Location = new System.Drawing.Point(570, 282);
            this.objMoveRight.Name = "objMoveRight";
            this.objMoveRight.Size = new System.Drawing.Size(81, 23);
            this.objMoveRight.TabIndex = 22;
            this.objMoveRight.Text = "move Right";
            this.objMoveRight.UseVisualStyleBackColor = true;
            this.objMoveRight.Click += new System.EventHandler(this.objMoveRight_Click);
            // 
            // objMoveLeft
            // 
            this.objMoveLeft.Location = new System.Drawing.Point(484, 282);
            this.objMoveLeft.Name = "objMoveLeft";
            this.objMoveLeft.Size = new System.Drawing.Size(80, 23);
            this.objMoveLeft.TabIndex = 21;
            this.objMoveLeft.Text = "move Left";
            this.objMoveLeft.UseVisualStyleBackColor = true;
            this.objMoveLeft.Click += new System.EventHandler(this.objMoveLeft_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 435);
            this.Controls.Add(this.objMoveFurther);
            this.Controls.Add(this.objMoveCloser);
            this.Controls.Add(this.objMoveDown);
            this.Controls.Add(this.objMoveUp);
            this.Controls.Add(this.objMoveRight);
            this.Controls.Add(this.objMoveLeft);
            this.Controls.Add(this.objectsList);
            this.Controls.Add(this.chooseObjectLabel);
            this.Controls.Add(this.shadersList);
            this.Controls.Add(this.chooseShaderLabel);
            this.Controls.Add(this.camMoveFurther);
            this.Controls.Add(this.camMoveCloser);
            this.Controls.Add(this.ObjectLabel);
            this.Controls.Add(this.objRotateDown);
            this.Controls.Add(this.objRotateUp);
            this.Controls.Add(this.objRotateRight);
            this.Controls.Add(this.objRotateLeft);
            this.Controls.Add(this.cameraLabel);
            this.Controls.Add(this.camMoveDown);
            this.Controls.Add(this.camMoveUp);
            this.Controls.Add(this.camMoveRight);
            this.Controls.Add(this.camMoveLeft);
            this.Controls.Add(this.glControlMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControlMain;
        private System.Windows.Forms.Button camMoveLeft;
        private System.Windows.Forms.Button camMoveRight;
        private System.Windows.Forms.Button camMoveUp;
        private System.Windows.Forms.Button camMoveDown;
        private System.Windows.Forms.Label cameraLabel;
        private System.Windows.Forms.Label ObjectLabel;
        private System.Windows.Forms.Button objRotateDown;
        private System.Windows.Forms.Button objRotateUp;
        private System.Windows.Forms.Button objRotateRight;
        private System.Windows.Forms.Button objRotateLeft;
        private System.Windows.Forms.Button camMoveFurther;
        private System.Windows.Forms.Button camMoveCloser;
        private System.Windows.Forms.ComboBox shadersList;
        private System.Windows.Forms.Label chooseShaderLabel;
        private System.Windows.Forms.ComboBox objectsList;
        private System.Windows.Forms.Label chooseObjectLabel;
        private System.Windows.Forms.Button objMoveFurther;
        private System.Windows.Forms.Button objMoveCloser;
        private System.Windows.Forms.Button objMoveDown;
        private System.Windows.Forms.Button objMoveUp;
        private System.Windows.Forms.Button objMoveRight;
        private System.Windows.Forms.Button objMoveLeft;
    }
}

