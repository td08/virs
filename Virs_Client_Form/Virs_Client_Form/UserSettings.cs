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
    public partial class UserSettings : Form
    {
        public string path { get { return this.pathSelect.Text; } }
        public string firstName { get {return this.firstNameBox.Text; } }
        public string lastName { get { return this.lastNameBox.Text; } }
        public string age { get { return this.ageBox.Text; } }
        public string weight { get { return this.weightBox.Text; } }

        private bool firstLoad = true;
        private string settingsPath = Path.Combine(Application.StartupPath, "UserSettings.xml");

        public UserSettings()
        {
            InitializeComponent();
            if (firstLoad)
            {
                loadSettings();
                firstLoad = false;
            }
        }

        private void ClientSettings_Load(object sender, EventArgs e)
        {
            
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            DialogResult result = browser.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (!(this.pathSelect.Items.IndexOf((object)browser.SelectedPath) > -1))    // check if selection already contains item
                    this.pathSelect.Items.Add(browser.SelectedPath);
                this.pathSelect.Text = browser.SelectedPath;
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
                        XmlSerializer xs = new XmlSerializer(typeof(XmlSettings));
                        XmlSettings xsFile = xs.Deserialize(fs) as XmlSettings;
                        foreach (string s in xsFile.paths)
                        {
                            this.pathSelect.Items.Add(s);
                        }
                        this.pathSelect.Text = this.pathSelect.Items[xsFile.lastIndex].ToString();
                        this.firstNameBox.Text = xsFile.firstName;
                        this.lastNameBox.Text = xsFile.lastName;
                        this.ageBox.Text = xsFile.age;
                        this.weightBox.Text = xsFile.weight;
                    }
                }

                // else create default settings
                else
                {
                    this.pathSelect.Items.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "VIRS Files"));
                    this.pathSelect.Items.Add(Path.Combine(Application.StartupPath, "VIRS Files"));
                    this.pathSelect.Text = this.pathSelect.Items[1].ToString();     // select default path value
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error loading previous settings!\nPlease enter local file directory in settings menu!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            XmlSettings xsFile = new XmlSettings();

            foreach (Object o in this.pathSelect.Items)
            {
                xsFile.paths.Add(o.ToString());     // add all items in pathSelect to paths list
            }            

            // add user attributes to xsfile
            xsFile.firstName = this.firstNameBox.Text;
            xsFile.lastName = this.lastNameBox.Text;
            xsFile.age = this.ageBox.Text;
            xsFile.weight = this.weightBox.Text;
            if (this.pathSelect.SelectedItem != null)
                xsFile.lastIndex = this.pathSelect.Items.IndexOf(this.pathSelect.SelectedItem); // get index of currently selected item in pathSelect

            if (xsFile.paths.ToArray().Length > 0 & xsFile.firstName != "" & xsFile.lastName != "" & xsFile.age != "" & xsFile.weight != "")
            {
                try
                {
                    using (FileStream fs = File.Create(settingsPath))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(XmlSettings));
                        xs.Serialize(fs, xsFile);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error writing settings file!\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Close();
            }

            else MessageBox.Show("Please enter data into all fields!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            this.firstNameBox.Text = "";
            this.lastNameBox.Text = "";
            this.ageBox.Text = "";
            this.weightBox.Text = "";
            this.pathSelect.Items.Clear();
            this.pathSelect.Text = "";
            File.Delete(settingsPath);
        }        
    }

    [Serializable]
    public class XmlSettings
    {
        public List<string> paths;
        public string firstName, lastName, age, weight;
        public int lastIndex;

        public XmlSettings()
        {
            paths = new List<string>();
            firstName = null;
            lastName = null;
            age = null;
            weight = null;
            lastIndex = 0;
        }
    }

}
