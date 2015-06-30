namespace Json_Client_Form
{
    partial class InputBox
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
        private void InitializeComponent(string title, string label)
        {
            this.inputLabel = new System.Windows.Forms.Label();
            this.inputText = new System.Windows.Forms.TextBox();
            this.inputConfirm = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(73, 59);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(51, 20);
            this.inputLabel.TabIndex = 0;
            this.inputLabel.Text = label;
            // 
            // inputText
            // 
            this.inputText.Location = new System.Drawing.Point(73, 83);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(150, 26);
            this.inputText.TabIndex = 1;
            // 
            // inputConfirm
            // 
            this.inputConfirm.Location = new System.Drawing.Point(112, 116);
            this.inputConfirm.Name = "inputConfirm";
            this.inputConfirm.Size = new System.Drawing.Size(75, 46);
            this.inputConfirm.TabIndex = 2;
            this.inputConfirm.Text = "OK";
            this.inputConfirm.UseVisualStyleBackColor = true;
            this.inputConfirm.Click += new System.EventHandler(this.inputConfirm_Click);
            this.inputConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;

            // 
            // InputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Controls.Add(this.inputConfirm);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.inputLabel);
            this.Name = "InputBox";
            this.Text = title;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label inputLabel;
        public System.Windows.Forms.TextBox inputText;
        private System.Windows.Forms.Button inputConfirm;
    }
}