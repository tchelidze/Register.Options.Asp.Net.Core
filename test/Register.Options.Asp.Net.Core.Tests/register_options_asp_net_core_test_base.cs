using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Register.Options.Asp.Net.Core.Tests
{
    public class register_options_asp_net_core_test_base
    {
        protected readonly IConfiguration Configuration;
        protected readonly IServiceCollection ServiceCollection;

        public register_options_asp_net_core_test_base()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ServiceCollection = new ServiceCollection();
        }
    }
}