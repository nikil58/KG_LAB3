namespace LAB3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openGlController = new SharpGL.OpenGLControl();
            this.resetButton = new System.Windows.Forms.Button();
            this.pointsButton = new System.Windows.Forms.Button();
            this.axisComboBox = new System.Windows.Forms.ComboBox();
            this.axisLabel = new System.Windows.Forms.Label();
            this.angleLabel = new System.Windows.Forms.Label();
            this.angleTextBox = new System.Windows.Forms.TextBox();
            this.rotateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.openGlController)).BeginInit();
            this.SuspendLayout();
            // 
            // openGlController
            // 
            this.openGlController.DrawFPS = false;
            this.openGlController.Location = new System.Drawing.Point(-2, -1);
            this.openGlController.Name = "openGlController";
            this.openGlController.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            this.openGlController.RenderContextType = SharpGL.RenderContextType.DIBSection;
            this.openGlController.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            this.openGlController.Size = new System.Drawing.Size(801, 441);
            this.openGlController.TabIndex = 0;
            this.openGlController.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGlController_OpenGLDraw);
            this.openGlController.Resized += new System.EventHandler(this.openGlController_Resized);
            // 
            // resetButton
            // 
            this.resetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resetButton.Location = new System.Drawing.Point(660, 547);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(112, 44);
            this.resetButton.TabIndex = 1;
            this.resetButton.Text = "Сбросить";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // pointsButton
            // 
            this.pointsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pointsButton.Location = new System.Drawing.Point(12, 550);
            this.pointsButton.Name = "pointsButton";
            this.pointsButton.Size = new System.Drawing.Size(143, 41);
            this.pointsButton.TabIndex = 2;
            this.pointsButton.Text = "Координаты";
            this.pointsButton.UseVisualStyleBackColor = true;
            this.pointsButton.Click += new System.EventHandler(this.pointsButton_Click);
            // 
            // axisComboBox
            // 
            this.axisComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.axisComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.axisComboBox.FormattingEnabled = true;
            this.axisComboBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.axisComboBox.Items.AddRange(new object[] {
            "X",
            "Y"});
            this.axisComboBox.Location = new System.Drawing.Point(80, 474);
            this.axisComboBox.Name = "axisComboBox";
            this.axisComboBox.Size = new System.Drawing.Size(121, 33);
            this.axisComboBox.TabIndex = 3;
            // 
            // axisLabel
            // 
            this.axisLabel.AutoSize = true;
            this.axisLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.axisLabel.Location = new System.Drawing.Point(12, 478);
            this.axisLabel.Name = "axisLabel";
            this.axisLabel.Size = new System.Drawing.Size(46, 24);
            this.axisLabel.TabIndex = 4;
            this.axisLabel.Text = "Ось";
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.angleLabel.Location = new System.Drawing.Point(252, 478);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.Size = new System.Drawing.Size(52, 24);
            this.angleLabel.TabIndex = 5;
            this.angleLabel.Text = "Угол";
            // 
            // angleTextBox
            // 
            this.angleTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.angleTextBox.Location = new System.Drawing.Point(346, 478);
            this.angleTextBox.Name = "angleTextBox";
            this.angleTextBox.Size = new System.Drawing.Size(143, 29);
            this.angleTextBox.TabIndex = 6;
            // 
            // rotateButton
            // 
            this.rotateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rotateButton.Location = new System.Drawing.Point(644, 472);
            this.rotateButton.Name = "rotateButton";
            this.rotateButton.Size = new System.Drawing.Size(128, 43);
            this.rotateButton.TabIndex = 7;
            this.rotateButton.Text = "Повернуть";
            this.rotateButton.UseVisualStyleBackColor = true;
            this.rotateButton.Click += new System.EventHandler(this.rotateButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 626);
            this.Controls.Add(this.rotateButton);
            this.Controls.Add(this.angleTextBox);
            this.Controls.Add(this.angleLabel);
            this.Controls.Add(this.axisLabel);
            this.Controls.Add(this.axisComboBox);
            this.Controls.Add(this.pointsButton);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.openGlController);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.openGlController)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpGL.OpenGLControl openGlController;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Button pointsButton;
        private System.Windows.Forms.ComboBox axisComboBox;
        private System.Windows.Forms.Label axisLabel;
        private System.Windows.Forms.Label angleLabel;
        private System.Windows.Forms.TextBox angleTextBox;
        private System.Windows.Forms.Button rotateButton;
    }
}

