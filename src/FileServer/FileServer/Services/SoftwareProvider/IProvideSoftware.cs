using System.Threading;
using System.Threading.Tasks;

namespace FileServer.Services.SoftwareProvider
{
    public interface IProvideSoftware
    {
        Task<string> GetPathForLatestSoftwareAsync(CancellationToken token);
    }
}
