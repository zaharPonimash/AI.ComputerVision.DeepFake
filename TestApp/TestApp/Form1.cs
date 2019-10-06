using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using AI.ComputerVision.DeepFake.Core;
using AI.ComputerVision.DeepFake;
using AI.ComputerVision;
using System.Windows.Forms;


namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap face = ImgConverter.BmpResizeM(new Bitmap("data\\face.jpg"),700);
        Bitmap face2 = ImgConverter.BmpResizeM(new Bitmap("data\\face2.jpg"), 700);

        private void Form1_Load(object sender, EventArgs e)
        {
            AI.Dlib.FaceData fd = new AI.Dlib.FaceData();

            pictureBox1.Image = fd.AccurateFaceDetectionBm(face);
            pictureBox2.Image = fd.AccurateFaceDetectionBm(face2);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ReplFaceImg.Repl(face, face2);
            ((Bitmap)pictureBox1.Image).Save("output.png");
        }
    }
}
