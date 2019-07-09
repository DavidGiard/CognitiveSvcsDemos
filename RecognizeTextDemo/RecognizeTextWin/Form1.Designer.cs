namespace RecognizeTextWin
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
            this.GetTextButton = new System.Windows.Forms.Button();
            this.LocationAddressLabel = new System.Windows.Forms.Label();
            this.ResultsTextbox = new System.Windows.Forms.TextBox();
            this.StartOcrFromFileButton = new System.Windows.Forms.Button();
            this.GetFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.FileNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GetTextButton
            // 
            this.GetTextButton.Location = new System.Drawing.Point(20, 88);
            this.GetTextButton.Name = "GetTextButton";
            this.GetTextButton.Size = new System.Drawing.Size(183, 30);
            this.GetTextButton.TabIndex = 3;
            this.GetTextButton.Text = "Get Text";
            this.GetTextButton.UseVisualStyleBackColor = true;
            this.GetTextButton.Click += new System.EventHandler(this.GetTextButton_Click);
            // 
            // LocationAddressLabel
            // 
            this.LocationAddressLabel.AutoSize = true;
            this.LocationAddressLabel.Location = new System.Drawing.Point(17, 134);
            this.LocationAddressLabel.Name = "LocationAddressLabel";
            this.LocationAddressLabel.Size = new System.Drawing.Size(89, 13);
            this.LocationAddressLabel.TabIndex = 4;
            this.LocationAddressLabel.Text = "Location Address";
            // 
            // ResultsTextbox
            // 
            this.ResultsTextbox.Location = new System.Drawing.Point(20, 175);
            this.ResultsTextbox.Multiline = true;
            this.ResultsTextbox.Name = "ResultsTextbox";
            this.ResultsTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultsTextbox.Size = new System.Drawing.Size(835, 500);
            this.ResultsTextbox.TabIndex = 5;
            // 
            // StartOcrFromFileButton
            // 
            this.StartOcrFromFileButton.Location = new System.Drawing.Point(20, 53);
            this.StartOcrFromFileButton.Name = "StartOcrFromFileButton";
            this.StartOcrFromFileButton.Size = new System.Drawing.Size(183, 29);
            this.StartOcrFromFileButton.TabIndex = 6;
            this.StartOcrFromFileButton.Text = "Start OCR (from file)";
            this.StartOcrFromFileButton.UseVisualStyleBackColor = true;
            this.StartOcrFromFileButton.Click += new System.EventHandler(this.StartOcrFromFileButton_Click);
            // 
            // GetFileButton
            // 
            this.GetFileButton.Location = new System.Drawing.Point(20, 14);
            this.GetFileButton.Name = "GetFileButton";
            this.GetFileButton.Size = new System.Drawing.Size(183, 33);
            this.GetFileButton.TabIndex = 7;
            this.GetFileButton.Text = "Get File";
            this.GetFileButton.UseVisualStyleBackColor = true;
            this.GetFileButton.Click += new System.EventHandler(this.GetFileButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FileNameLabel
            // 
            this.FileNameLabel.AutoSize = true;
            this.FileNameLabel.Location = new System.Drawing.Point(233, 14);
            this.FileNameLabel.Name = "FileNameLabel";
            this.FileNameLabel.Size = new System.Drawing.Size(58, 13);
            this.FileNameLabel.TabIndex = 8;
            this.FileNameLabel.Text = "File name: ";
            this.FileNameLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 703);
            this.Controls.Add(this.FileNameLabel);
            this.Controls.Add(this.GetFileButton);
            this.Controls.Add(this.StartOcrFromFileButton);
            this.Controls.Add(this.ResultsTextbox);
            this.Controls.Add(this.LocationAddressLabel);
            this.Controls.Add(this.GetTextButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button GetTextButton;
        private System.Windows.Forms.Label LocationAddressLabel;
        private System.Windows.Forms.TextBox ResultsTextbox;
        private System.Windows.Forms.Button StartOcrFromFileButton;
        private System.Windows.Forms.Button GetFileButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label FileNameLabel;
    }
}

