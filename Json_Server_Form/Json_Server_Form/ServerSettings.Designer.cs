namespace Json_Server_Form
{
    partial class ServerSettings
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
            this.browseButton = new System.Windows.Forms.Button();
            this.pathSelect = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.fileDirectoryLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.localhostCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(486, 35);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(84, 40);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // pathSelect
            // 
            this.pathSelect.FormattingEnabled = true;
            this.pathSelect.Location = new System.Drawing.Point(12, 42);
            this.pathSelect.Name = "pathSelect";
            this.pathSelect.Size = new System.Drawing.Size(468, 28);
            this.pathSelect.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(254, 124);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 40);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // fileDirectoryLabel
            // 
            this.fileDirectoryLabel.AutoSize = true;
            this.fileDirectoryLabel.Location = new System.Drawing.Point(13, 13);
            this.fileDirectoryLabel.Name = "fileDirectoryLabel";
            this.fileDirectoryLabel.Size = new System.Drawing.Size(202, 20);
            this.fileDirectoryLabel.TabIndex = 3;
            this.fileDirectoryLabel.Text = "VIRS Patient File Directory:";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(486, 83);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(84, 40);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // localhostCheck
            // 
            this.localhostCheck.AutoSize = true;
            this.localhostCheck.Location = new System.Drawing.Point(12, 92);
            this.localhostCheck.Name = "localhostCheck";
            this.localhostCheck.Size = new System.Drawing.Size(203, 24);
            this.localhostCheck.TabIndex = 5;
            this.localhostCheck.Text = "Start in Localhost Mode";
            this.localhostCheck.UseVisualStyleBackColor = true;
            // 
            // ServerSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 189);
            this.Controls.Add(this.localhostCheck);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.fileDirectoryLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.pathSelect);
            this.Controls.Add(this.browseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ServerSettings";
            this.Text = "ServerSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.ComboBox pathSelect;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label fileDirectoryLabel;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox localhostCheck;
    }
}