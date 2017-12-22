using System;
using Microsoft.Extensions.CommandLineUtils;

namespace picdown
{

    class Program
    {
        private static Boolean Execute(CommandLineApplication app, CommandOption url, CommandOption imagelist, CommandOption downloadpath) {
            var success = true; // Assume success

            if (url.HasValue() || imagelist.HasValue()) {

                var downloadPath = "./download";

                if (downloadpath.HasValue()) {
                    downloadPath = downloadpath.Value();
                }

                if (url.HasValue()) {
                    var download = new Download(url.Value(), downloadPath);
                    success = download.Execute();
                }

                if (imagelist.HasValue()) {
                    var batch = new BatchDownload(imagelist.Value(), downloadPath);
                    success = batch.Execute();
                }
            }
            else {
                Console.Error.WriteLine("Either an image URL or an image list file is required");
                app.ShowHelp();
                success = false;
            }

            return success;
        }

        static void Main(string[] args) {

            var app = new CommandLineApplication();

            var url = app.Option("--url", "Specify the image url to download", CommandOptionType.SingleValue);
            var imagelist = app.Option("--imagelist", "Specify the file that contains the list of images urls to download", CommandOptionType.SingleValue);
            var downloadpath = app.Option("--downloadpath", "Specify the path to save the downloaded files too", CommandOptionType.SingleValue);
            
            app.OnExecute(() => {
                var success = Program.Execute(app, url, imagelist, downloadpath);

                // If all is successful status code = 0
                return (success ? 0 : 1);
            });

            app.HelpOption("-? | -h | --help");
            var result = app.Execute(args);
            Environment.Exit(result);
        }
    }
}
