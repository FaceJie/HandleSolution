using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonHelper;
using DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OAuthLogin;

namespace HandleWeb
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
            services.AddControllersWithViews();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
                app.UseHsts();
            }

            #region 注入配置

            //对接平台的处理
            var Credentials = Configuration.GetSection("Credentials").Get<CredentialsSetting>();
            var ConnStrings = Configuration.GetSection("DataBaseUrl").Get<DbSetting>();
            LoginProvider.UseWeibo(Credentials.Weibo.client_id, Credentials.Weibo.client_secret);
            LoginProvider.UseQQ(Credentials.QQ.client_id, Credentials.QQ.client_secret);
            LoginProvider.UseFaceBook(Credentials.FaceBook.client_id, Credentials.FaceBook.client_secret);
            LoginProvider.UseWechat(Credentials.Wechat.client_id, Credentials.Wechat.client_secret);
            LoginProvider.UseKakao(Credentials.KaKao.client_id);

            //连接配置注入到基础层
            DbProvider.UseDb(ConnStrings.ConnectionString.databaseType, ConnStrings.ConnectionString.connectionStr, ConnStrings.ConnectionString.isOpenOrg);

            //把IHostingEnvironment 注入FilesHelper
            app.UseWkMvcDI();
            #endregion

            #region 启用多个配置
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            #endregion

            //CORE标准库3.0的路由
            app.UseEndpoints(endpoints =>
            {
                //自定义区域路由
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
