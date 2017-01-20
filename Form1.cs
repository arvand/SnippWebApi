using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnippingTool.Helper;

namespace SnippingTool
{
    public partial class Form1 : Form
    {
        private Image snipImage;

        private Uploader uploader = new Uploader();
        public Form1()
        {
            InitializeComponent();
            SnippingTool.AreaSelected += OnAreaSelected;

        }
        public static Stream ToStream(Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        private async void btnPost_Click(object sender, EventArgs e)
        {
            btnPost.Text = "Uploading";
            btnPost.Enabled = false;

            try
            {
                ImageHelper.PutWatermark(snipImage);

                //snipImage.Save(@"D:\1.jpg");
                var tasks = new List<Task>();

                tasks.Add(Task.Run(() => uploader.PostToWebSite(snipImage, new SampleClass() { Title = "sth" })));


                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnPost.Text = "Post";
            btnPost.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnAreaSelected(object sender, EventArgs e)
        {
            snipImage = SnippingTool.Image;
            // Do something with the bitmap
            //...
            //snipImage.Save(@"C:\Downloads\myfile.png", ImageFormat.Png);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnSnip_Click(object sender, EventArgs e)
        {
            SnippingTool.Snip();
        }
    }
}
