using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _360RotationAndBrightness
{
    class Rotation
    {
        public Image RotateImage(
            Image pImage, float pAngle, double[] center)
        {
            Matrix lMatrix = new Matrix();
            lMatrix.RotateAt(pAngle, new Point((int)center[0], (int)center[1]));
            Bitmap lNewBitmap = new Bitmap(pImage.Width, pImage.Height);
            lNewBitmap.SetResolution(pImage.HorizontalResolution, pImage.VerticalResolution);
            Graphics lGraphics = Graphics.FromImage(lNewBitmap);
            lGraphics.Clear(Color.Transparent);
            lGraphics.Transform = lMatrix;
            lGraphics.DrawImage(pImage, 0, 0);
            lGraphics.Dispose();
            lMatrix.Dispose();
            return lNewBitmap;
        }
    }
}
