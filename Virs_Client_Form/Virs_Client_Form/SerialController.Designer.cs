namespace Virs_Client_Form
{
    partial class SerialController
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
            this.scanButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comPortSelect = new System.Windows.Forms.ComboBox();
            this.runSteth = new System.Windows.Forms.Button();
            this.runPulse = new System.Windows.Forms.Button();
            this.runBP = new System.Windows.Forms.Button();
            this.runTemp = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.TextBox();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // scanButton
            // 
            this.scanButton.Location = new System.Drawing.Point(9, 23);
            this.scanButton.Margin = new System.Windows.Forms.Padding(2);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(65, 24);
            this.scanButton.TabIndex = 0;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Enabled = false;
            this.connectButton.Location = new System.Drawing.Point(78, 23);
            this.connectButton.Margin = new System.Windows.Forms.Padding(2);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(65, 24);
            this.connectButton.TabIndex = 1;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip.Size = new System.Drawing.Size(296, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 22);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // comPortSelect
            // 
            this.comPortSelect.Enabled = false;
            this.comPortSelect.FormattingEnabled = true;
            this.comPortSelect.Location = new System.Drawing.Point(163, 23);
            this.comPortSelect.Name = "comPortSelect";
            this.comPortSelect.Size = new System.Drawing.Size(121, 21);
            this.comPortSelect.TabIndex = 5;
            // 
            // runSteth
            // 
            this.runSteth.Enabled = false;
            this.runSteth.Location = new System.Drawing.Point(36, 60);
            this.runSteth.Name = "runSteth";
            this.runSteth.Size = new System.Drawing.Size(90, 23);
            this.runSteth.TabIndex = 6;
            this.runSteth.Text = "Stethoscope";
            this.runSteth.UseVisualStyleBackColor = true;
            this.runSteth.Click += new System.EventHandler(this.runSteth_Click);
            // 
            // runPulse
            // 
            this.runPulse.Enabled = false;
            this.runPulse.Location = new System.Drawing.Point(170, 60);
            this.runPulse.Name = "runPulse";
            this.runPulse.Size = new System.Drawing.Size(90, 23);
            this.runPulse.TabIndex = 7;
            this.runPulse.Text = "Pulse";
            this.runPulse.UseVisualStyleBackColor = true;
            this.runPulse.Click += new System.EventHandler(this.runPulse_Click);
            // 
            // runBP
            // 
            this.runBP.Enabled = false;
            this.runBP.Location = new System.Drawing.Point(36, 89);
            this.runBP.Name = "runBP";
            this.runBP.Size = new System.Drawing.Size(90, 23);
            this.runBP.TabIndex = 8;
            this.runBP.Text = "Blood Pressure";
            this.runBP.UseVisualStyleBackColor = true;
            this.runBP.Click += new System.EventHandler(this.runBP_Click);
            // 
            // runTemp
            // 
            this.runTemp.Enabled = false;
            this.runTemp.Location = new System.Drawing.Point(170, 89);
            this.runTemp.Name = "runTemp";
            this.runTemp.Size = new System.Drawing.Size(90, 23);
            this.runTemp.TabIndex = 9;
            this.runTemp.Text = "Temperature";
            this.runTemp.UseVisualStyleBackColor = true;
            this.runTemp.Click += new System.EventHandler(this.runTemp_Click);
            // 
            // statusBox
            // 
            this.statusBox.Location = new System.Drawing.Point(12, 157);
            this.statusBox.Multiline = true;
            this.statusBox.Name = "statusBox";
            this.statusBox.ReadOnly = true;
            this.statusBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.statusBox.Size = new System.Drawing.Size(272, 70);
            this.statusBox.TabIndex = 10;
            // 
            // SerialController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 239);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.runTemp);
            this.Controls.Add(this.runBP);
            this.Controls.Add(this.runPulse);
            this.Controls.Add(this.runSteth);
            this.Controls.Add(this.comPortSelect);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SerialController";
            this.Text = "SerialController";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ComboBox comPortSelect;
        private System.Windows.Forms.Button runSteth;
        private System.Windows.Forms.Button runPulse;
        private System.Windows.Forms.Button runBP;
        private System.Windows.Forms.Button runTemp;
        private System.Windows.Forms.TextBox statusBox;
    }
}