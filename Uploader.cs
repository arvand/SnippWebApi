using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SnippingTool
{
    public class SampleClass
    {
        public string Title { get; set; }
    }
    public class Uploader
    {
        private static readonly string RequestUri = ConfigurationManager.AppSettings["siteUrl"];

        public static Stream ToStream(Image image, ImageFormat formaw)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }

        public async Task PostToWebSite(Image image, SampleClass obj)
        {
            using (var copy = new Bitmap(image))
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        var values = new[]
                        {
                            new KeyValuePair<string, string>("Product", JsonConvert.SerializeObject(obj)),
                            new KeyValuePair<string, string>("Key", ConfigurationManager.AppSettings["SitePassword"]),

                        };

                        foreach (var keyValuePair in values)
                        {
                            content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
                        }

                        var fileContent = new StreamContent(ToStream(copy, ImageFormat.Jpeg));

                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = obj.Title + ".jpg",

                        };

                        content.Add(fileContent);

                        var result = await client.PostAsync(RequestUri, content);
                    }
                }
            }
        }
    }
}
