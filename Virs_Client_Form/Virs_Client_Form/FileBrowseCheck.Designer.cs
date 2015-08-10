namespace Virs_Client_Form
{
    partial class FileBrowseCheck
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
        private void InitializeComponent(bool f1, bool f2, bool f3, bool f4)
        {
            this.file1Check = new System.Windows.Forms.CheckBox();
            this.file2Check = new System.Windows.Forms.CheckBox();
            this.file3Check = new System.Windows.Forms.CheckBox();
            this.file4Check = new System.Windows.Forms.CheckBox();
            this.file1Label = new System.Windows.Forms.Label();
            this.file2Label = new System.Windows.Forms.Label();
            this.file3Label = new System.Windows.Forms.Label();
            this.file4Label = new System.Windows.Forms.Label();
            this.foundLabel1 = new System.Windows.Forms.Label();
            this.foundLabel2 = new System.Windows.Forms.Label();
            this.foundLabel3 = new System.Windows.Forms.Label();
            this.foundLabel4 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(60, 150);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "OK";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // file1Check
            // 
            this.file1Check.AutoSize = true;
            this.file1Check.Location = new System.Drawing.Point(80, 20);
            this.file1Check.Name = "file1Check";
            this.file1Check.Size = new System.Drawing.Size(22, 21);
            this.file1Check.TabIndex = 0;
            this.file1Check.UseVisualStyleBackColor = true;
            this.file1Check.Enabled = false;
            this.file1Check.Checked = f1;
            // 
            // file2Check
            // 
            this.file2Check.AutoSize = true;
            this.file2Check.Location = new System.Drawing.Point(80, 51);
            this.file2Check.Name = "file2Check";
            this.file2Check.Size = new System.Drawing.Size(22, 21);
            this.file2Check.TabIndex = 1;
            this.file2Check.UseVisualStyleBackColor = true;
            this.file2Check.Enabled = false;
            this.file2Check.Checked = f2;
            // 
            // file3Check
            // 
            this.file3Check.AutoSize = true;
            this.file3Check.Location = new System.Drawing.Point(80, 82);
            this.file3Check.Name = "file3Check";
            this.file3Check.Size = new System.Drawing.Size(22, 21);
            this.file3Check.TabIndex = 2;
            this.file3Check.UseVisualStyleBackColor = true;
            this.file3Check.Enabled = false;
            this.file3Check.Checked = f3;
            // 
            // file4Check
            // 
            this.file4Check.AutoSize = true;
            this.file4Check.Location = new System.Drawing.Point(80, 113);
            this.file4Check.Name = "file4Check";
            this.file4Check.Size = new System.Drawing.Size(22, 21);
            this.file4Check.TabIndex = 3;
            this.file4Check.UseVisualStyleBackColor = true;
            this.file4Check.Enabled = false;
            this.file4Check.Checked = f4;
            // 
            // file1Label
            // 
            this.file1Label.AutoSize = true;
            this.file1Label.Location = new System.Drawing.Point(20, 20);
            this.file1Label.Name = "file1Label";
            this.file1Label.Size = new System.Drawing.Size(59, 20);
            this.file1Label.TabIndex = 4;
            this.file1Label.Text = "steth.sts: ";
            // 
            // file2Label
            // 
            this.file2Label.AutoSize = true;
            this.file2Label.Location = new System.Drawing.Point(20, 51);
            this.file2Label.Name = "file2Label";
            this.file2Label.Size = new System.Drawing.Size(51, 20);
            this.file2Label.TabIndex = 5;
            this.file2Label.Text = "pulse.oxi";
            // 
            // file3Label
            // 
            this.file3Label.AutoSize = true;
            this.file3Label.Location = new System.Drawing.Point(20, 82);
            this.file3Label.Name = "file3Label";
            this.file3Label.Size = new System.Drawing.Size(51, 20);
            this.file3Label.TabIndex = 6;
            this.file3Label.Text = "bp.sph: ";
            // 
            // file4Label
            // 
            this.file4Label.AutoSize = true;
            this.file4Label.Location = new System.Drawing.Point(20, 113);
            this.file4Label.Name = "file4Label";
            this.file4Label.Size = new System.Drawing.Size(51, 20);
            this.file4Label.TabIndex = 7;
            this.file4Label.Text = "temp.temp";
            // 
            // foundLabel1
            // 
            this.foundLabel1.AutoSize = true;
            this.foundLabel1.Location = new System.Drawing.Point(110, 20);
            this.foundLabel1.Name = "file4Label";
            this.foundLabel1.Size = new System.Drawing.Size(51, 20);
            this.foundLabel1.TabIndex = 8;
            this.foundLabel1.Text = f1 ? "Found!" : "Not Found!";
            this.foundLabel1.ForeColor = f1 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            // 
            // foundLabel2
            // 
            this.foundLabel2.AutoSize = true;
            this.foundLabel2.Location = new System.Drawing.Point(110, 51);
            this.foundLabel2.Name = "file4Label";
            this.foundLabel2.Size = new System.Drawing.Size(51, 20);
            this.foundLabel2.TabIndex = 9;
            this.foundLabel2.Text = f2 ? "Found!" : "Not Found!";
            this.foundLabel2.ForeColor = f2 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            // 
            // foundLabel3
            // 
            this.foundLabel3.AutoSize = true;
            this.foundLabel3.Location = new System.Drawing.Point(110, 82);
            this.foundLabel3.Name = "file4Label";
            this.foundLabel3.Size = new System.Drawing.Size(51, 20);
            this.foundLabel3.TabIndex = 10;
            this.foundLabel3.Text = f3 ? "Found!" : "Not Found!";
            this.foundLabel3.ForeColor = f3 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            // 
            // foundLabel4
            // 
            this.foundLabel4.AutoSize = true;
            this.foundLabel4.Location = new System.Drawing.Point(110, 113);
            this.foundLabel4.Name = "file4Label";
            this.foundLabel4.Size = new System.Drawing.Size(51, 20);
            this.foundLabel4.TabIndex = 11;
            this.foundLabel4.Text = f4 ? "Found!" : "Not Found!";
            this.foundLabel4.ForeColor = f4 ? System.Drawing.Color.Green : System.Drawing.Color.Red;
            // 
            // FileBrowseCheck
            // 
            this.Location = new System.Drawing.Point(0, 0);
            this.ClientSize = new System.Drawing.Size(120, 200);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.foundLabel4);
            this.Controls.Add(this.foundLabel3);
            this.Controls.Add(this.foundLabel2);
            this.Controls.Add(this.foundLabel1);
            this.Controls.Add(this.file4Label);
            this.Controls.Add(this.file3Label);
            this.Controls.Add(this.file2Label);
            this.Controls.Add(this.file1Label);
            this.Controls.Add(this.file4Check);
            this.Controls.Add(this.file3Check);
            this.Controls.Add(this.file2Check);
            this.Controls.Add(this.file1Check);
            this.Name = "FileBrowseCheck";
            this.Text = "File Check";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox file1Check;
        private System.Windows.Forms.CheckBox file2Check;
        private System.Windows.Forms.CheckBox file3Check;
        private System.Windows.Forms.CheckBox file4Check;
        private System.Windows.Forms.Label file1Label;
        private System.Windows.Forms.Label file2Label;
        private System.Windows.Forms.Label file3Label;
        private System.Windows.Forms.Label file4Label;
        private System.Windows.Forms.Label foundLabel1;
        private System.Windows.Forms.Label foundLabel2;
        private System.Windows.Forms.Label foundLabel3;
        private System.Windows.Forms.Label foundLabel4;
        private System.Windows.Forms.Button closeButton;
    }
}