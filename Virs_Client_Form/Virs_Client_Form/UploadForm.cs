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
using System.Xml.Serialization;

namespace Virs_Client_Form
{
    public partial class UploadForm : Form
    {
        public string ip { get { return this.ipEntryBox.Text; } }
        public string port { get { return this.portEntryBox.Text; } }

        private string clientDataAsJson;
        private string startPath;
        private string openPath;
        private string settingsPath = Path.Combine(Application.StartupPath, "ConnectionSettings.xml");
        private bool firstLoad = true;

        private delegate void setTextCallback(string text);

        public UploadForm(string path)
        {
            InitializeComponent();
            this.startPath = path;
            if (firstLoad)
            {
                loadSettings();
                firstLoad = false;
            }
        }

        private void loadSettings()
        {
            try
            {
                // load previous settings if UserSettings.xml exists
                if (File.Exists(settingsPath))
                {
                    using (FileStream fs = File.Open(settingsPath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(XmlConnections));
                        XmlConnections xcFile = xs.Deserialize(fs) as XmlConnections;
                        foreach (string s in xcFile.connections)
                        {
                            this.ipSelect.Items.Add(s);
                        }
                        this.ipSelect.Text = this.ipSelect.Items[xcFile.lastIndex].ToString();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error loading previous settings!\nPlease enter local file directory in settings menu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void ipSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                this.ipSelect.Items.Add(this.ipSelect.Text);
        }

        private void UploadForm_Load(object sender, EventArgs e)
        {
            
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (ip != "" & port != "")
            {
                string ipPort = ip + ":" + port;
                if (!(this.ipSelect.Items.IndexOf((object)ipPort) > -1))    // check if selection already contains item
                    this.ipSelect.Items.Add(ipPort);
                this.ipSelect.Text = ipPort;
                AesClient.upload(this, ip, port, clientDataAsJson, openPath);
            }
            else
                MessageBox.Show("Please complete IP address and port fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            
        }

        public void appendStatus(string text)
        {
            if (this.statusBox.InvokeRequired)
            {
                setTextCallback d = new setTextCallback(appendStatus);
                this.Invoke(d, new string[] { text });
            }
            else
            {
                this.statusBox.AppendText(text);
                this.statusBox.AppendText(System.Environment.NewLine);
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseDirectory = new FolderBrowserDialog();
            browseDirectory.SelectedPath = startPath;
            DialogResult result = browseDirectory.ShowDialog(); // browse for folder
            if (result == DialogResult.OK)
            {
                openPath = browseDirectory.SelectedPath;   // get full file path
                this.selectedFileLabel.Text = "Selected File: " + Path.GetFileName(openPath);
                try
                {
                    clientDataAsJson = Jlib.getJStringFromFile(openPath);
                    this.connectButton.Enabled = true;
                }

                catch (System.IO.FileNotFoundException fnfe)
                {
                    MessageBox.Show("Error no VIRS file found!\nPlease check path directory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UploadForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            XmlConnections xcFile = new XmlConnections();

            foreach (Object o in this.ipSelect.Items)
            {
                xcFile.connections.Add(o.ToString());     // add all items in ipSelect to paths list
            }

            if (this.ipSelect.SelectedItem != null)
                xcFile.lastIndex = this.ipSelect.Items.IndexOf(this.ipSelect.SelectedItem); // get index of currently selected item in ipSelect

            if (xcFile.connections.ToArray().Length > 0)
            {
                try
                {
                    using (FileStream fs = File.Create(settingsPath))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(XmlConnections));
                        xs.Serialize(fs, xcFile);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error writing settings file!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ipSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ipPort = ipSelect.Text;
            int colonIndex = ipPort.IndexOf(":");
            ipEntryBox.Text = ipPort.Substring(0, colonIndex);
            portEntryBox.Text = ipPort.Substring((colonIndex + 1), (ipPort.Length - colonIndex - 1));
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.ipEntryBox.Text = "";
            this.portEntryBox.Text = "";
            this.ipSelect.Items.Clear();
            this.ipSelect.Text = "";
            File.Delete(settingsPath);
        }
    }

    [Serializable]
    public class XmlConnections
    {
        public List<string> connections;
        public int lastIndex;

        public XmlConnections()
        {
            connections = new List<string>();
            lastIndex = 0;
        }
    }
}
