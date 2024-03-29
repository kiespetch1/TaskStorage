using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TaskStorage.Converters;
using TaskStorage.Interfaces;
using TaskStorage.Services;

namespace TaskStorage
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
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    var customConverters = new List<JsonConverter>
                    {
                        new CommentConverter(),
                        new CustomFieldsConverter(),
                        new WorkLogConverter()
                    };

                    foreach (var converter in customConverters)
                    {
                        options.SerializerSettings.Converters.Add(converter);
                    }
                });
            
            services.AddScoped<IUploadService, UploadService>();
            services.AddHttpClient();
            services.AddScoped<YouTrackHttpClient>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            
            app.UseRouting(); 
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); 
            });
        }
    }
}