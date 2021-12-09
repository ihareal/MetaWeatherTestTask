using System.Net.Http;
using System.Threading.Tasks;

namespace DebugTest.Services.Abstract
{
    public interface IRequestSender
    {
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
