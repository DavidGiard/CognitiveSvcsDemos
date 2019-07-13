using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ImageAnalysisConsoleAppDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            string key = GetKey();
            string imageFilePath = @"c:\test\kittens.jpg";
            if (!File.Exists(imageFilePath))
            {
                Console.WriteLine("File {0} does not exist", imageFilePath);
                return;
            }
            string results = await GetRecognizeTextOperationResultsFromFile(imageFilePath, key);
            Console.WriteLine(results);
        }


        public static async Task<string> GetRecognizeTextOperationResultsFromFile(string imageFilePath, string computerVisionKey)
        {
            // Convert file into Byte Array
            byte[] byteData; ;
            using (FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                byteData = binaryReader.ReadBytes((int)fileStream.Length);
            }

            // Make web service call. Pass byte array in body
            var cogSvcUrl = "https://westus.api.cognitive.microsoft.com/vision/v2.0/analyze?visualFeatures=Description&language=en";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", computerVisionKey);
            HttpResponseMessage response;
            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(cogSvcUrl, content);
            }

            // Get results
            string webServiceResponseContent = await response.Content.ReadAsStringAsync();
            return webServiceResponseContent;
        }

        public static string GetKey()
        {
            string computerVisionKey = ConfigurationManager.AppSettings["ComputerVisionKey"];
            return computerVisionKey;
        }

    }
}
