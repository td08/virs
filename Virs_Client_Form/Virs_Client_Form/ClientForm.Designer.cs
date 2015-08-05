namespace Virs_Client_Form
{
    partial class clientFormMain
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
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Heart Rate");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Blood Pressure");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Temperature");
            this.importFileButton = new System.Windows.Forms.Button();
            this.saveAsButton = new System.Windows.Forms.Button();
            this.dataViewList = new System.Windows.Forms.ListView();
            this.dataKeyColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataValueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.debugLabel = new System.Windows.Forms.Label();
            this.debugCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // importFileButton
            // 
            this.importFileButton.Location = new System.Drawing.Point(67, 41);
            this.importFileButton.Name = "importFileButton";
            this.importFileButton.Size = new System.Drawing.Size(103, 46);
            this.importFileButton.TabIndex = 0;
            this.importFileButton.Text = "Import Files";
            this.importFileButton.UseVisualStyleBackColor = true;
            this.importFileButton.Click += new System.EventHandler(this.importFileButton_Click);
            // 
            // saveAsButton
            // 
            this.saveAsButton.Enabled = false;
            this.saveAsButton.Location = new System.Drawing.Point(67, 103);
            this.saveAsButton.Name = "saveAsButton";
            this.saveAsButton.Size = new System.Drawing.Size(103, 39);
            this.saveAsButton.TabIndex = 1;
            this.saveAsButton.Text = "Save As...";
            this.saveAsButton.UseVisualStyleBackColor = true;
            // 
            // dataViewList
            // 
            this.dataViewList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dataKeyColumn,
            this.dataValueColumn});
            this.dataViewList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.dataViewList.Location = new System.Drawing.Point(270, 41);
            this.dataViewList.Name = "dataViewList";
            this.dataViewList.Scrollable = false;
            this.dataViewList.Size = new System.Drawing.Size(264, 140);
            this.dataViewList.TabIndex = 2;
            this.dataViewList.UseCompatibleStateImageBehavior = false;
            this.dataViewList.View = System.Windows.Forms.View.Details;
            // 
            // dataKeyColumn
            // 
            this.dataKeyColumn.Text = "Metric";
            this.dataKeyColumn.Width = 121;
            // 
            // dataValueColumn
            // 
            this.dataValueColumn.Text = "Value";
            // 
            // debugLabel
            // 
            this.debugLabel.AutoSize = true;
            this.debugLabel.Location = new System.Drawing.Point(270, 213);
            this.debugLabel.Name = "debugLabel";
            this.debugLabel.Size = new System.Drawing.Size(80, 20);
            this.debugLabel.TabIndex = 3;
            this.debugLabel.Text = "Debugger";
            // 
            // debugCheckbox
            // 
            this.debugCheckbox.AutoSize = true;
            this.debugCheckbox.Checked = true;
            this.debugCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.debugCheckbox.Location = new System.Drawing.Point(39, 56);
            this.debugCheckbox.Name = "debugCheckbox";
            this.debugCheckbox.Size = new System.Drawing.Size(22, 21);
            this.debugCheckbox.TabIndex = 4;
            this.debugCheckbox.UseVisualStyleBackColor = true;
            // 
            // clientFormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 356);
            this.Controls.Add(this.debugCheckbox);
            this.Controls.Add(this.debugLabel);
            this.Controls.Add(this.dataViewList);
            this.Controls.Add(this.saveAsButton);
            this.Controls.Add(this.importFileButton);
            this.Name = "clientFormMain";
            this.Text = "VIRS Client Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button importFileButton;
        private System.Windows.Forms.Button saveAsButton;
        private System.Windows.Forms.ListView dataViewList;
        private System.Windows.Forms.ColumnHeader dataKeyColumn;
        private System.Windows.Forms.ColumnHeader dataValueColumn;
        private System.Windows.Forms.Label debugLabel;
        private System.Windows.Forms.CheckBox debugCheckbox;
    }
}

