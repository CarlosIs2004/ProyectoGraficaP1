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
        private int currentLines = 0;
        private int elapsedSeconds = 0;
        private int animationDuration = 15; // Duración total en segundos o frames



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
            animationTimer.Interval = 200;
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


            float centerX = bufferBitmap.Width / 2f;
            float centerY = bufferBitmap.Height / 2f;

            
            if (currentLines > 15){ currentLines = 2;}// Reinicia la animación
            borderLines = new BorderLines(bufferGraphics, bufferBitmap.Width, bufferBitmap.Height, currentLines);
            borderLines.DrawBorderLines();
            currentLines++;

            // Rota la estrella y la dibuja en el buffer
            star.Rotate(2f, bufferGraphics, centerX, centerY);

            // Actualiza el PictureBox con el bitmap actualizado
            canvas.Image = bufferBitmap;
        }

        public void RenderFrame(int frameNumber)
        {
            SolidBrush fadeBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0));

            bufferGraphics.FillRectangle(fadeBrush, 0, 0, bufferBitmap.Width, bufferBitmap.Height);


            float centerX = bufferBitmap.Width / 2f;
            float centerY = bufferBitmap.Height / 2f;

            if (currentLines > 15) { currentLines = 2; }// Reinicia la animación
            borderLines = new BorderLines(bufferGraphics, bufferBitmap.Width, bufferBitmap.Height, frameNumber);
            borderLines.DrawBorderLines();

            star.SetAngle(frameNumber * 2f); 
            star.Rotate(0f, bufferGraphics, centerX, centerY);
            canvas.Image = bufferBitmap;




        }

       



    }
}
