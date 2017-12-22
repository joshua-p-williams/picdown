using System;
using System.Collections.Generic;
using System.IO;

namespace picdown
{
    public class BatchDownload
    {
        public BatchDownload(String fileName, String downloadPath) {
            this.fileName = fileName;
            this.downloadPath = downloadPath;
        }

        private String _fileName = String.Empty;
        public String fileName {
            get { return this._fileName; }
            set { this._fileName = value; }
        }

        private String _downloadPath = String.Empty;
        public String downloadPath {
            get { return this._downloadPath; }
            set { this._downloadPath = value; }
        }

        private List<String> _urls = new List<string>();

        private Boolean Load() {
            var loaded = File.Exists(this._fileName);
            Console.WriteLine($"Loading image urls from {this._fileName}");

            if (loaded) {
                var urls = File.ReadAllLines(this._fileName);
                if (urls.Length > 0) {
                    this._urls = new List<String>(urls);
                }
            }
            else {
                Console.Error.WriteLine($"The file {this._fileName} does not exist!");
            }
            return loaded;
        }
        
        public Boolean Execute() {
            var success = this.Load();

            if (success && this._urls != null) {
                var progress = 0;
                var total = this._urls.Count;

                foreach(var url in this._urls) {
                    progress++;
                    Console.Write($"{progress} of {total} - ");

                    if (!String.IsNullOrWhiteSpace(url)) {
                        var download = new Download(url, this.downloadPath);
                        if (!download.Execute()) {
                            // One failure returns false for the batch as a whole
                            success = false;
                        }
                    }
                    else {
                        Console.WriteLine("Empty line");
                    }
                }
            }

            return success;
        }

    }
}
