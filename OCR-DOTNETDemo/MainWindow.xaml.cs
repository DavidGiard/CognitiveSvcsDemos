using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OCR_DOTNETDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string ImagePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void GetImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.bmp, *.jpg, *.gif, *.png)|*.bmp;*.jpg;*.gif;*.png|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                ImagePath = openFileDialog.FileName;
                var uriSource = new Uri(ImagePath);
                OCRImage.Source = new BitmapImage(uriSource);
            }
        }

        private async void GetText_Click(object sender, RoutedEventArgs e)
        {
            OutputTextBlock.Text = "Thinking...";
            var language = OcrLanguages.En;
            OcrResult ocrResult =  await OCRServices.UploadAndRecognizeImageAsync(ImagePath, language);
            string resultText = await OCRServices.FormatOcrResult(ocrResult);
            OutputTextBlock.Text = resultText; // ocrResult.Regions[0].Lines[0].Words[0].Text;
        }
    }
}
