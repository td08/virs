using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace Json_Server_Form
{
    public partial class ViewData : Form
    {
        private string dateFormatString = "HHmm";   // string used to parse dateTime format for logTime
        private bool isPlaying = false;
        private SoundPlayer wavPlayer;

        public ViewData(string path)
        {
            InitializeComponent();
            // adds a blank SubItem field to each Item in dataViewList to allow populateForm method to set the text for that SubItem
            foreach (ListViewItem item in this.dataViewList.Items)
            {
                item.SubItems.Add("");
            }
            loadData(path);
        }

        private void loadData(string path)
        {
            // deserialize clientData from a stored json formatted virs file
            Vitals clientData = Jlib.deserializeJsonToVitals(path);

            // disable playAudioButton in case steth audio file is not present
            this.playAudioButton.Enabled = false;

            // add stethoscope audio
            if (clientData.fileChecks[0])
            {
                WavBuilder.generateWav(path, clientData.steth);
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

            // add logTime
            DateTime dt = DateTime.ParseExact(clientData.logTime, dateFormatString, null);
            this.logTimeLabel.Text = "Log Time: " + dt;

            // add name
            this.nameLabel.Text = "Patient: " + clientData.lastName + ", " + clientData.firstName;
        }

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

        private void ViewData_Load(object sender, EventArgs e)
        {
            
        }
    }
}
