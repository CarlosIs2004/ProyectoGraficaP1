using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proy_P1
{
    internal class BorderLines
    {
        private int numLines = 0;
        private float width = 0;
        private float height = 0;
        Graphics g;

        public BorderLines(Graphics g, float width, float height, int numLines) { 
            this.g = g;  
            this.width = width; 
            this.height = height;
            this.numLines = numLines;
        }
        public void DrawBorderLines()
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
