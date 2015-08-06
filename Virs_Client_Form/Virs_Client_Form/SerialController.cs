using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

namespace Virs_Client_Form
{
    public partial class SerialController : Form
    {
        private SerialSettings settings;
        private SerialPort comPort;
        private delegate void setTextCallback(string text);

        public SerialController()
        {
            InitializeComponent();
            settings = new SerialSettings();
            comPort = new SerialPort();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settings.ShowDialog();
        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            
        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            if (comPort.IsOpen)
            {
                comPort.Close();
                appendStatusBox(comPort.PortName + " closed!" + System.Environment.NewLine);
            }

            string[] comPorts = null;

            comPorts = SerialPort.GetPortNames();   // returns an array of serial port names on the machine
            
            this.comPortSelect.Items.Clear();           // remove all previous entries in comPortSelect
            this.comPortSelect.Text = "";               // clear comPortSelect

            foreach (string s in comPorts)
            {
                comPortSelect.Items.Add(s);
            }

            if (comPorts.Length < 1)
            {
                this.comPortSelect.Enabled = false;
                MessageBox.Show("No serial ports detected!\nCheck device connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.comPortSelect.Text = this.comPortSelect.Items[0].ToString();   // set comPortSelect to first detected port
            this.comPortSelect.Enabled = true;
            this.connectButton.Enabled = true;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            appendStatusBox("Connecting..." + System.Environment.NewLine);
            comPort.PortName = comPortSelect.Text;
            comPort.BaudRate = Convert.ToInt32(settings.baud);
            comPort.DataBits = Convert.ToInt16(settings.dataBits);
            comPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), settings.stopBits);
            comPort.Parity = (Parity)Enum.Parse(typeof(Parity), settings.parity);
            comPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), settings.flow);
            comPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(dataReceived);

            try
            {
                comPort.Open();
                appendStatusBox("Connected on " + comPort.PortName + "!" + System.Environment.NewLine);
                connectedButtonsEnable();         
            }

            catch (System.IO.IOException i)
            {
                MessageBox.Show("Error connecting to device!\nCheck device connection!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                appendStatusBox("Connection failed!" + System.Environment.NewLine);
            }                        
        }

        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            appendStatusBox(comPort.ReadExisting());
        }

        private void runSteth_Click(object sender, EventArgs e)
        {
            comPort.Write("help\r\n");
        }
        
        private void runPulse_Click(object sender, EventArgs e)
        {

        }        

        private void runBP_Click(object sender, EventArgs e)
        {

        }

        private void runTemp_Click(object sender, EventArgs e)
        {

        }

        private void appendStatusBox(string text)
        {
            if (statusBox.InvokeRequired)
                this.Invoke(new setTextCallback(appendStatusBox), new string[] { text });
            else
                this.statusBox.AppendText(text);
        }

        private void connectedButtonsEnable()
        {
            this.connectButton.Enabled = false;
            this.runSteth.Enabled = true;
            this.runPulse.Enabled = true;
            this.runBP.Enabled = true;
            this.runTemp.Enabled = true;
        }
    }
}
