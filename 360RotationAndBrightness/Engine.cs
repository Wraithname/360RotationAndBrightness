using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _360RotationAndBrightness
{
    class Engine
    {
        Image img;
        string path;
        Brightnes brightnes = new Brightnes();
        Rotation rotation = new Rotation();

        public Engine(Image im, string pt)
        {
            img = im;
            path = pt;
        }
        public void Processing()
        {
            int[,] counter = GetCountorPoints(new Bitmap(img));
            Image[] imgarray = new Image[7];
            int k = 0;
            for (float x = -0.3f; x <= 0.3f; x += 0.1f)
            {
                imgarray[k] = brightnes.MakeGrayscale3(new Bitmap(img), x);
                k++;
            }
            Thread[] tr = new Thread[2];
            tr[0] = new Thread(x => ProcPng(imgarray[0], -0.3f));
            tr[1] = new Thread(x => ProcPng(imgarray[1], -0.2f));
            foreach (var t in tr)
                t.Start();
            foreach (var t in tr)
                t.Join();
            tr = new Thread[2];
            tr[0] = new Thread(x => ProcPng(imgarray[2], -0.1f));
            tr[1] = new Thread(x => ProcPng(imgarray[3], 0f));
            foreach (var t in tr)
                t.Start();
            foreach (var t in tr)
                t.Join();
            tr = new Thread[3];
            tr[0] = new Thread(x => ProcPng(imgarray[4], 0.1f));
            tr[1] = new Thread(x => ProcPng(imgarray[5], 0.2f));
            tr[2] = new Thread(x => ProcPng(imgarray[6], 0.3f));
            foreach (var t in tr)
                t.Start();
            foreach (var t in tr)
                t.Join();
        }
        private void ProcPng(Image img, float k)
        {
            Bitmap bitmp = new Bitmap(img);
            double[] center = new double[2];
            center[0] = bitmp.Width / 2;
            center[1] = bitmp.Height / 2;
            for (int i = 0; i <= 360; i++)
                WriteToPngFile(rotation.RotateImage(bitmp, i, center), k.ToString(), i.ToString());
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

        void WriteToPngFile(Image image, string numbr, string nameangel)
        {
            if (!Directory.Exists(path + @"\Brightnes" + numbr))
                Directory.CreateDirectory(path + @"\Brightnes" + numbr);
            image.Save(path + @"\Brightnes" + numbr + @"\" + nameangel + @".png");
        }
    }
}
