using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonHelper;
using DataBase;
using HandleWeb.Configuration;
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



        #region �м�������� ����˳��
        /*
         *  һ���Ӧ���е� Startup.Configure ��������м������˳��
            Exception/error handling
            HTTP Strict Transport Security Protocol
            HTTPS redirection
            Static file server
            Cookie policy enforcement
            Authentication
            Session
            MVC
         */
        #endregion
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

            //ע���м��
            app.ConfigureMiddlewares();

            #region ע������

            //�Խ�ƽ̨�Ĵ���
            var Credentials = Configuration.GetSection("Credentials").Get<CredentialsSetting>();
            var ConnStrings = Configuration.GetSection("DataBaseUrl").Get<DbSetting>();
            LoginProvider.UseWeibo(Credentials.Weibo.client_id, Credentials.Weibo.client_secret);
            LoginProvider.UseQQ(Credentials.QQ.client_id, Credentials.QQ.client_secret);
            LoginProvider.UseFaceBook(Credentials.FaceBook.client_id, Credentials.FaceBook.client_secret);
            LoginProvider.UseWechat(Credentials.Wechat.client_id, Credentials.Wechat.client_secret);
            LoginProvider.UseKakao(Credentials.KaKao.client_id);

            //��������ע�뵽������
            DbProvider.UseDb(ConnStrings.ConnectionString.databaseType, ConnStrings.ConnectionString.connectionStr, ConnStrings.ConnectionString.isOpenOrg);

            //��IHostingEnvironment ע��FilesHelper
            app.UseWkMvcDI();
            #endregion


            #region ���ö������
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            #endregion

            //CORE��׼��3.0��·��
            app.UseEndpoints(endpoints =>
            {
                //�Զ�������·��
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
