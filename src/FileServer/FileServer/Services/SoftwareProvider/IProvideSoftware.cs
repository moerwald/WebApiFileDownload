using System.Threading.Tasks;

namespace FileServer.Services.SoftwareProvider
{
    public interface IProvideSoftware
    {
        Task<string> GetPathForLatestSoftwareAsync(string directoryToCheck);
    }
}
