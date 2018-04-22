using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Register.Options.Asp.Net.Core.Register.Options.Asp.Net.Core;
using RegisterOptionsDemo.Options;

namespace RegisterOptionsDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureOptionsFromEntyAssembly(Configuration);

            //You can specify specific assembly to search options classes in.
            //   services.ConfigureOptionsFromAssembly(Configuration, typeof(int).Assembly);

            //Alternatively, you can register options one by one explicitly
            //   services.ConfigureOption<AOptions>(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var aoptions = app.ApplicationServices.GetRequiredService<IOptions<PersonOptions>>();
        }
    }
}
