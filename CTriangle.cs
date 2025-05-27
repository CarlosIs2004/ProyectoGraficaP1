using System;
using System.Drawing;

namespace Proy_P1
{
    internal class CTriangle
    {
        private float size; // Tamaño del triángulo
        private float angle = 0f; // Ángulo de rotación

        public CTriangle(float initialSize)
        {
            size = initialSize;
        }

        public void SetSize(float newSize)
        {
            size = newSize;
        }

        public void SetAngle(float newAngle)
        {
            angle = newAngle;
        }

        public void Rotate(float angleDelta, Graphics g, float centerX, float centerY)
        {
            angle += angleDelta;

            if (size <= 0)
                return;

            // Calcular los puntos del triángulo equilátero
            PointF[] trianglePoints = new PointF[3];
            double angleRad = angle * Math.PI / 180.0;

            // Los ángulos de los vértices del triángulo
            for (int i = 0; i < 3; i++)
            {
                double vertexAngle = angleRad + i * 2 * Math.PI / 3; // Distribución en 120 grados

                float x = (float)(size * Math.Cos(vertexAngle));
                float y = (float)(size * Math.Sin(vertexAngle));

                // Rotar los puntos alrededor del centro
                trianglePoints[i] = new PointF(centerX + x, centerY + y);
            }

            // Dibujar el triángulo
            Pen trianglePen = new Pen(Color.Red, 3);
            g.DrawPolygon(trianglePen, trianglePoints);
        }
    }
}
