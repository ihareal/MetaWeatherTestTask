using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DebugTest.Helpers
{
    public class ReadResponseMessageHelper : IReadResponseMessageHelper
    {
        public async Task<T> ReadContentAsync<T>(HttpResponseMessage httpResponseMessage)
        {
            using (var streamReader = new StreamReader(await httpResponseMessage.Content.ReadAsStreamAsync()))
            {
                var data = await streamReader.ReadToEndAsync();

                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
