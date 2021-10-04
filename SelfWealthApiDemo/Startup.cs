using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SelfWealthApiDemo
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
                options.Configuration = Configuration.GetValue<string>("RedisConnection");
            });
            services.AddScoped<ICacheProvider, CacheProvider>();
            services.AddScoped<ICacheUserService, CacheUserService>();            
        }
                
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            string logfileName = LogDir.GetLogFilePath(Configuration.GetValue<string>("LogDirPath"));
            loggerFactory.AddFile(logfileName);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthorization();

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
