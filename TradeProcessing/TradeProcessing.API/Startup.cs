using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradeProcessing.DataAceess;
using TradeProcessing.Processor;
using TradeProcessing.Processor.Interfaces;

namespace TradeProcessing.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IComTraderCsvProcessor, ComTraderCsvProcessor>();
            services.AddScoped<IJAOCsvProcessor, JAOCsvProcessor>();
            services.AddScoped<IEpexParser, EpexParser>();
            services.AddControllers();

            services.AddDbContext<HeliosContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("TradeContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
