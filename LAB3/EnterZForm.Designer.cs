namespace LAB3
{
    partial class EnterZForm
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
            this.zTextBox = new System.Windows.Forms.TextBox();
            this.saveZButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // zTextBox
            // 
            this.zTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.zTextBox.Location = new System.Drawing.Point(143, 50);
            this.zTextBox.Name = "zTextBox";
            this.zTextBox.Size = new System.Drawing.Size(100, 29);
            this.zTextBox.TabIndex = 0;
            // 
            // saveZButton
            // 
            this.saveZButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.saveZButton.Location = new System.Drawing.Point(126, 107);
            this.saveZButton.Name = "saveZButton";
            this.saveZButton.Size = new System.Drawing.Size(134, 36);
            this.saveZButton.TabIndex = 1;
            this.saveZButton.Text = "Сохранить";
            this.saveZButton.UseVisualStyleBackColor = true;
            this.saveZButton.Click += new System.EventHandler(this.saveZButton_Click);
            // 
            // EnterZForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 196);
            this.Controls.Add(this.saveZButton);
            this.Controls.Add(this.zTextBox);
            this.Name = "EnterZForm";
            this.Text = "Введите координаты Z";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox zTextBox;
        private System.Windows.Forms.Button saveZButton;
    }
}