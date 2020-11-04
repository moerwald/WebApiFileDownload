using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.Services.SoftwareProvider
{
    public class GetHighestSoftwareVersion : IProvideSoftware
    {
        private readonly CancellationToken _token;
        private readonly IProvideFilesInDirectory _provideFilesInDirectory;

        public GetHighestSoftwareVersion(CancellationToken token, IProvideFilesInDirectory provideFilesInDirectory)
        {
            _token = token;
            _provideFilesInDirectory = provideFilesInDirectory;
        }

        public async Task<string> GetPathForLatestSoftwareAsync(string directoryToCheck)
        {
            var files = await _provideFilesInDirectory.GetFilesAsync(directoryToCheck, "*.zip", _token);

            // Order files format: blablabla.1.2.3.4.zip
            var filesWithVersionInName = files.Where(f => Regex.IsMatch(f, @"(?<version>(\d\.){ 3}\d)")).Select(f => f);


            // return biggest one
            return null;
        }


        //public void CheckForNewFiles()
        //{
        //    try
        //    {
        //        using var watcher = new FileSystemWatcher
        //        {
        //            Path = @"C:\temp\software",

        //            // Watch for changes in LastAccess and LastWrite times, and
        //            // the renaming of files or directories.
        //            NotifyFilter = NotifyFilters.LastAccess
        //                             | NotifyFilters.LastWrite
        //                             | NotifyFilters.FileName
        //                             | NotifyFilters.DirectoryName,

        //            // Only watch text files.
        //            Filter = "*.zip"
        //        };

        //        // Add event handlers.
        //        //watcher.Changed += OnChanged;
        //        watcher.Created += OnChanged;
        //        //watcher.Deleted += OnChanged;
        //        //watcher.Renamed += OnRenamed;

        //        // Begin watching.
        //        watcher.EnableRaisingEvents = true;

        //        // Wait for the user to quit the program.
        //        while (_token.IsCancellationRequested == false)
        //            Thread.Sleep(1000);
        //    }
        //    catch (Exception)
        //    {

        //    }

        //}

        //private void OnChanged(object sender, FileSystemEventArgs e)
        //{
        //    e.FullPath;
        //}
    }
}
