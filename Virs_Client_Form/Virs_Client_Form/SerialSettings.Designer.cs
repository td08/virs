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
            this.SuspendLayout();
            // 
            // baudSelect
            // 
            this.baudSelect.FormattingEnabled = true;
            this.baudSelect.Location = new System.Drawing.Point(16, 51);
            this.baudSelect.Name = "baudSelect";
            this.baudSelect.Size = new System.Drawing.Size(121, 28);
            this.baudSelect.TabIndex = 0;
            // 
            // dataBitSelect
            // 
            this.dataBitSelect.FormattingEnabled = true;
            this.dataBitSelect.Location = new System.Drawing.Point(158, 51);
            this.dataBitSelect.Name = "dataBitSelect";
            this.dataBitSelect.Size = new System.Drawing.Size(121, 28);
            this.dataBitSelect.TabIndex = 1;
            // 
            // stopBitSelect
            // 
            this.stopBitSelect.FormattingEnabled = true;
            this.stopBitSelect.Location = new System.Drawing.Point(300, 50);
            this.stopBitSelect.Name = "stopBitSelect";
            this.stopBitSelect.Size = new System.Drawing.Size(121, 28);
            this.stopBitSelect.TabIndex = 2;
            // 
            // paritySelect
            // 
            this.paritySelect.FormattingEnabled = true;
            this.paritySelect.Location = new System.Drawing.Point(442, 51);
            this.paritySelect.Name = "paritySelect";
            this.paritySelect.Size = new System.Drawing.Size(121, 28);
            this.paritySelect.TabIndex = 3;
            // 
            // flowControlSelect
            // 
            this.flowControlSelect.FormattingEnabled = true;
            this.flowControlSelect.Location = new System.Drawing.Point(584, 50);
            this.flowControlSelect.Name = "flowControlSelect";
            this.flowControlSelect.Size = new System.Drawing.Size(121, 28);
            this.flowControlSelect.TabIndex = 4;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(315, 107);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 33);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // baudLabel
            // 
            this.baudLabel.AutoSize = true;
            this.baudLabel.Location = new System.Drawing.Point(31, 28);
            this.baudLabel.Name = "baudLabel";
            this.baudLabel.Size = new System.Drawing.Size(90, 20);
            this.baudLabel.TabIndex = 6;
            this.baudLabel.Text = "Baud Rate:";
            // 
            // dataBitLabel
            // 
            this.dataBitLabel.AutoSize = true;
            this.dataBitLabel.Location = new System.Drawing.Point(182, 28);
            this.dataBitLabel.Name = "dataBitLabel";
            this.dataBitLabel.Size = new System.Drawing.Size(79, 20);
            this.dataBitLabel.TabIndex = 7;
            this.dataBitLabel.Text = "Data Bits:";
            // 
            // stopBitLabel
            // 
            this.stopBitLabel.AutoSize = true;
            this.stopBitLabel.Location = new System.Drawing.Point(324, 27);
            this.stopBitLabel.Name = "stopBitLabel";
            this.stopBitLabel.Size = new System.Drawing.Size(78, 20);
            this.stopBitLabel.TabIndex = 8;
            this.stopBitLabel.Text = "Stop Bits:";
            // 
            // parityLabel
            // 
            this.parityLabel.AutoSize = true;
            this.parityLabel.Location = new System.Drawing.Point(480, 28);
            this.parityLabel.Name = "parityLabel";
            this.parityLabel.Size = new System.Drawing.Size(52, 20);
            this.parityLabel.TabIndex = 9;
            this.parityLabel.Text = "Parity:";
            // 
            // flowControlLabel
            // 
            this.flowControlLabel.AutoSize = true;
            this.flowControlLabel.Location = new System.Drawing.Point(595, 27);
            this.flowControlLabel.Name = "flowControlLabel";
            this.flowControlLabel.Size = new System.Drawing.Size(101, 20);
            this.flowControlLabel.TabIndex = 10;
            this.flowControlLabel.Text = "Flow Control:";
            // 
            // SerialSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 152);
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
    }
}