using AI.ComputerVision;
using AI.ComputerVision.DeepFake;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private Bitmap face;
        private Bitmap background;
        private bool faceIsLoaded = false;
        private bool backgroundIsLoaded = false;
        private readonly OpenFileDialog openFile = new OpenFileDialog();

        // Open background
        private void button3_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read))
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(fs);
                        background = ImgConverter.BmpResizeM(bmp, 700);
                        backgroundIsLoaded = true;
                        backgroundBox.Image = background;
                        UnLock();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Open face
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog() == DialogResult.OK) 
            {
                using (FileStream fs = new FileStream(openFile.FileName, FileMode.Open, FileAccess.Read)) 
                {
                    try
                    {
                        Bitmap bmp = new Bitmap(fs);
                        face = ImgConverter.BmpResizeM(bmp, 700);
                        faceIsLoaded = true;
                        faceBox.Image = face;
                        UnLock();
                    }
                    catch(Exception ex) 
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        
        // Replace
        private void Button1_Click(object sender, EventArgs e)
        {
            backgroundBox.Image = ReplFaceImg.Repl(background, face);
            ((Bitmap)backgroundBox.Image).Save("output.png");
        }

        private void UnLock() 
        {
            if (faceIsLoaded && backgroundIsLoaded)
            {
                button1.Enabled = true;
            }
        }

        
    }
}
