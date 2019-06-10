using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OCR_DOTNETDemo
{
    internal class OCRServices
    {
    internal static async Task<OcrResult> UploadAndRecognizeImageAsync(string imageFilePath, OcrLanguages language)
        {
            string key = ConfigurationManager.AppSettings["ComputerVisionApiKey"];  
            string endPoint = ConfigurationManager.AppSettings["ComputerVisionEndpoint"];
            var credentials = new ApiKeyServiceClientCredentials(key);

            using (var client = new ComputerVisionClient(credentials) { Endpoint = endPoint })
            {
                using (Stream imageFileStream = File.OpenRead(imageFilePath))
                {
                    OcrResult ocrResult = await client.RecognizePrintedTextInStreamAsync(false, imageFileStream, language);
                    return ocrResult;
                }
            }
        }

    internal static async Task<string> FormatOcrResult(OcrResult ocrResult)
        {
            var sb = new StringBuilder();
            foreach(OcrRegion region in  ocrResult.Regions)
            {
                foreach (OcrLine line in region.Lines)
                {
                    foreach (OcrWord word in line.Words)
                    {
                        sb.Append(word.Text);
                        sb.Append(" ");
                    }
                    sb.Append("\r\n");
                }
                sb.Append("\r\n\r\n");
            }
            return sb.ToString();
        }
    }
}
