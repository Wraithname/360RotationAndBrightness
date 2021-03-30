using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _360RotationAndBrightness
{
    public partial class Form1 : Form
    {
        Image img;
        string path = "";
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd=new OpenFileDialog())
            {
                ofd.Filter = "";
                if(ofd.ShowDialog()==DialogResult.OK)
                {
                    img = new Bitmap(ofd.FileName);
                }
            }
            button2.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog fbd=new FolderBrowserDialog())
            {
                if(fbd.ShowDialog()==DialogResult.OK)
                {
                    path = fbd.SelectedPath;
                    Engine eng = new Engine(img, path);
                    eng.Processing();
                }
            }
        }
    }
}
