namespace Virs_Client_Form
{
    partial class SerialSettings
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
            this.baudSelect = new System.Windows.Forms.ComboBox();
            this.dataBitSelect = new System.Windows.Forms.ComboBox();
            this.stopBitSelect = new System.Windows.Forms.ComboBox();
            this.paritySelect = new System.Windows.Forms.ComboBox();
            this.flowControlSelect = new System.Windows.Forms.ComboBox();
            this.okButton = new System.Windows.Forms.Button();
            this.baudLabel = new System.Windows.Forms.Label();
            this.dataBitLabel = new System.Windows.Forms.Label();
            this.stopBitLabel = new System.Windows.Forms.Label();
            this.parityLabel = new System.Windows.Forms.Label();
            this.flowControlLabel = new System.Windows.Forms.Label();
            this.defaultButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // baudSelect
            // 
            this.baudSelect.FormattingEnabled = true;
            this.baudSelect.Location = new System.Drawing.Point(11, 33);
            this.baudSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.baudSelect.Name = "baudSelect";
            this.baudSelect.Size = new System.Drawing.Size(82, 21);
            this.baudSelect.TabIndex = 0;
            // 
            // dataBitSelect
            // 
            this.dataBitSelect.FormattingEnabled = true;
            this.dataBitSelect.Location = new System.Drawing.Point(105, 33);
            this.dataBitSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataBitSelect.Name = "dataBitSelect";
            this.dataBitSelect.Size = new System.Drawing.Size(82, 21);
            this.dataBitSelect.TabIndex = 1;
            // 
            // stopBitSelect
            // 
            this.stopBitSelect.FormattingEnabled = true;
            this.stopBitSelect.Location = new System.Drawing.Point(200, 32);
            this.stopBitSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stopBitSelect.Name = "stopBitSelect";
            this.stopBitSelect.Size = new System.Drawing.Size(82, 21);
            this.stopBitSelect.TabIndex = 2;
            // 
            // paritySelect
            // 
            this.paritySelect.FormattingEnabled = true;
            this.paritySelect.Location = new System.Drawing.Point(295, 33);
            this.paritySelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.paritySelect.Name = "paritySelect";
            this.paritySelect.Size = new System.Drawing.Size(82, 21);
            this.paritySelect.TabIndex = 3;
            // 
            // flowControlSelect
            // 
            this.flowControlSelect.FormattingEnabled = true;
            this.flowControlSelect.Location = new System.Drawing.Point(389, 32);
            this.flowControlSelect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.flowControlSelect.Name = "flowControlSelect";
            this.flowControlSelect.Size = new System.Drawing.Size(82, 21);
            this.flowControlSelect.TabIndex = 4;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(217, 67);
            this.okButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(50, 21);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // baudLabel
            // 
            this.baudLabel.AutoSize = true;
            this.baudLabel.Location = new System.Drawing.Point(22, 18);
            this.baudLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.baudLabel.Name = "baudLabel";
            this.baudLabel.Size = new System.Drawing.Size(61, 13);
            this.baudLabel.TabIndex = 6;
            this.baudLabel.Text = "Baud Rate:";
            // 
            // dataBitLabel
            // 
            this.dataBitLabel.AutoSize = true;
            this.dataBitLabel.Location = new System.Drawing.Point(120, 18);
            this.dataBitLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dataBitLabel.Name = "dataBitLabel";
            this.dataBitLabel.Size = new System.Drawing.Size(53, 13);
            this.dataBitLabel.TabIndex = 7;
            this.dataBitLabel.Text = "Data Bits:";
            // 
            // stopBitLabel
            // 
            this.stopBitLabel.AutoSize = true;
            this.stopBitLabel.Location = new System.Drawing.Point(215, 18);
            this.stopBitLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.stopBitLabel.Name = "stopBitLabel";
            this.stopBitLabel.Size = new System.Drawing.Size(52, 13);
            this.stopBitLabel.TabIndex = 8;
            this.stopBitLabel.Text = "Stop Bits:";
            // 
            // parityLabel
            // 
            this.parityLabel.AutoSize = true;
            this.parityLabel.Location = new System.Drawing.Point(318, 18);
            this.parityLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.parityLabel.Name = "parityLabel";
            this.parityLabel.Size = new System.Drawing.Size(36, 13);
            this.parityLabel.TabIndex = 9;
            this.parityLabel.Text = "Parity:";
            // 
            // flowControlLabel
            // 
            this.flowControlLabel.AutoSize = true;
            this.flowControlLabel.Location = new System.Drawing.Point(396, 18);
            this.flowControlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.flowControlLabel.Name = "flowControlLabel";
            this.flowControlLabel.Size = new System.Drawing.Size(68, 13);
            this.flowControlLabel.TabIndex = 10;
            this.flowControlLabel.Text = "Flow Control:";
            // 
            // defaultButton
            // 
            this.defaultButton.Location = new System.Drawing.Point(421, 67);
            this.defaultButton.Name = "defaultButton";
            this.defaultButton.Size = new System.Drawing.Size(50, 21);
            this.defaultButton.TabIndex = 11;
            this.defaultButton.Text = "Default";
            this.defaultButton.UseVisualStyleBackColor = true;
            this.defaultButton.Click += new System.EventHandler(this.defaultButton_Click);
            // 
            // SerialSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 99);
            this.Controls.Add(this.defaultButton);
            this.Controls.Add(this.flowControlLabel);
            this.Controls.Add(this.parityLabel);
            this.Controls.Add(this.stopBitLabel);
            this.Controls.Add(this.dataBitLabel);
            this.Controls.Add(this.baudLabel);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.flowControlSelect);
            this.Controls.Add(this.paritySelect);
            this.Controls.Add(this.stopBitSelect);
            this.Controls.Add(this.dataBitSelect);
            this.Controls.Add(this.baudSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SerialSettings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox baudSelect;
        private System.Windows.Forms.ComboBox dataBitSelect;
        private System.Windows.Forms.ComboBox stopBitSelect;
        private System.Windows.Forms.ComboBox paritySelect;
        private System.Windows.Forms.ComboBox flowControlSelect;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label baudLabel;
        private System.Windows.Forms.Label dataBitLabel;
        private System.Windows.Forms.Label stopBitLabel;
        private System.Windows.Forms.Label parityLabel;
        private System.Windows.Forms.Label flowControlLabel;
        private System.Windows.Forms.Button defaultButton;
    }
}