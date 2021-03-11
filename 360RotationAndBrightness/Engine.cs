using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360RotationAndBrightness
{
    class Engine
    {
        Image img;
        string path;
        Brightnes brightnes = new Brightnes();
        Rotation rotation = new Rotation();

        public Engine(Image im,string pt)
        {
            img = im;
            path = pt;
        }
        public void Processing()
        {

        }

        private int[,] GetCountorPoints(Bitmap img)
        {
            int colorBackr = img.GetPixel(0, 0).R;
            Bitmap tecImg = new Bitmap(img);
            List<Point> objectsPoints = new List<Point>();
            int i = 0, j = 0;
            while (i < img.Width - 1)
            {
                if (img.GetPixel(i, j).R != colorBackr)
                {

                    objectsPoints.Add(new Point(i, j));
                }
                if (j == img.Height - 1)
                {
                    j = 0;
                    i++;
                }
                else
                    j++;
            }
            int xmin = int.MaxValue, ymin = int.MaxValue, ymax = int.MinValue, xmax = int.MinValue;
            foreach (Point pt in objectsPoints)
            {
                if (pt.X > xmax) xmax = pt.X;
                if (pt.X < xmin) xmin = pt.X;
                if (pt.Y > ymax) ymax = pt.Y;
                if (pt.Y < ymin) ymin = pt.Y;
            }

            int[,] resmat = new int[xmax - xmin + 1, ymax - ymin + 1];
            for (int k = 0; k < xmax - xmin + 1; k++)
            {
                for (int l = 0; l < ymax - ymin + 1; l++)
                    resmat[k, l] = -1;
            }
            int y1 = 0, x1 = 0;
            for (int y = ymin; y <= ymax; y++)
            {
                for (int x = xmin; x <= xmax; x++)
                {
                    if (tecImg.GetPixel(x, y).R != 0)
                    {
                        resmat[x1, y1] = tecImg.GetPixel(x, y).R;
                    }
                    x1++;
                }
                x1 = 0;
                y1++;
            }
            return resmat;
        }
    }
}
