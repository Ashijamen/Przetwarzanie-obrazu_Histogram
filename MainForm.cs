using System.Diagnostics;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.Util.TypeEnum;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using Emgu.CV.Util;
using static System.Net.Mime.MediaTypeNames;
using Emgu.CV.Reg;
using static Emgu.Util.Platform;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms.Design;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections;
using Emgu.CV.Flann;
namespace WinFormsApp1BMP
{

    //Install-Package Emgu.CV
    //Install-Package Emgu.CV.Bitmap
    //
    public partial class MainForm : Form
    {

        
        Mat _image;
        Bitmap _imageLoaded1;
        Bitmap _imageLoaded2;
        int[] histogramR = new int[256];
        int[] histogramG = new int[256];
        int[] histogramB = new int[256];



        public MainForm()
        {
            InitializeComponent();
            Htxt.Text = H.Value.ToString();
            Stxt.Text = S.Value.ToString();
            Vtxt.Text = V.Value.ToString();
            H2txt.Text = H2.Value.ToString();
            S2txt.Text = S2.Value.ToString();
            V2txt.Text = V2.Value.ToString();

        }

        private void LoadImage1btn_Click(object sender, EventArgs e)
        {
            try
            {
                var loaded = LoadBitmapFileWindow();
                _imageLoaded1 = loaded.Bitmap;

                pictureBox1.Image = _imageLoaded1;
                Image1Label.Text = loaded.Filename;
                PictureWindow pictureWindow = new PictureWindow(_imageLoaded1, "original");
                pictureWindow.Show();

                //Recalculate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadImage2btn_Click(object sender, EventArgs e)
        {
            var loaded = LoadBitmapFileWindow();
            _imageLoaded2 = loaded.Bitmap;
            this.Text = loaded.Filename;
            pictureBox2.Image = _imageLoaded2;
            PictureWindow pictureWindow = new PictureWindow(_imageLoaded2, "original");
            pictureWindow.Show();
            //Recalculate();
        }


        public void Recalculate()
        {
            //var img=AdjustBrightnessContrast(_imageLoaded1, H.Value, S.Value);
            // var img=ROzciagnijHistogram(_imageLoaded1)
            // pictureBox2.Image = img;

            //var win = new PictureWindow(img, "Contrast");

            // pobranie wymiarów obrazu
            // int wysokosc = naszObraz.Height;
            // int szerokosc = naszObraz.Width;
            //
            // utworzenie nowej bitmapy
            // Bitmap bmp = new Bitmap(szerokosc, wysokosc);
            //
            // pobranie wartości pixela
            // bmp.GetPixel(x, y);
            //
            // ustawienie wartości pixela
            // bmp.SetPixel(x, y, color);
            //
            // stworzenie zmiennej typu color
            // Color c = Color.FromArgb(redValue, greenValue, blueValue);
            //
            // zapisanie Bitmapy do pliku 
            // bmp.Save("example.bmp");

            //var img = AdjustBrightnessContrast(_imageLoaded1, wysokosc, szerokosc);


            pictureBox1.Image = _imageLoaded1;
            pictureBox2.Image = _imageLoaded2;
            int wysokosc = _imageLoaded1.Height;
            int szerokosc = _imageLoaded1.Width;

            Bitmap bmp1  = new Bitmap(szerokosc, wysokosc);

            double factor = 0.5;


            for (int y = 0; y < wysokosc; y++)
            {


                for (int x = 0; x < szerokosc; x++)
                {

                    var pixel = _imageLoaded1.GetPixel(x, y);
                    //Color kolor = Color.FromArgb();
                    


                    var pixel2 = _imageLoaded2.GetPixel(x, y);
                    //Color kolor2 = Color.FromArgb();
                    //Color kolor3 = Color.FromArgb();

                    byte R = (byte)(((float)pixel.R * factor) + (1 - factor) * (float)pixel2.R);
                    byte G = (byte)(((float)pixel.G * factor) + (1 - factor) * (float)pixel2.G);
                    byte B = (byte)(((float)pixel.B * factor) + (1 - factor) * (float)pixel2.B);
                    Color kolor2 = Color.FromArgb(R, G, B);
                    bmp1.SetPixel(x, y, kolor2);






                }
            }
            pictureBox3.Image=bmp1;

        }

        private void H_Scroll(object sender, EventArgs e)
        {
            Htxt.Text = H.Value.ToString();
        }
        private void S_Scroll(object sender, EventArgs e)
        {
            Stxt.Text = S.Value.ToString();
        }
        private void V_Scroll(object sender, EventArgs e)
        {
            Vtxt.Text = V.Value.ToString();
        }
        private void H2_Scroll(object sender, EventArgs e)
        {
            H2txt.Text = H2.Value.ToString();
        }
        private void S2_Scroll(object sender, EventArgs e)
        {
            S2txt.Text = S2.Value.ToString();
        }
        private void V2_Scroll(object sender, EventArgs e)
        {
            V2txt.Text = V2.Value.ToString();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Recalculate();
        }
        private void Htxt_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(Htxt.Text.Trim(), out int val))
            {
                H.Value = val;
            }
        }
        private void Stxt_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(Stxt.Text.Trim(), out int val))
            {
                S.Value = val;
            }
        }
        private void Vtxt_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(Vtxt.Text.Trim(), out int val))
            {
                V.Value = val;
            }
        }
        private void H2txt_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(H2txt.Text.Trim(), out int val))
            {
                H2.Value = val;
            }
        }
        private void S2txt_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(S2txt.Text.Trim(), out int val))
            {
                S2.Value = val;
            }
        }
        private void V2txt_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(V2txt.Text.Trim(), out int val))
            {
                V2.Value = val;
            }
        }
        private (Bitmap Bitmap, string Filename) LoadBitmapFileWindow()
        {
            Bitmap image = null;
            string filename = string.Empty;
            try
            {
                openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.PNG;*.JPG;*.JPEG;*.BMP";
                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    var file = openFileDialog1.FileName;
                    this.Text = file;
                    image = new Bitmap(file);
                }
            }
            catch (Exception ex) { throw ex; }
            return (image, filename);
        }

        private void HistBtn_Click(object sender, EventArgs e)
        {

            //stworzenie nowej serii danych na wykres
            //Series seriesRed = new Series
            //{
            //    Name = "Red",
            //    Color = Color.Red,
            //    ChartType = SeriesChartType.Column
            //};
            //
            //dodanie do serii danych punktów na wykres
            //seriesRed.AddXY(wartośćX, WartośćY);
            //
            //stworzenie nowej kolekcji dla wielu serii  
            //List<Series> series = new List<Series>();
            //
            //dodanie do kolekcji utworzonej wczesniej serii danych
            //series.Add(seriesRed);
            //
            //przekazanie kolekcji danych do okna wykresu
            //ChartWindow chart = new ChartWindow(series, "histogram obrazu");
            //chart.Show();
            for (int y = 0; y < _imageLoaded1.Height; y++)
            {
                for (int x = 0; x < _imageLoaded1.Width; x++)
                {
                    Color pixel = _imageLoaded1.GetPixel(x, y);

                    histogramR[pixel.R]++;
                    histogramG[pixel.G]++;
                    histogramB[pixel.B]++;
                }
            }
            Series seriesRed = new Series
            {
                Name = "Red",
                Color = Color.Red,
                ChartType = SeriesChartType.FastLine
            };
            Series seriesGreen = new Series
            {
                Name = "Green",
                Color = Color.Green,
                ChartType = SeriesChartType.FastLine
            };
            Series seriesBlue = new Series
            {
                Name = "Blue",
                Color = Color.Blue,
                ChartType = SeriesChartType.FastLine
            };

            for (int i = 0; i < 256; i++)
            {
                seriesRed.Points.AddXY(i, histogramR[i]);
                seriesGreen.Points.AddXY(i, histogramG[i]);
                seriesBlue.Points.AddXY(i, histogramB[i]);
            }

            
            List<Series> seriesList = new List<Series> { seriesRed, seriesGreen, seriesBlue };
            ChartWindow chart = new ChartWindow(seriesList, "Histogram obrazu");
            chart.Show();

        }


    }
}
    

