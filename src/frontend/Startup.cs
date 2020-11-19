using common;
using frontend.Lib;
using Hangfire;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace frontend
{
    public class Startup
    {
        private static ConnectionMultiplexer RedisConnection { get; set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            RedisConnection = ConnectionMultiplexer.Connect(Configuration.GetConnectionString("redis"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureContainer(ServiceRegistry services)
        {
            // Also exposes Lamar specific registrations
            // and functionality
            services.IncludeRegistry<CommonRegistry>();
            services.Scan(s =>
            {
                s.TheCallingAssembly();
                s.WithDefaultConventions();
            });
            services.AddControllersWithViews();
            services.AddHangfire(options =>
            {
                options.UseRedisStorage(RedisConnection);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseHangfireDashboard("/dashbard", new DashboardOptions
            {
                Authorization = new[] {new DashboardAuthorizationFilter()}
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
