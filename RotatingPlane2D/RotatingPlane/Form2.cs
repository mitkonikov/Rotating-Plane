using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotatingPlane
{
    public partial class Form2 : Form
    {
        Bitmap bmp = new Bitmap(400, 400);
        Graphics g;
        // Settings
        int radius = 400 / 2 - 50;
        int center = 350 / 2;
        int angle = 0;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load_1(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp); //Binding the graphics to the Bitmap
            pictureBox1.Image = bmp;
            RotatingTimer.Start();
        }

        private int[] LineCoord(int angleIn, int radius, int center)
        {
            int[] coord = new int[2]; // Setting up the int array for return
            angleIn %= 360;
            angleIn *= 1;

            if (angleIn >= 0 && angleIn <= 180)
            {
                coord[0] = center + (int)(radius * Math.Sin(Math.PI * angleIn / 180));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / 180));
            }
            else
            {
                coord[0] = center - (int)(radius * -Math.Sin(Math.PI * angleIn / 180));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / 180));
            }
            return coord;
        }

        private void RotatingTimer_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            // Points
            Point point1 = new Point(LineCoord(angle, radius, center)[0], LineCoord(angle, radius, center)[1]);
            Point point2 = new Point(LineCoord(angle + 90, radius, center)[0], LineCoord(angle + 90, radius, center)[1]);
            Point point3 = new Point(LineCoord(angle + 180, radius, center)[0], LineCoord(angle + 180, radius, center)[1]);
            Point point4 = new Point(LineCoord(angle + 270, radius, center)[0], LineCoord(angle + 270, radius, center)[1]);
            Point[] points = {point1, point2, point3, point4}; // Get all points in one array
            pictureBox1.Image = bmp;
            g.FillPolygon(new SolidBrush(Color.Blue), points); //Draw a fill polygon
            // If you want to see the circle
            // g.DrawEllipse(new Pen(Color.Black, 2f), center - radius, center - radius, radius * 2, radius * 2);
            g.Dispose();
            if (angle == 360)
            {
                angle = 0;
            }
            else
            {
                angle++;
            }
        }
    }
}
