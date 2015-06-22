using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Json_Server_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            //start server without localhost
            AsyncServer server = new AsyncServer(this);
            server.StartListening(false);
        }

        private void localStartButton_Click(object sender, EventArgs e)
        {
            //start server using localhost
            AsyncServer server = new AsyncServer(this);
            server.StartListening(true);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {

        }

        public void setIpLabel(string text)
        {
            this.ipLabel.Text = "IP Address: " + text;
        }

        public void appendReceivedData(string text)
        {
            this.outputDisplay.AppendText(text);
        }
    }
}
