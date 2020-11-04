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
    }
}
