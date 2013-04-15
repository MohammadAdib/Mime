using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mime
{
    public partial class GUI : Form
    {
        private Mime mime;

        public GUI()
        {
            InitializeComponent();
            mime = new Mime();
            mime.Start();
        }

        private void GUI_Load(object sender, EventArgs e)
        {

        }

        private void GUIClosed(object sender, FormClosedEventArgs e)
        {            
            File.WriteAllLines(@"C:\Users\Mohammad\Dropbox\Transfer\Text\recording.mime", mime.GetRecording());
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            new Player().play();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (mime.getStatus())
            {
                mime.Stop();
            }
            else
            {
                mime.Start();
            }
        }
    }
}
