using Microsoft.Extensions.DependencyInjection;
using Grains;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => OrleansClient.ClusterClient)
                    .AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseMvc(routes => {
                routes.MapRoute("default", "");
            });
        }
    }
}
