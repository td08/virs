namespace Virs_Client_Form
{
    partial class UserSettings
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
            this.pathSelect = new System.Windows.Forms.ComboBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.firstNameBox = new System.Windows.Forms.TextBox();
            this.lastNameBox = new System.Windows.Forms.TextBox();
            this.firstNameLabel = new System.Windows.Forms.Label();
            this.lastNameLabel = new System.Windows.Forms.Label();
            this.ageBox = new System.Windows.Forms.TextBox();
            this.weightBox = new System.Windows.Forms.TextBox();
            this.ageLabel = new System.Windows.Forms.Label();
            this.weightLabel = new System.Windows.Forms.Label();
            this.weightUnitsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pathSelect
            // 
            this.pathSelect.FormattingEnabled = true;
            this.pathSelect.Location = new System.Drawing.Point(13, 38);
            this.pathSelect.Name = "pathSelect";
            this.pathSelect.Size = new System.Drawing.Size(442, 28);
            this.pathSelect.TabIndex = 0;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(461, 33);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(94, 38);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Local VIRS File Directory:";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(246, 235);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 38);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(461, 235);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(94, 38);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // firstNameBox
            // 
            this.firstNameBox.Location = new System.Drawing.Point(17, 112);
            this.firstNameBox.Name = "firstNameBox";
            this.firstNameBox.Size = new System.Drawing.Size(144, 26);
            this.firstNameBox.TabIndex = 5;
            // 
            // lastNameBox
            // 
            this.lastNameBox.Location = new System.Drawing.Point(311, 112);
            this.lastNameBox.Name = "lastNameBox";
            this.lastNameBox.Size = new System.Drawing.Size(144, 26);
            this.lastNameBox.TabIndex = 6;
            // 
            // firstNameLabel
            // 
            this.firstNameLabel.AutoSize = true;
            this.firstNameLabel.Location = new System.Drawing.Point(17, 86);
            this.firstNameLabel.Name = "firstNameLabel";
            this.firstNameLabel.Size = new System.Drawing.Size(144, 20);
            this.firstNameLabel.TabIndex = 7;
            this.firstNameLabel.Text = "Patient First Name:";
            // 
            // lastNameLabel
            // 
            this.lastNameLabel.AutoSize = true;
            this.lastNameLabel.Location = new System.Drawing.Point(311, 86);
            this.lastNameLabel.Name = "lastNameLabel";
            this.lastNameLabel.Size = new System.Drawing.Size(144, 20);
            this.lastNameLabel.TabIndex = 8;
            this.lastNameLabel.Text = "Patient Last Name:";
            // 
            // ageBox
            // 
            this.ageBox.Location = new System.Drawing.Point(17, 188);
            this.ageBox.Name = "ageBox";
            this.ageBox.Size = new System.Drawing.Size(144, 26);
            this.ageBox.TabIndex = 9;
            // 
            // weightBox
            // 
            this.weightBox.Location = new System.Drawing.Point(311, 188);
            this.weightBox.Name = "weightBox";
            this.weightBox.Size = new System.Drawing.Size(144, 26);
            this.weightBox.TabIndex = 10;
            // 
            // ageLabel
            // 
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(17, 162);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(42, 20);
            this.ageLabel.TabIndex = 11;
            this.ageLabel.Text = "Age:";
            // 
            // weightLabel
            // 
            this.weightLabel.AutoSize = true;
            this.weightLabel.Location = new System.Drawing.Point(311, 161);
            this.weightLabel.Name = "weightLabel";
            this.weightLabel.Size = new System.Drawing.Size(63, 20);
            this.weightLabel.TabIndex = 12;
            this.weightLabel.Text = "Weight:";
            // 
            // weightUnitsLabel
            // 
            this.weightUnitsLabel.AutoSize = true;
            this.weightUnitsLabel.Location = new System.Drawing.Point(457, 191);
            this.weightUnitsLabel.Name = "weightUnitsLabel";
            this.weightUnitsLabel.Size = new System.Drawing.Size(31, 20);
            this.weightUnitsLabel.TabIndex = 13;
            this.weightUnitsLabel.Text = "Ibs";
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 290);
            this.Controls.Add(this.weightUnitsLabel);
            this.Controls.Add(this.weightLabel);
            this.Controls.Add(this.ageLabel);
            this.Controls.Add(this.weightBox);
            this.Controls.Add(this.ageBox);
            this.Controls.Add(this.lastNameLabel);
            this.Controls.Add(this.firstNameLabel);
            this.Controls.Add(this.lastNameBox);
            this.Controls.Add(this.firstNameBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.pathSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "UserSettings";
            this.Text = "User Settings";
            this.Load += new System.EventHandler(this.ClientSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox pathSelect;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TextBox firstNameBox;
        private System.Windows.Forms.TextBox lastNameBox;
        private System.Windows.Forms.Label firstNameLabel;
        private System.Windows.Forms.Label lastNameLabel;
        private System.Windows.Forms.TextBox ageBox;
        private System.Windows.Forms.TextBox weightBox;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.Label weightLabel;
        private System.Windows.Forms.Label weightUnitsLabel;
    }
}