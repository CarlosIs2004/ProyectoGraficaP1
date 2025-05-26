using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proy_P1
{
    internal class CStar
    {
        private float sRadiusOuter; //Radio de las puntas
        private float sRadiusInner; //Radio entre puntas
        private float mAngle = 0f;
        private const float SF = 10;
        private int numPoints = 5; //Puntas por defecto

        public void ReadData(float outer, float inner)
        {
            sRadiusOuter = outer;
            sRadiusInner = inner;
        }

        public void SetPoints(int points)
        {
            if (points >= 2) // mínimo 2 puntas
                numPoints = points;
        }

        public void Rotate(float angleDelta, Graphics g, float centerX, float centerY)
        {
            mAngle += angleDelta;

            if (sRadiusOuter <= 0 || sRadiusInner <= 0)
                return;

            Pen ePen = new Pen(Color.Cyan, 3);
            PointF[] starPoints = new PointF[numPoints * 2];
            double angle = -Math.PI / 2;
            double angleRad = mAngle * Math.PI / 180.0;
            double angleStep = Math.PI / numPoints;

            for (int i = 0; i < starPoints.Length; i++)
            {
                float radius = (i % 2 == 0) ? sRadiusOuter * SF : sRadiusInner * SF;
                float x = (float)(radius * Math.Cos(angle));
                float y = (float)(radius * Math.Sin(angle));

                float rotX = (float)(x * Math.Cos(angleRad) - y * Math.Sin(angleRad)) * SF;
                float rotY = (float)(x * Math.Sin(angleRad) + y * Math.Cos(angleRad)) * SF;

                starPoints[i] = new PointF(centerX + rotX, centerY + rotY);
                angle += angleStep;
            }

            g.DrawPolygon(ePen, starPoints);
        }

        public void PlotShape(PictureBox picCanvas)
        {
            if (sRadiusOuter <= 0 || sRadiusInner <= 0)
                return;

            Graphics eGraph = picCanvas.CreateGraphics();
            Pen ePen = new Pen(Color.Blue, 3);
            float centerX = picCanvas.Width / 2f;
            float centerY = picCanvas.Height / 2f;

            PointF[] starPoints = new PointF[numPoints * 2];
            double angle = -Math.PI / 2;
            double angleRad = mAngle * Math.PI / 180.0;
            double angleStep = Math.PI / numPoints;

            for (int i = 0; i < starPoints.Length; i++)
            {
                float radius = (i % 2 == 0) ? sRadiusOuter * SF : sRadiusInner * SF;
                float x = (float)(radius * Math.Cos(angle));
                float y = (float)(radius * Math.Sin(angle));

                float rotX = (float)(x * Math.Cos(angleRad) - y * Math.Sin(angleRad)) * SF;
                float rotY = (float)(x * Math.Sin(angleRad) + y * Math.Cos(angleRad)) * SF;

                starPoints[i] = new PointF(centerX + rotX, centerY + rotY);
                angle += angleStep;
            }

            eGraph.DrawPolygon(ePen, starPoints);
        }

        public void Rotate(float angleDelta, PictureBox picCanvas)
        {
            mAngle += angleDelta;
            PlotShape(picCanvas);
        }

        public void CloseForm(Form ObjForm)
        {
            ObjForm.Close();
        }
    }
}
