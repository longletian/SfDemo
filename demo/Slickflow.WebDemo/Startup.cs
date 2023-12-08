using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using VueCliMiddleware;

namespace Slickflow.WebDemo
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
            services.AddControllers();
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});
            services.AddMvc()
                .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);

            //����
            services.AddCors(options =>
            {
                // Policy �������Զ���ģ������Լ���
                options.AddPolicy("AllowAll", policy =>
                        {
                            //�趨���������ʵĵ�ַ���ж���Ļ��� `,` ����
                            policy
                            //.WithOrigins("http://localhost:8080")   //�����Ӧǰ��վ���ַ
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                                //.AllowCredentials();
                        });
            });
            var dbType = ConfigurationExtensions.GetConnectionString(Configuration, "WfDBConnectionType");
            var sqlConnectionString = ConfigurationExtensions.GetConnectionString(Configuration, "WfDBConnectionString");
            Slickflow.Data.DBTypeExtenstions.InitConnectionString(dbType, sqlConnectionString);

            //set default language
            Slickflow.Module.Localize.LocalizeHelper.SetDefault();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            //app.UseSpaStaticFiles();
            app.UseAuthorization()
                .UseCors("AllowAll");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });
           // app.UseSpa(spa =>
           //{
           //    spa.Options.SourcePath = "ClientApp";

           //    if (env.IsDevelopment())
           //    {
           //        spa.UseVueCli(npmScript: "serve", forceKill: true);
           //    }
           //});
        }
    }
}
