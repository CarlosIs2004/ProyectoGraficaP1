using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proy_P1
{
    public partial class Reproductor : Form
    {
        private CVideoSimulator videoSim;
        Timer timer = new Timer();
        int frame = 0;
        bool isPlaying = false;
        int maxFrames = 33; // Por ejemplo, duración simulada


        public Reproductor()
        {
            InitializeComponent();
            trackBar.Minimum = 0;
            trackBar.Maximum = maxFrames;
            trackBar.Value = 0;

            timer.Interval = 100; // ~30 FPS
            timer.Tick += (s, e) =>
            {
                if (isPlaying && frame < maxFrames)
                {
                    frame++;
                    trackBar.Value = frame;  // ← Avanza la barra
                    videoSim.RenderFrame(frame);
                    if (labelTiempo != null)
                        labelTiempo.Text = $"Tiempo: {frame / 30.0:F2} s";
                }
                else if (frame >= maxFrames)
                {
                    videoSim.Stop();
                    isPlaying = false;
                    btnPlay.Text = "▶️ Reproducir";
                }

            };

         

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            videoSim = new CVideoSimulator(pictureBox1);

        }

        
        private void btnPlay_Click_1(object sender, EventArgs e)
        {
            if (!isPlaying)
            {
                videoSim.Start();
                timer.Start();
                isPlaying = true;
                btnPlay.Text = "⏸️ Pausar";
            }
            else
            {
                videoSim.Stop();
                timer.Stop();
                isPlaying = false;
                btnPlay.Text = "▶️ Reanudar";
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            frame = trackBar.Value;
            videoSim.RenderFrame(frame);
            if (labelTiempo != null)
                labelTiempo.Text = $"Tiempo: {frame / 30.0:F2} s";

        }


    }
}
