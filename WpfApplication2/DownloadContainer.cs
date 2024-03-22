using System;
using System.Net;

namespace WpfApplication2
{
    public class DownloadContainer
    {
        public static Types.UpdateVar<int> downloadProgress = new Types.UpdateVar<int>();
        public string _url { get; set; }
        public WebClient _client { get; set; }
        public DownloadContainer(string url)
        {
            this._url = url;
            this._client = new WebClient();
        }

        public void executeDownload()
        {
            try
            {
                Uri uri = new Uri(this._url);
                string filename = System.IO.Path.GetFileName(uri.LocalPath);
                this._client.DownloadFileAsync(uri, filename);
            }
            catch
            {
                Console.WriteLine("Error downloading file");
            }
            
        }
    }
}