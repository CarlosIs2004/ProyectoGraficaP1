using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proy_P1
{
    public partial class Reproductor : Form
    {
        private CVideoSimulator videoSim;
        public Reproductor()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            videoSim = new CVideoSimulator(pictureBox1);
            videoSim.Start();

        }
    }
}
