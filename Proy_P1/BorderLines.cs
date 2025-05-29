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
        private float width = 0f;
        private float height = 0f;
        Graphics g;
        float stepX = 0f;
        float stepY = 0f;

        public BorderLines(Graphics g, float width, float height, int numLines) { 
            this.g = g;  
            this.width = width; 
            this.height = height;
            this.numLines = numLines;
        }
        public void DrawBorderLines()
        {
            stepX = width / numLines;
            stepY = height / numLines;

            if (numLines <= 1)
                return;

            Pen[] pens = {
                new Pen(Color.LightGreen, 1),
                new Pen(Color.LightSkyBlue, 1),
                new Pen(Color.Orange, 1),
                new Pen(Color.Violet, 1)
            };

            for (int i = 1; i < numLines; i++){
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
