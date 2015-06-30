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
using System.Diagnostics;

namespace Json_Client_Form
{
    public partial class ApplicationInterface : Form
    {

        
        private Vitals clientData;
        private string jstringmaster;

        public ApplicationInterface()
        {
            InitializeComponent();
        }

        private delegate void SetTextCallback(string text);

        //openFile_Click event handler
        private void openFile_Click(object sender, EventArgs e)
        {
            Stream fileStream = null;
            String jstring = null;

            //open file dialog window with start directory defined
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"c:\\Users\Documents\School Work\Ecen 403";
            openFileDialog1.Filter = "json files (*.json)|*.json";
            openFileDialog1.RestoreDirectory = true;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((fileStream = openFileDialog1.OpenFile()) != null)
                    {
                        //get json string from file stream
                        jstring = Jlib.getJStringFromStream(fileStream);

                        jstringmaster = jstring;
                        
                        //create new Vitals object from jstring
                        clientData = new Vitals(jstring);
                        //populate form fields and chart with clientData
                        populate(clientData);          
                        //enable save button
                        this.saveFile.Enabled = true;
                        this.uploadFile.Enabled = true;
                        Debug.Write(jstring);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //saveFile_Click event handler writes object data to json structure
        private void saveFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "json files (*.json)|*.json";
            saveFileDialog1.RestoreDirectory = true;

            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    String fileName = saveFileDialog1.FileName;
                    Debug.Write("Filename: " + fileName);
                    if (clientData != null)
                    {
                        String json = Jlib.toJson(clientData);
                        File.WriteAllText(fileName, json);
                    }
                    else MessageBox.Show("Error: No data loaded to save!");                    
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }            
        }

        //uploadFile_Click event handler transmits client data to remote server
        private void uploadFile_Click(object sender, EventArgs e)
        {
            //create new input box form
            InputBox iBox = new InputBox("IP Address", "Enter IP Address");

            //assign dialogresult of textbox to ipentry
            string ipentry = iBox.ShowDialog() == DialogResult.OK ? iBox.inputText.Text : "";
            appendOutputDisplay(ipentry);

            //start client
            if (clientData != null) 
            {                
                string jstring = Jlib.toJson(clientData);
                AsyncClient client = new AsyncClient(this, jstring);
                appendOutputDisplay(jstring.Length.ToString());
                client.StartClient(ipentry);
            }          
        }

        public void appendOutputDisplay(string text)
        {
            if (this.outputDisplay.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(appendOutputDisplay);
                this.Invoke(d, new string[] { text });
            }
            else
            {
                this.outputDisplay.AppendText(text);
                this.outputDisplay.AppendText(System.Environment.NewLine);
            }
        }

        //functions to add data to the listView collection
        #region add functions
        private void populate(Vitals v)
        {
            addTemp(clientData.temp);
            addHR(clientData.heart);
            addBP(clientData.bp);
            addEKG(clientData.ekg);
            addTime(clientData.logTime);
        }
        private void addTemp(float temp)
        {
            this.listView1.Items[1].SubItems.Add(temp.ToString());
        }

        private void addHR(int hr)
        {
            this.listView1.Items[0].SubItems.Add(hr.ToString());
        }
        
        private void addBP(int[] bp)
        {
            this.listView1.Items[2].SubItems.Add(bp[0].ToString() + "/" + bp[1].ToString());
        }

        //function to add data points to chart series
        private void addEKG(double[] ekg)
        {
            foreach (double pt in ekg)
            {
                this.chart1.Series[0].Points.Add(pt);
            }
        }

        private void addTime(String time)
        {
            this.logTimeLabel.Text = "Log date: " + time;
        }
        #endregion

        




    }
}
