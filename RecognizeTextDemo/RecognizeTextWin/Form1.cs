using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextLib;
using TextLib.Models;

namespace RecognizeTextWin
{
    public partial class Form1 : Form
    {
        private string _locationAddress = "";
        private string _computerVisionKey = "";
        private string _receiptFileLocation; // = @"c:\test\Receipt.jpg";

        public Form1()
        {
            InitializeComponent();
            GetTextButton.Enabled = false;
            StartOcrFromFileButton.Enabled = false;
            LocationAddressLabel.Visible = false;
            _computerVisionKey = Utilities.GetKey();
        }
        private async void GetTextButton_Click(object sender, EventArgs e)
        {
            RecognizeTextResult results = await TextService.GetRecognizeTextOperationResults(_locationAddress, _computerVisionKey);
            await DisplayText(results);
        }

        private async Task DisplayText(RecognizeTextResult results)
        {
            var sb = new StringBuilder();

            sb.Append("Status: ");
            sb.Append(results.Status);
            sb.Append("\r\n\r\n");

            var lines = results.RecognitionResult.Lines;
            foreach (Line line in lines)
            {
                sb.Append(line.Text);
                sb.Append("\r\n");
            }

            ResultsTextbox.Text = sb.ToString();

        }

        private async void StartOcrFromFileButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(_receiptFileLocation))
            {
                MessageBox.Show("No such file: " + _receiptFileLocation);
                return;
            }

            _locationAddress = await TextService.GetRecognizeTextOperationResultsFromFile(_receiptFileLocation, _computerVisionKey);
            if (_locationAddress != "")
            {
                LocationAddressLabel.Text = "Location Address:\r\n" + _locationAddress;
                LocationAddressLabel.Visible = true;
                GetTextButton.Enabled = true;
            }

        }

        private void GetFileButton_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "JPG |*.jpg|PNG|*.png|BMP|*.bmp";
            this.openFileDialog1.Title = "Select an image with text";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _receiptFileLocation = openFileDialog1.FileName;
                StartOcrFromFileButton.Enabled = true;
                FileNameLabel.Visible = true;
                FileNameLabel.Text = "File Name: " + _receiptFileLocation;
            }

        }
    }
}
