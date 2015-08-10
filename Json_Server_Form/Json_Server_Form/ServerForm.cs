using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Json_Server_Form
{
    public partial class ServerForm : Form
    {
        private ServerHelper helper;    // local ServerHelper instance variable
        public ServerSettings settings;

        public ServerForm()
        {
            InitializeComponent();
            settings = new ServerSettings();
        }

        //Delegates

        private delegate void setTextCallback(string text);
        private delegate void startEnable(); 

        //Button Controls

        private void startButton_Click(object sender, EventArgs e)
        {
            if (settings.path != "")
            {
                // enable and disable respective controls
                this.startButton.Enabled = false;
                this.stopButton.Enabled = true;

                helper = new ServerHelper(this, settings.localHost);    // start without localhost
            }

            else
            {
                MessageBox.Show("Please ensure a file directory is selected in the settings pane before starting server!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                settings.ShowDialog();
            }
            
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.stopButton.Enabled = false;
            helper.stopServer.Set();
            setIpLabel("");
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            string openPath;
            FolderBrowserDialog browseDirectory = new FolderBrowserDialog();
            browseDirectory.SelectedPath = settings.path;
            DialogResult result = browseDirectory.ShowDialog(); // browse for folder
            if (result == DialogResult.OK)
            {
                openPath = browseDirectory.SelectedPath;   // get full file path
                if (File.Exists(Path.Combine(openPath, "data.virs")))
                {
                    ViewData viewer = new ViewData(openPath);
                    viewer.ShowDialog();
                }
                else MessageBox.Show("Error!\nNo VIRS files found in selected directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        public void enableStartButtons()
        {
            if (this.startButton.InvokeRequired)
            {
                startEnable s = new startEnable(enableStartButtons);
                this.Invoke(s);
            }
            else
            {
                if (!startButton.Enabled)
                    this.startButton.Enabled = true;
            }
        }

        // ipLabel Controls

        public void setIpLabel(string text)
        {
            this.ipLabel.Text = "IP Address: " + text;
        }

        // outputDisplay Controls

        public void appendOutputDisplay(string text)
        {
            if (this.outputDisplay.InvokeRequired)
            {
                setTextCallback d = new setTextCallback(appendOutputDisplay);
                this.Invoke(d, new string[] { text });
            }
            else
            {
                this.outputDisplay.AppendText(text);
                this.outputDisplay.AppendText(System.Environment.NewLine);
            }           
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startButton.Enabled)
                settings.ShowDialog();
            else MessageBox.Show("Please stop server before attempting to edit settings!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }     
    }
}
