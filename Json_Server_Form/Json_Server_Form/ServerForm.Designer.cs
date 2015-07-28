namespace Json_Server_Form
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
            this.startButton = new System.Windows.Forms.Button();
            this.ipLabel = new System.Windows.Forms.Label();
            this.localStartButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.outputDisplay = new System.Windows.Forms.TextBox();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(23, 11);
            this.startButton.Margin = new System.Windows.Forms.Padding(2);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(50, 54);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Server";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(93, 48);
            this.ipLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(64, 13);
            this.ipLabel.TabIndex = 1;
            this.ipLabel.Text = "IP Address: ";
            // 
            // localStartButton
            // 
            this.localStartButton.Location = new System.Drawing.Point(23, 88);
            this.localStartButton.Margin = new System.Windows.Forms.Padding(2);
            this.localStartButton.Name = "localStartButton";
            this.localStartButton.Size = new System.Drawing.Size(50, 54);
            this.localStartButton.TabIndex = 2;
            this.localStartButton.Text = "Start Local Server";
            this.localStartButton.UseVisualStyleBackColor = true;
            this.localStartButton.Click += new System.EventHandler(this.localStartButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(23, 166);
            this.stopButton.Margin = new System.Windows.Forms.Padding(2);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(50, 54);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Stop Server";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // outputDisplay
            // 
            this.outputDisplay.Location = new System.Drawing.Point(96, 88);
            this.outputDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.outputDisplay.Multiline = true;
            this.outputDisplay.Name = "outputDisplay";
            this.outputDisplay.ReadOnly = true;
            this.outputDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputDisplay.Size = new System.Drawing.Size(181, 132);
            this.outputDisplay.TabIndex = 4;
            this.outputDisplay.Text = "Status\r\n-----------\r\n";
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(284, 7);
            this.OpenFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(50, 54);
            this.OpenFileButton.TabIndex = 5;
            this.OpenFileButton.Text = "Open File";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 245);
            this.Controls.Add(this.OpenFileButton);
            this.Controls.Add(this.outputDisplay);
            this.Controls.Add(this.localStartButton);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.stopButton);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ServerForm";
            this.Text = "Server Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Button localStartButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.TextBox outputDisplay;
        private System.Windows.Forms.Button OpenFileButton;
    }
}

