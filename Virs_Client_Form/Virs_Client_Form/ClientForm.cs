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
using System.Media;

namespace Virs_Client_Form
{
    //TODO implement directory enumeration method detailed in howtolistdirectories.txt
    public partial class clientFormMain : Form
    {
        private Vitals clientData;          // currently loaded instance of clientData
        private UserSettings settings;    // ClientSettings form instance
        private SoundPlayer wavPlayer;      // SoundPlayer instance used to play stethoscope audio on form
        private string importPath;          // full file path that specifies where VIRS files are located within the import context
        private string importPathName;      // name of importPath directory; used for clientData logTime as directory name will be a datetime
        private string openPath;            // full file path that specifies where VIRS files are located within the open context
        private string dateFormatString = "yyyyMMddHHmm";   // string used to parse dateTime format for logTime
        private bool isPlaying;


        // constructor
        public clientFormMain()
        {
            InitializeComponent();
            settings = new UserSettings();    // instantiate new settings form
            importPath = null;        // initialize values  
            importPathName = null;    
            openPath = null;
            isPlaying = false;
        }

        // on clientForm load
        private void clientFormMain_Load(object sender, EventArgs e)
        {
            // adds a blank SubItem field to each Item in dataViewList to allow populateForm method to set the text for that SubItem
            foreach (ListViewItem item in this.dataViewList.Items)
            {
                item.SubItems.Add("");
            }
        }

        #region Toolstrip Menu Item Methods

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browseDirectory = new FolderBrowserDialog();
            browseDirectory.SelectedPath = settings.path;
            DialogResult result = browseDirectory.ShowDialog(); // browse for folder
            if (result == DialogResult.OK)
            {
                openPath = browseDirectory.SelectedPath;   // get full file path
                populateForm(openPath);
            }
        }

        // method called when import option is clicked that allows user to browse for SD carde directory containing VIRS files
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (settings.firstName != "" & settings.lastName != "" & settings.age != "" & settings.weight != "")
            {
                string[] fileNames = new string[] { "steth4.sts", "pulse.oxi", "bp.sph", "temp.temp" };   // VIRS file names to search for in directory
                bool[] fileCheck = new bool[4];    // bool array that corresponds to whether the fnames exist

                FolderBrowserDialog browseDirectory = new FolderBrowserDialog();
                DialogResult result = browseDirectory.ShowDialog(); // browse for folder
                if (result == DialogResult.OK)
                {
                    importPath = browseDirectory.SelectedPath;   // get full file path
                    importPathName = Path.GetFileName(importPath);    // get name of directory for dateTime

                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        if (File.Exists(Path.Combine(importPath, fileNames[i])))
                        {
                            fileCheck[i] = true;   // if file exists at specified location, set fileCheck value to true
                            fileNames[i] = (Path.Combine(importPath, fileNames[i]));    // then append directory name to file name in fNames
                        }
                    }

                    checkFiles(fileCheck, fileNames);
                }                
            }

            else
            {
                MessageBox.Show("Please ensure user settings pane has all fields completed before importing!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                settings.ShowDialog();
            }              
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ShowDialog();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialController sControl = new SerialController();
            sControl.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Button Click Methods

        private void playAudioButton_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                wavPlayer.Stop();
                this.playAudioButton.Text = "Play Stethoscope Audio";
                isPlaying = false;
            }
            else
            {
                wavPlayer.Play();
                this.playAudioButton.Text = "Stop Playing";
                isPlaying = true;
            }
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            wavPlayer.Stop();
        }

        #endregion

        // method called to launch a dialogbox that displays if the expected files were found
        private void checkFiles(bool[] checks, string[] names)
        {
            if (!checks[0] & !checks[1] & !checks[2] & !checks[3])
            {
                MessageBox.Show("No VIRS files found!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                FileBrowseCheck fbc = new FileBrowseCheck(checks);
                fbc.ShowDialog();
                processFiles(checks, names);
            }
        }

        // method used to call associated processing methods if relevant data is available
        private void processFiles(bool[] checks, string[] names)
        {
            clientData = new Vitals(checks, importPathName); // create new instance of rawClientData with file checks and available file paths

            string writePath = Path.Combine(settings.path, importPathName);

            // give rawClientData instance data from imported files
            if (checks[0])
                clientData.steth = processStethFile(names[0]);
            if (checks[1])
                clientData.pulse = processPulseFile(names[1]);
            if (checks[2])
                clientData.bp = processBPFile(names[2]);
            if (checks[3])
                clientData.temp = processTempFile(names[3]);

            clientData.firstName = settings.firstName;
            clientData.lastName = settings.lastName;
            clientData.age = settings.age;
            clientData.weight = settings.weight;

            try
            {
                // serialize imported clientData to JSON and write to file in clientSettings path
                Jlib.serializeVitalsToJson(writePath, clientData);
                // build filtered wav audio file using data from clientData.steth
                WavBuilder.generateWav(writePath, clientData.steth);
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //MessageBox.Show("Error importing files to disk!\nPlease check path directory in settings and try again!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            populateForm(writePath);
        }

        // method called to read and store raw stethoscope audio data from the specified file
        private int[] processStethFile(string path)
        {
            List<int> raw = new List<int>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string value = reader.ReadLine();
                    try
                    {
                        raw.Add(Int32.Parse(value));
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Error reading data!\nStethoscope audio data may be corrupted!", "Import Error");
                    }
                }
            }
            return raw.ToArray();
        }

        // method called to read and store pulse oximeter data from the specified file
        private int processPulseFile(string path)
        {
            int pulse = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string value = reader.ReadLine();
                    try
                    {
                        pulse = Int32.Parse(value);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Error reading data!\nPulse oximeter data may be corrupted!", "Import Error");
                    }
                }
            }
            return pulse;
        }

        // method called to read and store blood pressure data from the specified file
        private int[] processBPFile(string path)
        {
            int[] bp = new int[2];
            int i = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string value = reader.ReadLine();
                    try
                    {
                        bp[i++] = Int32.Parse(value);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Error reading data!\nBlood pressure data may be corrupted!", "Import Error");
                    }
                }
            }
            return bp;
        }

        // method called to read and store temperature data from the specified file
        private double processTempFile(string path)
        {
            double temp = 0.0;
            using (StreamReader reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    string value = reader.ReadLine();
                    try
                    {
                        temp = double.Parse(value);
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Error reading data!\nTemperature data may be corrupted!", "Import Error");
                    }
                }
            }
            return temp;
        }

        // method called to populate fields of dataViewList with current clientData object
        private void populateForm(string path)
        {
            // deserialize clientData from a stored json formatted virs file
            clientData = Jlib.deserializeJsonToVitals(path);

            // disable playAudioButton in case steth audio file is not present
            this.playAudioButton.Enabled = false;

            // add stethoscope audio
            if (clientData.fileChecks[0])
            {
                wavPlayer = new SoundPlayer(Path.Combine(path, "steth.wav"));
                this.playAudioButton.Enabled = true;
            }
            // add pulse
            if (clientData.fileChecks[1])
                this.dataViewList.Items[0].SubItems[1].Text = (clientData.pulse.ToString() + " bpm");
            // add bp
            if (clientData.fileChecks[2])
                this.dataViewList.Items[1].SubItems[1].Text = clientData.bp[0].ToString() + "/" + clientData.bp[1].ToString();
            // add temp
            if (clientData.fileChecks[3])
                this.dataViewList.Items[2].SubItems[1].Text = (clientData.temp.ToString() + " °F");

            // add age
            this.dataViewList.Items[3].SubItems[1].Text = clientData.age.ToString();

            // add weight
            this.dataViewList.Items[4].SubItems[1].Text = (clientData.weight.ToString() + " Lbs");

            DateTime dt = DateTime.ParseExact(clientData.logTime, dateFormatString, null);
            this.logTimeLabel.Text = "Log Time: " + dt;
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (settings.firstName != "" & settings.lastName != "" & settings.age != "" & settings.weight != "")
            {
                UploadForm uForm = new UploadForm(settings.path);
                uForm.ShowDialog();
            }

            else
            {
                MessageBox.Show("Please ensure user settings pane has all fields completed before uploading!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                settings.ShowDialog();
            }

        }




    }
}
