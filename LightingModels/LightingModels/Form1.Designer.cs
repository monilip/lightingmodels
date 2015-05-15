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
            this.camMoveLeft.Size = new System.Drawing.Size(75, 23);
            this.camMoveLeft.TabIndex = 1;
            this.camMoveLeft.Text = "move Left";
            this.camMoveLeft.UseVisualStyleBackColor = true;
            this.camMoveLeft.Click += new System.EventHandler(this.camMoveLeft_Click);
            // 
            // camMoveRight
            // 
            this.camMoveRight.Location = new System.Drawing.Point(567, 29);
            this.camMoveRight.Name = "camMoveRight";
            this.camMoveRight.Size = new System.Drawing.Size(75, 23);
            this.camMoveRight.TabIndex = 2;
            this.camMoveRight.Text = "move Right";
            this.camMoveRight.UseVisualStyleBackColor = true;
            this.camMoveRight.Click += new System.EventHandler(this.camMoveRight_Click);
            // 
            // camMoveUp
            // 
            this.camMoveUp.Location = new System.Drawing.Point(485, 58);
            this.camMoveUp.Name = "camMoveUp";
            this.camMoveUp.Size = new System.Drawing.Size(75, 23);
            this.camMoveUp.TabIndex = 3;
            this.camMoveUp.Text = "move Up";
            this.camMoveUp.UseVisualStyleBackColor = true;
            this.camMoveUp.Click += new System.EventHandler(this.camMoveUp_Click);
            // 
            // camMoveDown
            // 
            this.camMoveDown.Location = new System.Drawing.Point(567, 58);
            this.camMoveDown.Name = "camMoveDown";
            this.camMoveDown.Size = new System.Drawing.Size(75, 23);
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
            this.cameraLabel.Size = new System.Drawing.Size(100, 13);
            this.cameraLabel.TabIndex = 5;
            this.cameraLabel.Text = "Camera movements";
            // 
            // ObjectLabel
            // 
            this.ObjectLabel.AutoSize = true;
            this.ObjectLabel.Location = new System.Drawing.Point(516, 84);
            this.ObjectLabel.Name = "ObjectLabel";
            this.ObjectLabel.Size = new System.Drawing.Size(95, 13);
            this.ObjectLabel.TabIndex = 10;
            this.ObjectLabel.Text = "Object movements";
            // 
            // objRotateDown
            // 
            this.objRotateDown.Location = new System.Drawing.Point(567, 132);
            this.objRotateDown.Name = "objRotateDown";
            this.objRotateDown.Size = new System.Drawing.Size(75, 23);
            this.objRotateDown.TabIndex = 9;
            this.objRotateDown.Text = "rotate Down";
            this.objRotateDown.UseVisualStyleBackColor = true;
            this.objRotateDown.Click += new System.EventHandler(this.objRotateDown_Click);
            // 
            // objRotateUp
            // 
            this.objRotateUp.Location = new System.Drawing.Point(485, 132);
            this.objRotateUp.Name = "objRotateUp";
            this.objRotateUp.Size = new System.Drawing.Size(75, 23);
            this.objRotateUp.TabIndex = 8;
            this.objRotateUp.Text = "rotate Up";
            this.objRotateUp.UseVisualStyleBackColor = true;
            this.objRotateUp.Click += new System.EventHandler(this.objRotateUp_Click);
            // 
            // objRotateRight
            // 
            this.objRotateRight.Location = new System.Drawing.Point(567, 103);
            this.objRotateRight.Name = "objRotateRight";
            this.objRotateRight.Size = new System.Drawing.Size(75, 23);
            this.objRotateRight.TabIndex = 7;
            this.objRotateRight.Text = "rotate Right";
            this.objRotateRight.UseVisualStyleBackColor = true;
            this.objRotateRight.Click += new System.EventHandler(this.objRotateRight_Click);
            // 
            // objRotateLeft
            // 
            this.objRotateLeft.Location = new System.Drawing.Point(486, 103);
            this.objRotateLeft.Name = "objRotateLeft";
            this.objRotateLeft.Size = new System.Drawing.Size(75, 23);
            this.objRotateLeft.TabIndex = 6;
            this.objRotateLeft.Text = "rotate Left";
            this.objRotateLeft.UseVisualStyleBackColor = true;
            this.objRotateLeft.Click += new System.EventHandler(this.objRotateLeft_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 435);
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
    }
}

