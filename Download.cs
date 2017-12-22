using System;
using System.IO;
using System.Net;
using System.Threading;

namespace picdown
{
    public class Download
    {
        public Download(String url, String downloadPath) {
            this.url = url;
            this.downloadPath = downloadPath;
        }

        private String _url = String.Empty;
        public String url {
            get { return this._url; }
            set { this._url = value; }
        }

        private String _downloadPath = String.Empty;
        public String downloadPath {
            get { return this._downloadPath; }
            set { this._downloadPath = value; }
        }

        private Boolean isValidImageUrl(String url) {
            Uri uriResult;
            var created = Uri.TryCreate(url, UriKind.Absolute, out uriResult);
            var isHttpScheme = (uriResult != null && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
            return (created && isHttpScheme);
        }        

        public Boolean Execute() {
            var valid = this.isValidImageUrl(this.url);
            
            if (valid) {
                Console.WriteLine($"downloading {this.url}");
                using (WebClient client = new WebClient()) 
                {
                    var uri = new Uri(this.url);
                    var localFileName = this.downloadPath + uri.LocalPath;                    
                    Directory.CreateDirectory(Path.GetDirectoryName(localFileName));
                    client.DownloadFile(uri, localFileName);
                }                
            }
            else {
                Console.WriteLine($"{this.url} is invalid");
            }

            return valid;
        }

    }
}
