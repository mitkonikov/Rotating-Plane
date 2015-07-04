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
    public partial class RotatingPlane : Form
    {
        Bitmap bmp = new Bitmap(1500, 1500);
        Graphics g;
        // Settings
        int radius = 1500 / 2 - 50;
        int center = 1500 / 2;
        int angle = 180;

        public RotatingPlane()
        {
            InitializeComponent();
        }

        private void RotatingPlane_Load(object sender, EventArgs e)
        {
            Form form2 = new Form2();
            form2.Show();
            g = Graphics.FromImage(bmp); //Binding the graphics to the Bitmap
            pictureBox1.Image = bmp;
            RotationTimer.Start();
        }

        private int[] LineCoord(int angleIn, int radius, int center)
        {
            int[] coord = new int[2]; // Setting up the int array for return
            angleIn %= 1440;
            angleIn *= 1;

            if (angleIn >= 0 && angleIn <= 720)
            {
                coord[0] = center + (int)(radius * Math.Sin(Math.PI * angleIn / 720));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / 720));
            }
            else
            {
                coord[0] = center - (int)(radius * -Math.Sin(Math.PI * angleIn / 720));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / 720));
            }
            return coord;
        }

        private void RotationTimer_Tick(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            // Points
            Point point1 = new Point(LineCoord(angle, radius, center)[0], LineCoord(angle, radius, center)[1]);
            Point point2 = new Point(LineCoord(angle + 360, radius, center)[0], LineCoord(angle + 360, radius, center)[1]);
            Point point3 = new Point(LineCoord(angle + 720, radius, center)[0], LineCoord(angle + 720, radius, center)[1]);
            Point point4 = new Point(LineCoord(angle + 1080, radius, center)[0], LineCoord(angle + 1080, radius, center)[1]);
            Point[] points = {point1, point2, point3, point4}; // Get all points in one array
            pictureBox1.Image = bmp;
            g.FillPolygon(new SolidBrush(Color.Blue), points); //Draw a fill polygon
            // If you want to see the circle
            // g.DrawEllipse(new Pen(Color.Black, 10f), center - radius, center - radius, radius * 2, radius * 2);
            g.Dispose();
            if (angle == 1440)
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
