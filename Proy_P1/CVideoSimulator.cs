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
        public PictureBox canvas;
        public Timer animationTimer;
        public CStar star;
        public BorderLines borderLines;
        public Bitmap bufferBitmap;
        public Graphics bufferGraphics;
        public const float outerRadius = 1.75f;
        public const float innerRadius = 0.95f;
        public const int lineCount = 50;

        public CVideoSimulator(PictureBox pictureBox)
        {
            canvas = pictureBox;
            star = new CStar();
            star.ReadData(outerRadius, innerRadius, 8);
            

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

        public void Stop() {
            animationTimer.Stop();
        }

       



        public void OnAnimationTick(object sender, EventArgs e)
        {
            // Pinta un rectángulo negro semi-transparente para crear el efecto rastro
            SolidBrush fadeBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0));

            bufferGraphics.FillRectangle(fadeBrush, 0, 0, bufferBitmap.Width, bufferBitmap.Height);


            // Dibujar líneas del borde en el buffer para que aparezcan siempre
            borderLines = new BorderLines(bufferGraphics, bufferBitmap.Width, bufferBitmap.Height, lineCount);
            borderLines.DrawBorderLines();

            float centerX = bufferBitmap.Width / 2f;
            float centerY = bufferBitmap.Height / 2f;

            // Rota la estrella y la dibuja en el buffer
            star.Rotate(2f, bufferGraphics, centerX, centerY);

            // Actualiza el PictureBox con el bitmap actualizado
            canvas.Image = bufferBitmap;
        }

        public void RenderFrame(int frameNumber)
        {
            SolidBrush fadeBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0));

            bufferGraphics.FillRectangle(fadeBrush, 0, 0, bufferBitmap.Width, bufferBitmap.Height);

            borderLines = new BorderLines(bufferGraphics, bufferBitmap.Width, bufferBitmap.Height, lineCount);
            borderLines.DrawBorderLines();

            float centerX = bufferBitmap.Width / 2f;
            float centerY = bufferBitmap.Height / 2f;
            // Lógica para pintar la estrella en el frame dado
            star.SetAngle(frameNumber * 2f);  // Necesita método SetAngle
            star.Rotate(0f, bufferGraphics, centerX, centerY);
            canvas.Image = bufferBitmap;
        }

    }
}
