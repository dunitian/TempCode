using ImageMagick;
using System;
using System.Windows.Forms;

namespace MagickTest
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                //在原始图片的上距离上部30像素左部50为起点的位置, 分别向左向下截取一块大小为100x80的图片。如果x相对于坐标，宽度不够100，那就取实际值。
                //convert src.jpg - crop 100x80 + 50 + 30 dest.jpg

                //裁剪
                using (var magickImg = new MagickImage(@"C:\Users\DNT\Desktop\识别目录\658978-20160922111329527-2030285818.png"))
                {
                    //X,Y,Width,Height
                    magickImg.Crop(830, 151, 183, 183);
                    magickImg.Write(@"C:\Users\DNT\Desktop\识别目录\1.png");
                }
            }
            catch (MagickException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            MessageBox.Show("ok");
        }
    }
}
