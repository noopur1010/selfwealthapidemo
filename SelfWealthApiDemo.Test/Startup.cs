using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SelfWealthApiDemo.Test
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
            services.AddControllers().AddApplicationPart(Assembly.Load("SelfWealthApiDemo")).AddControllersAsServices();
            
            services.AddTransient<ErrorHandlerMiddleware>();
            services.AddControllers();
            //services.AddSwaggerDocument();
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "SelfWealth Web API Demo";
                    document.Info.Description = "API demo with Github and Redis Cache";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "Nupur Shah",
                        Email = "noopur1010@gmail.com",
                        Url = "https://www.linkedin.com/in/nupur-shah-277a5926/"
                    };
                };
            });
            services.AddHttpClient<IGithubApiService, GithubApiService>(c =>
            {
                c.BaseAddress = new Uri("https://api.github.com");
                string agent = "ClientDemo/1.0.0.1 test user agent DefaultRequestHeaders";
                c.DefaultRequestHeaders.Add("User-Agent", agent);
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            services.AddScoped<ICacheProvider, CacheProvider>();
            services.AddScoped<ICacheUserService, CacheUserService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
