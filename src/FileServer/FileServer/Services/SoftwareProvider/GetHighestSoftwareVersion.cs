using FileServer.Config;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.Services.SoftwareProvider
{
    public class GetHighestSoftwareVersion : IProvideSoftware
    {
        private readonly IProvideFilesInDirectory _provideFilesInDirectory;
        private readonly FileScanConfig _config;
        private readonly Regex _regex;

        public GetHighestSoftwareVersion(IProvideFilesInDirectory provideFilesInDirectory,
                                         IOptions<FileScanConfig> options)
        {
            _provideFilesInDirectory = provideFilesInDirectory;
            _config = options.Value;
            _regex = new Regex(_config.Filter, RegexOptions.Compiled);
        }

        public async Task<string> GetPathForLatestSoftwareAsync(CancellationToken token)
        {
            var foundFiles = new List<string>();

            foreach (var d  in _config.Directories)
                foundFiles.AddRange(
                    await _provideFilesInDirectory.GetFilesAsync(d, "*", token));

            var matches = foundFiles.Select(f => new { Match = _regex.Match(f), FilePath = f })
                                    .Where(x => x.Match.Success);

            // Todo add natural sort
            var pathToHighestVersion = matches.OrderByDescending(m => m.Match.Groups[_config.CaptureGroupName].Value).First();

            // return biggest one
            return pathToHighestVersion.FilePath;
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
