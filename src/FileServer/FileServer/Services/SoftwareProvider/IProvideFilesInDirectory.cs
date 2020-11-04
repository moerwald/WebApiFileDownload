using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FileServer.Services.SoftwareProvider
{
    public interface IProvideFilesInDirectory
    {
        Task<IEnumerable<string>> GetFilesAsync(string path, string searchPattern, CancellationToken token);
    }
}