using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OAuthLogin;

namespace HandleSolution
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            #region 授权类平台注册

            #endregion
            var Credentials = Configuration.GetSection("Credentials").Get<CredentialsSetting>();

            LoginProvider.UseWeibo(Credentials.Weibo.client_id, Credentials.Weibo.client_secret);

            LoginProvider.UseQQ(Credentials.QQ.client_id, Credentials.QQ.client_secret);

            LoginProvider.UseFaceBook(Credentials.FaceBook.client_id, Credentials.FaceBook.client_secret);

            LoginProvider.UseWechat(Credentials.Wechat.client_id, Credentials.Wechat.client_secret);

            LoginProvider.UseKakao(Credentials.KaKao.client_id);


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
