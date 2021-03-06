﻿namespace Json_Client_Form
{
    partial class ApplicationInterface
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
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Temperature");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Blood Pressure");
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.openFile = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.saveFile = new System.Windows.Forms.Button();
            this.logTimeLabel = new System.Windows.Forms.Label();
            this.uploadFile = new System.Windows.Forms.Button();
            this.outputDisplay = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(40, 12);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(119, 46);
            this.openFile.TabIndex = 0;
            this.openFile.Text = "Open File";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.Location = new System.Drawing.Point(199, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(230, 132);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Metric";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Value";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(40, 177);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(662, 300);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // saveFile
            // 
            this.saveFile.Enabled = false;
            this.saveFile.Location = new System.Drawing.Point(40, 94);
            this.saveFile.Name = "saveFile";
            this.saveFile.Size = new System.Drawing.Size(119, 50);
            this.saveFile.TabIndex = 4;
            this.saveFile.Text = "Save To File";
            this.saveFile.UseVisualStyleBackColor = true;
            this.saveFile.Click += new System.EventHandler(this.saveFile_Click);
            // 
            // logTimeLabel
            // 
            this.logTimeLabel.AutoSize = true;
            this.logTimeLabel.Location = new System.Drawing.Point(198, 154);
            this.logTimeLabel.Name = "logTimeLabel";
            this.logTimeLabel.Size = new System.Drawing.Size(0, 20);
            this.logTimeLabel.TabIndex = 5;
            // 
            // uploadFile
            // 
            this.uploadFile.Enabled = false;
            this.uploadFile.Location = new System.Drawing.Point(587, 12);
            this.uploadFile.Name = "uploadFile";
            this.uploadFile.Size = new System.Drawing.Size(115, 46);
            this.uploadFile.TabIndex = 6;
            this.uploadFile.Text = "Upload File";
            this.uploadFile.UseVisualStyleBackColor = true;
            this.uploadFile.Click += new System.EventHandler(this.uploadFile_Click);
            // 
            // outputDisplay
            // 
            this.outputDisplay.Location = new System.Drawing.Point(456, 64);
            this.outputDisplay.Multiline = true;
            this.outputDisplay.Name = "outputDisplay";
            this.outputDisplay.ReadOnly = true;
            this.outputDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputDisplay.Size = new System.Drawing.Size(246, 80);
            this.outputDisplay.TabIndex = 7;
            // 
            // ApplicationInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 501);
            this.Controls.Add(this.outputDisplay);
            this.Controls.Add(this.uploadFile);
            this.Controls.Add(this.logTimeLabel);
            this.Controls.Add(this.saveFile);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.openFile);
            this.Name = "ApplicationInterface";
            this.Text = "Application Interface";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion




        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button saveFile;
        private System.Windows.Forms.Label logTimeLabel;
        private System.Windows.Forms.Button uploadFile;
        private System.Windows.Forms.TextBox outputDisplay;
    }
}