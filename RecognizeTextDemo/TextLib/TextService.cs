using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TextLib.Models;

namespace TextLib
{

    public static class TextService
    {
        const string visionEndPoint = "https://westus.api.cognitive.microsoft.com/";

        public static async Task<string> RecognizeText(string imageUrl, string computerVisionKey)
        {
            //var visionEndPoint = "https://westus.api.cognitive.microsoft.com/";
            var cogSvcUrl = visionEndPoint + "vision/v2.0/recognizeText?mode=Printed";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", computerVisionKey);

            HttpResponseMessage response;
            var json = "{'url': '" + imageUrl + "'}";
            byte[] byteData = Encoding.UTF8.GetBytes(json);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(cogSvcUrl, content);
            }

            var headers = response.Headers;
            var locationHeaders = response.Headers.GetValues("Operation-Location");
            string locationAddress = "";
            IEnumerable<string> values;
            if (headers.TryGetValues("Operation-Location", out values))
            {
                locationAddress = values.First();
            }

            return locationAddress;
        }


        public static async Task<RecognizeTextResult> GetRecognizeTextOperationResults(string locationAddress, string computerVisionKey)
        {

            var client = new HttpClient();
            var cogSvcUrl = visionEndPoint + "vision/v2.0/recognizeText?mode=Printed";

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", computerVisionKey);

            var response = await client.GetAsync(locationAddress);
            RecognizeTextResult results = null;
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<RecognizeTextResult>(data);
            }
            return results;
        }

        public static async Task<string> GetRecognizeTextOperationResultsFromFile(string imageLocation, string computerVisionKey)
        {

            var cogSvcUrl = visionEndPoint + "vision/v2.0/recognizeText?mode=Printed";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", computerVisionKey);

            HttpResponseMessage response;

            byte[] byteData = GetImageAsByteArray(imageLocation);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(cogSvcUrl, content);
            }

            RecognizeTextResult results = null;
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                results = JsonConvert.DeserializeObject<RecognizeTextResult>(data);
            }

            var headers = response.Headers;
            var locationHeaders = response.Headers.GetValues("Operation-Location");
            string locationAddress = "";
            IEnumerable<string> values;
            if (headers.TryGetValues("Operation-Location", out values))
            {
                locationAddress = values.First();
            }

            return locationAddress;

        }

        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

    }
}

