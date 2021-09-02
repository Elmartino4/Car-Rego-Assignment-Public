using Car_Rego_Assignment.Properties;

namespace Car_Rego_Assignment
{
    partial class ErrorBox
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

        public System.Windows.Forms.PictureBox okButton = new System.Windows.Forms.PictureBox();
        public System.Windows.Forms.Label errorMessage = new System.Windows.Forms.Label();

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // Start Button
            //
            this.okButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.okButton.BackgroundImage = Resources.InvalidEntryButton;
            this.okButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.okButton.Size = new System.Drawing.Size(135, 69);
            this.okButton.TabIndex = 2;
            this.okButton.TabStop = false;
            this.okButton.Click += (sender, EventArgs) => { this.Close(); };
            //
            // Error Message
            //
            this.errorMessage.Font = new System.Drawing.Font("Segoe UI", 20F);
            this.errorMessage.Size = new System.Drawing.Size(500, 250);
            //this.errorMessage.AutoSize = true;
            this.errorMessage.Text = "Owner's Name:";
            this.errorMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ErrorBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.MinimumSize = new System.Drawing.Size(680, 320);
            this.MaximumSize = new System.Drawing.Size(680, 320);
            this.Resize += ErrorBox_Resize;
            this.Load += ErrorBox_Resize;
            this.Name = "ErrorBox";
            this.Text = "Error Box";
            this.ResumeLayout(false);

            this.Controls.Add(this.okButton);
            this.Controls.Add(this.errorMessage);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
        }
    }
}