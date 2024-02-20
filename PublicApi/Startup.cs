using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TaskStorage.Converters;
using TaskStorage.Interfaces;
using TaskStorage.Services;
using TaskStorage.Utils;

namespace TaskStorage
{
    public class Startup(IConfiguration configuration)
    {
        private IConfiguration Configuration { get; } = configuration;

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
            services.AddScoped<IStorageService, StorageService>();
            services.AddSingleton<YouTrackHttpClient>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}