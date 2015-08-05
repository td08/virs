using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virs_Client_Form
{
    public partial class FileBrowseCheck : Form
    {
        public FileBrowseCheck(bool[] b)
        {
            InitializeComponent(b[0], b[1], b[2], b[3]);    // passed 'b' values indicate whether a file was found and to indicate status to user
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
