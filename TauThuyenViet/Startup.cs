using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;


namespace TauThuyenViet
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
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
           
            services.AddMvc().AddJsonOptions(x =>
            {
                //fix lỗi lấy dữ liệu menu đa cấp
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            string connection = @"Data Source=.;Initial Catalog=TauThuyenViet;Persist Security Info=True;User ID=sa;Password=shc@1234";
            services.AddDbContext<DBContext>(x => x.UseSqlServer(connection));

            services.AddHttpClient("default", client =>
            {
                //client.BaseAddress = new Uri("http://localhost:63028/");
                client.BaseAddress = new Uri("http://tauthuyenviet.shc.com/");

                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
