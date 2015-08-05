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

        public ServerForm()
        {
            InitializeComponent();
        }

        //Delegates

        private delegate void setTextCallback(string text);
        private delegate void startEnable(); 

        //Button Controls

        private void startButton_Click(object sender, EventArgs e)
        {
            // enable and disable respective controls
            this.startButton.Enabled = false;
            this.localStartButton.Enabled = false;
            this.stopButton.Enabled = true;

            helper = new ServerHelper(this, false);    // start without localhost

            //start server without localhost
            //AsyncServer server = new AsyncServer(this);
            //server.StartListening(false);
        }

        private void localStartButton_Click(object sender, EventArgs e)
        {
            // enable and disable respective controls
            this.localStartButton.Enabled = false;
            this.startButton.Enabled = false;
            this.stopButton.Enabled = true;

            helper = new ServerHelper(this, true);    // start with localhost

            //start server using localhost
            //AsyncServer server = new AsyncServer(this);
            //server.StartListening(true);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            helper.stopServer.Set();
            setIpLabel(null);
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            Stream fileStream = null;
            String jstring = null;
            Vitals data;

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
                        //create new Vitals object from jstring
                        data = new Vitals(jstring);
                        //display data to output
                        appendOutputDisplay(displayData(data));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        // Button controls

        public void enableStartButtons()
        {
            if (this.startButton.InvokeRequired | this.localStartButton.InvokeRequired)
            {
                startEnable s = new startEnable(enableStartButtons);
                this.Invoke(s);
            }
            else
            {
                if (!startButton.Enabled)
                    this.startButton.Enabled = true;
                if (!localStartButton.Enabled)
                    this.localStartButton.Enabled = true;
            }            
        }

        private string displayData(Vitals data)
        {
            appendOutputDisplay("\r\nVitals Data\r\n------------------");
            return "Temp: " + data.temp + System.Environment.NewLine + "Heart: " + data.heart 
                + System.Environment.NewLine + "BP: " + data.bp[0] + "/" + data.bp[1];
        }

        

        
    }
}
