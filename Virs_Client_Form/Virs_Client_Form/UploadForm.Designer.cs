namespace Virs_Client_Form
{
    partial class UploadForm
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
            this.ipSelect = new System.Windows.Forms.ComboBox();
            this.ipEntryBox = new System.Windows.Forms.TextBox();
            this.portEntryBox = new System.Windows.Forms.TextBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.prevConnections = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.selectedFileLabel = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ipSelect
            // 
            this.ipSelect.FormattingEnabled = true;
            this.ipSelect.Location = new System.Drawing.Point(310, 43);
            this.ipSelect.Name = "ipSelect";
            this.ipSelect.Size = new System.Drawing.Size(267, 28);
            this.ipSelect.TabIndex = 0;
            this.ipSelect.SelectedIndexChanged += new System.EventHandler(this.ipSelect_SelectedIndexChanged);
            this.ipSelect.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ipSelect_KeyPress);
            // 
            // ipEntryBox
            // 
            this.ipEntryBox.Location = new System.Drawing.Point(28, 44);
            this.ipEntryBox.Name = "ipEntryBox";
            this.ipEntryBox.Size = new System.Drawing.Size(167, 26);
            this.ipEntryBox.TabIndex = 1;
            // 
            // portEntryBox
            // 
            this.portEntryBox.Location = new System.Drawing.Point(201, 44);
            this.portEntryBox.Name = "portEntryBox";
            this.portEntryBox.Size = new System.Drawing.Size(76, 26);
            this.portEntryBox.TabIndex = 2;
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(28, 18);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(91, 20);
            this.ipLabel.TabIndex = 3;
            this.ipLabel.Text = "IP Address:";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(201, 18);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(42, 20);
            this.portLabel.TabIndex = 4;
            this.portLabel.Text = "Port:";
            // 
            // prevConnections
            // 
            this.prevConnections.AutoSize = true;
            this.prevConnections.Location = new System.Drawing.Point(310, 18);
            this.prevConnections.Name = "prevConnections";
            this.prevConnections.Size = new System.Drawing.Size(151, 20);
            this.prevConnections.TabIndex = 5;
            this.prevConnections.Text = "Saved Connections:";
            // 
            // connectButton
            // 
            this.connectButton.Enabled = false;
            this.connectButton.Location = new System.Drawing.Point(399, 77);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(91, 38);
            this.connectButton.TabIndex = 6;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(28, 132);
            this.statusBox.Multiline = true;
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.statusBox.Size = new System.Drawing.Size(549, 169);
            this.statusBox.TabIndex = 7;
            // 
            // selectedFileLabel
            // 
            this.selectedFileLabel.AutoSize = true;
            this.selectedFileLabel.Location = new System.Drawing.Point(136, 86);
            this.selectedFileLabel.Name = "selectedFileLabel";
            this.selectedFileLabel.Size = new System.Drawing.Size(109, 20);
            this.selectedFileLabel.TabIndex = 8;
            this.selectedFileLabel.Text = "Selected File: ";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(27, 77);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(103, 38);
            this.browseButton.TabIndex = 9;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(501, 77);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 38);
            this.clearButton.TabIndex = 10;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // UploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 313);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.selectedFileLabel);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.prevConnections);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.portEntryBox);
            this.Controls.Add(this.ipEntryBox);
            this.Controls.Add(this.ipSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "UploadForm";
            this.Text = "Upload to Server";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UploadForm_FormClosed);
            this.Load += new System.EventHandler(this.UploadForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ipSelect;
        private System.Windows.Forms.TextBox ipEntryBox;
        private System.Windows.Forms.TextBox portEntryBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label prevConnections;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TextBox statusBox;
        private System.Windows.Forms.Label selectedFileLabel;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button clearButton;
    }
}