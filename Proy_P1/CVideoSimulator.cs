using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proy_P1
{
    internal class CVideoSimulator
    {
        private PictureBox canvas;
        private Timer animationTimer;
        private CStar star;
        private Bitmap bufferBitmap;
        private Graphics bufferGraphics;
        private const float outerRadius = 1.75f;
        private const float innerRadius = 0.95f;
        private const int lineCount = 40;

        public CVideoSimulator(PictureBox pictureBox)
        {
            canvas = pictureBox;
            star = new CStar();
            star.ReadData(outerRadius, innerRadius);
            star.SetPoints(8); //Puntas

            // Crear el buffer del tamaño del PictureBox
            bufferBitmap = new Bitmap(canvas.Width, canvas.Height);
            bufferGraphics = Graphics.FromImage(bufferBitmap);
            bufferGraphics.Clear(Color.Black);

            animationTimer = new Timer();
            animationTimer.Interval = 150;
            animationTimer.Tick += OnAnimationTick;
            
        }

        public void Start()
        {
            animationTimer.Start();
        }

        public void Stop()
        {
            animationTimer.Stop();
        }

        private void OnAnimationTick(object sender, EventArgs e)
        {
            // Pinta un rectángulo negro semi-transparente para crear el efecto rastro
            using (SolidBrush fadeBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                bufferGraphics.FillRectangle(fadeBrush, 0, 0, bufferBitmap.Width, bufferBitmap.Height);
            }

            // Dibujar líneas del borde en el buffer para que aparezcan siempre
            DrawBorderLines(bufferGraphics, bufferBitmap.Width, bufferBitmap.Height, lineCount);

            float centerX = bufferBitmap.Width / 2f;
            float centerY = bufferBitmap.Height / 2f;

            // Rota la estrella y la dibuja en el buffer
            star.Rotate(2f, bufferGraphics, centerX, centerY);

            // Actualiza el PictureBox con el bitmap actualizado
            canvas.Image = bufferBitmap;
        }


        private void DrawFrame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Crea un pincel negro transparente para efecto rastro
            using (SolidBrush fadeBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
            {
                g.FillRectangle(fadeBrush, 0, 0, canvas.Width, canvas.Height);
            }

            // Dibujar líneas del borde desde CStar
            DrawBorderLines(g, canvas.Width, canvas.Height, lineCount);

            // Coordenadas del centro
            float centerX = canvas.Width / 2f;
            float centerY = canvas.Height / 2f;

            // Rotar y dibujar estrella
            star.Rotate(2f, g, centerX, centerY);
        }

        public void DrawBorderLines(Graphics g, float width, float height, int numLines)
        {
            if (numLines <= 1)
                return;

            Pen[] pens = {
                new Pen(Color.LightGreen, 1),
                new Pen(Color.LightSkyBlue, 1),
                new Pen(Color.Orange, 1),
                new Pen(Color.Violet, 1)
            };

            float stepX = width / numLines;
            float stepY = height / numLines;

            for (int i = 1; i < numLines; i++)
            {
                // Arriba → derecha
                g.DrawLine(pens[0], i * stepX, 0, width, i * stepY);

                // Derecha → abajo
                g.DrawLine(pens[1], width, i * stepY, width - i * stepX, height);

                // Abajo → izquierda
                g.DrawLine(pens[2], width - i * stepX, height, 0, height - i * stepY);

                // Izquierda → arriba
                g.DrawLine(pens[3], 0, height - i * stepY, i * stepX, 0);
            }
        }
    }
}
