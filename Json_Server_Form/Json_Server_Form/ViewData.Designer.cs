namespace Json_Server_Form
{
    partial class ViewData
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Heart Rate");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Blood Pressure");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Temperature");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Age");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Weight");
            this.dataViewList = new System.Windows.Forms.ListView();
            this.dataKeyColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataValueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.playAudioButton = new System.Windows.Forms.Button();
            this.logTimeLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataViewList
            // 
            this.dataViewList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dataKeyColumn,
            this.dataValueColumn});
            this.dataViewList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.dataViewList.Location = new System.Drawing.Point(12, 101);
            this.dataViewList.Name = "dataViewList";
            this.dataViewList.Scrollable = false;
            this.dataViewList.Size = new System.Drawing.Size(300, 191);
            this.dataViewList.TabIndex = 0;
            this.dataViewList.UseCompatibleStateImageBehavior = false;
            this.dataViewList.View = System.Windows.Forms.View.Details;
            // 
            // dataKeyColumn
            // 
            this.dataKeyColumn.Text = "Metric";
            this.dataKeyColumn.Width = 125;
            // 
            // dataValueColumn
            // 
            this.dataValueColumn.Text = "Value";
            this.dataValueColumn.Width = 141;
            // 
            // playAudioButton
            // 
            this.playAudioButton.Location = new System.Drawing.Point(12, 298);
            this.playAudioButton.Name = "playAudioButton";
            this.playAudioButton.Size = new System.Drawing.Size(300, 42);
            this.playAudioButton.TabIndex = 1;
            this.playAudioButton.Text = "Play Stethoscope Audio";
            this.playAudioButton.UseVisualStyleBackColor = true;
            this.playAudioButton.Click += new System.EventHandler(this.playAudioButton_Click);
            // 
            // logTimeLabel
            // 
            this.logTimeLabel.AutoSize = true;
            this.logTimeLabel.Location = new System.Drawing.Point(13, 61);
            this.logTimeLabel.Name = "logTimeLabel";
            this.logTimeLabel.Size = new System.Drawing.Size(82, 20);
            this.logTimeLabel.TabIndex = 2;
            this.logTimeLabel.Text = "Log Time: ";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(17, 18);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(67, 20);
            this.nameLabel.TabIndex = 3;
            this.nameLabel.Text = "Patient: ";
            // 
            // ViewData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 355);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.logTimeLabel);
            this.Controls.Add(this.playAudioButton);
            this.Controls.Add(this.dataViewList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "ViewData";
            this.Text = "Client Data";
            this.Load += new System.EventHandler(this.ViewData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView dataViewList;
        private System.Windows.Forms.ColumnHeader dataKeyColumn;
        private System.Windows.Forms.ColumnHeader dataValueColumn;
        private System.Windows.Forms.Button playAudioButton;
        private System.Windows.Forms.Label logTimeLabel;
        private System.Windows.Forms.Label nameLabel;
    }
}