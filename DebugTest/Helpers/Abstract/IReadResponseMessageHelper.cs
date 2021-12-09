using System.Net.Http;
using System.Threading.Tasks;

namespace DebugTest.Helpers
{
    public interface IReadResponseMessageHelper
    {
        Task<T> ReadContentAsync<T>(HttpResponseMessage httpResponseMessage);
    }
}