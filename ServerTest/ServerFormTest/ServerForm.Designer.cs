namespace ServerFormTest
{
    partial class ServerForm
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
            this.serverStart = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverStart
            // 
            this.serverStart.Location = new System.Drawing.Point(12, 27);
            this.serverStart.Name = "serverStart";
            this.serverStart.Size = new System.Drawing.Size(75, 23);
            this.serverStart.TabIndex = 0;
            this.serverStart.Text = "Start Server";
            this.serverStart.UseVisualStyleBackColor = true;
            this.serverStart.Click += new System.EventHandler(this.serverStart_Click);
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(120, 12);
            this.statusBox.Multiline = true;
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.Size = new System.Drawing.Size(260, 368);
            this.statusBox.TabIndex = 1;
            this.statusBox.Text = "Server Status\r\n-----------------------\r\n";
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(13, 103);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop Server";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 392);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.serverStart);
            this.Name = "ServerForm";
            this.Text = "Server Form";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button serverStart;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.Button stopButton;
    }
}

