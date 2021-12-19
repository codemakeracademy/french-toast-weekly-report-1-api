using CM.WeeklyTeamReport.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CM.WeeklyTeamReport.WebApp
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddTransient<IRepository<Company>, CompanyRepository>();
            services.AddTransient<IRepository<TeamMember>, TeamMemberRepository>();
            services.AddTransient<IRepository<WeeklyReport>, WeeklyReportRepository>();
            services.AddTransient<IRepository<ReportsFromTo>, ReportsFromToRepository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CM.WeeklyTeamReport.WebApp",
                    Version = "v1",
                    Description = "ASP.NET Core Web API"
                });
            });

            var _aspnetCorsFromOut = Environment.GetEnvironmentVariable("ASPNET_CORS")?.Split(",") != null ? Environment.GetEnvironmentVariable("ASPNET_CORS")?.Split(",") : new string[] { };
            var _aspnetCorsDefault = new string[] { "https://weekly-report-01.digitalocean.ankocorp.com", "https://localhost:3000", "http://localhost:3000" };
            var corsDomains = _aspnetCorsDefault.Concat(_aspnetCorsFromOut).ToArray();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins(corsDomains).AllowAnyHeader().AllowAnyMethod();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
                });
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
