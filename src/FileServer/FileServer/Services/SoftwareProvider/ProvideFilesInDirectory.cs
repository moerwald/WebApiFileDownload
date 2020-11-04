using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.Services.SoftwareProvider
{
    public class ProvideFilesInDirectory : IProvideFilesInDirectory
    {
        public Task<IEnumerable<string>> GetFilesAsync(string path, 
                                                       string searchPattern, 
                                                       CancellationToken token) => 
            Task.Run<IEnumerable<string>>(() => Directory.GetFiles(path, searchPattern), 
                                          token);
    }
}