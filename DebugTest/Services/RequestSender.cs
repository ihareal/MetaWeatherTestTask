using DebugTest.Services.Abstract;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace DebugTest.Services
{
    public class RequestSender : IRequestSender
    {
        private readonly HttpClient _httpClient;

        public RequestSender(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _httpClient.GetAsync(url);
        }

        /// <summary>
        /// Represents retry policy for 429(TooManyRequests) exception.
        /// </summary>
        /// <returns><see cref="IAsyncPolicy{HttpResponseMessage}"/></returns>
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                                                                            retryAttempt)));
        }
    }
}
