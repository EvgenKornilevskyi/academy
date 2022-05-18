using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResultsManager.Tests.Common.Configuration.Options;
using ResultsManager.Tests.Common.Configuration.Services.Http;
using ResultsManager.Tests.Common.Configuration.Helpers;

namespace Tests.Common.Configuration
{
    public static class TestServices
    {
        private static ServiceProvider ServiceProvider { get; set; }
        public static HttpClientFactory HttpClientFactory { get; set; }
        public static TestResultCollector TestResultCollector { get; set; }
        public static string AuthorizationToken { get; private set; }

        public static string NewId => Guid.NewGuid().ToString();
        public static int Rand => new Random(Environment.TickCount).Next(int.MaxValue);

        static TestServices() => ConfigureServices();

        private static void ConfigureServices()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            AuthorizationToken = configuration["AuthorizationToken"];

            ServiceProvider = GetServiceProvider(configuration);
            HttpClientFactory = ServiceProvider.GetService<HttpClientFactory>();
            TestResultCollector = ServiceProvider.GetService<TestResultCollector>();
        }

        private static ServiceProvider GetServiceProvider(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            //Add all options
            services.Configure<TestServicesOptions>(configuration.GetSection("TestServicesOptions"));
            services.AddSingleton<HttpClientFactory>();
            services.AddSingleton<TestResultCollector>();

            return services.BuildServiceProvider();
        }
    }
}
