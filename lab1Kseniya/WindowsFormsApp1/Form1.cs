using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap newBitmap;
        Image file;
        Boolean opened = false;
        float contrast = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK) {
                file = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = file;
                opened = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == DialogResult.OK) {
                if (opened)
                {
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "jpg")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    } 
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar1.Value.ToString();
            pictureBox1.Image = AdjustBrightness(newBitmap, trackBar1.Value);
        }

        public static Bitmap AdjustBrightness(Bitmap Image, int Value) {

            Bitmap TempBitmap = Image;
            float FinalValue = (float)Value / 255.0f;

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

            Graphics NewGraphics = Graphics.FromImage(NewBitmap);
            float [][] FloatColorMatrix = {
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0 },
                new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
                };
            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);
            ImageAttributes Attributes = new ImageAttributes();
            Attributes.SetColorMatrix(NewColorMatrix);
            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);
            Attributes.Dispose();
            NewGraphics.Dispose();
            return NewBitmap;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label6.Text = trackBar2.Value.ToString();
            contrast = 0.04f * trackBar2.Value;
            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);
            Graphics g = Graphics.FromImage(bm);
            ImageAttributes ia = new ImageAttributes();
            ColorMatrix cm = new ColorMatrix(new float[][]
            {
                new float[] {contrast, 0f, 0f, 0f, 0f},
                new float[] {0f, contrast, 0f, 0f, 0f},
                new float[] {0f, 0f, contrast, 0f, 0f},
                new float[] {0f, 0f, 0f, 1f, 0f},

                new float []{0.001f, 0.001f, 0.001f, 0f,1f}});
            ia.SetColorMatrix(cm);
            g.DrawImage(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), 0, 0, newBitmap.Width, newBitmap.Height, GraphicsUnit.Pixel, ia);
            g.Dispose();
            ia.Dispose();
            pictureBox1.Image = bm;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
