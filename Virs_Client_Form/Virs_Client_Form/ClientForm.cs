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


namespace Virs_Client_Form
{
    public partial class clientFormMain : Form
    {
        public clientFormMain()
        {
            InitializeComponent();
        }

        // method called when import button is clicked that allows user to browse for directory containing virs files
        private void importFileButton_Click(object sender, EventArgs e)
        {
            string fileDirectory = null;
            string[] fileNames = new string[] { @"\steth4.txt", @"\pulse.txt", @"\bp.txt", @"\temp.txt" };   // file names to search for in directory
            bool[] fileCheck = new bool[4];    // bool array that corresponds to whether the fnames exist

            if (!this.debugCheckbox.Checked)
            {
                FolderBrowserDialog browseDirectory = new FolderBrowserDialog();
                DialogResult result = browseDirectory.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileDirectory = browseDirectory.SelectedPath;
                }
            }
            else fileDirectory = @"C:\Users\Trevor\Documents\GitHub\virs\Virs_Client_Form";

            for (int i = 0; i < fileNames.Length; i++)
            {
                if (File.Exists(fileDirectory + fileNames[i]))
                {
                    fileCheck[i] = true;   // if file exists at specified location, set fCheck value to true
                    fileNames[i] = (fileDirectory + fileNames[i]);    // then append directory name to file name in fNames
                }
            }

            checkFiles(fileCheck, fileNames);          

        }

        // method called to launch a dialogbox that displays if the expected files were found
        private void checkFiles(bool[] bArray, string[] sArray)
        {
            if (!bArray[0] & !bArray[1] & !bArray[2] & !bArray[3])
            {
                MessageBox.Show("No VIRS files found!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                FileBrowseCheck fbc = new FileBrowseCheck(bArray);
                fbc.ShowDialog();
                processFiles(bArray, sArray);
            }
        }

        // method used to call associated processing methods if relevant data is available
        private void processFiles(bool[] checks, string[] names)
        {
            Vitals clientData = new Vitals();
            this.debugLabel.Text = "Processing";
            if (checks[0])
                clientData.steth = processStethFile(names[0]);
            if (checks[1])
                clientData.pulse = processPulseFile(names[1]);
            if (checks[2])
                clientData.bp = processBPFile(names[2]);
            if (checks[3])
                clientData.temp = processTempFile(names[3]);

            populate(checks, clientData);
            this.debugLabel.Text = clientData.temp.ToString();
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
        private void populate(bool[] checks, Vitals clientData)
        {
            // populate pulse
            if (checks[1])
                this.dataViewList.Items[0].SubItems.Add(clientData.pulse.ToString());
            if (checks[2])
                this.dataViewList.Items[1].SubItems.Add(clientData.bp[0].ToString() + "/" + clientData.bp[1].ToString());
            if (checks[3])
                this.dataViewList.Items[2].SubItems.Add(clientData.temp.ToString());
        }



    }
}
