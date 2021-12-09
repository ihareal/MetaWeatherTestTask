using DebugTest.Helpers;
using DebugTest.Services;
using DebugTest.Services.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DebugTest
{
    public static class DependencyRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
             .AddSingleton<IMetaWeatherService, MetaWeatherService>()
             .AddSingleton<IFileService, FileService>()
             .AddSingleton<IReadResponseMessageHelper, ReadResponseMessageHelper>();

            /// Adding http client factory to the DI.
            /// According to the MS best practices.
            /// <see cref="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests"/>
            services.AddHttpClient<IRequestSender, RequestSender>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(RequestSender.GetRetryPolicy());

            return services;
        }        
    }
}
