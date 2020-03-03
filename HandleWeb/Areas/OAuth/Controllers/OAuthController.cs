using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OAuthLogin;

namespace HandleWeb.Areas.OAuth.Controllers
{
    public class OAuthController : Controller
    {

        IHttpContextAccessor _contextAccessor;
        public OAuthController(IHttpContextAccessor
                  contextAccessor)
        { this._contextAccessor = contextAccessor; }

        [Area("OAuth")]
        public IActionResult QQ()
        {
            var res = new QQ(_contextAccessor).Authorize();

            if (res != null && res.code == 0)
            {
                return RedirectToLogin(new
                {
                    channel = "qq",
                    code = 0,
                    user = new
                    {
                        uid = res.result.Value<string>("openid"),
                        name = res.result.Value<string>("nickname"),
                        img = res.result.Value<string>("figureurl"),
                        token = res.token
                    }
                });
            }

            return View();
        }
        public IActionResult Wechat()
        {
            var res = new Wechat(_contextAccessor).Authorize();

            if (res != null && res.code == 0)
            {
                return RedirectToLogin(new
                {
                    channel = "wechat",
                    code = 0,
                    user = new
                    {
                        uid = res.result.Value<string>("uid"),
                        name = res.result.Value<string>("nickname"),
                        img = res.result.Value<string>("headimgurl"),
                        token = res.token
                    }
                });
            }

            return View();
        }

        public IActionResult Weibo()
        {
            var res = new Weibo(_contextAccessor).Authorize();

            if (res != null && res.code == 0)
            {
                return RedirectToLogin(new
                {
                    channel = "weibo",
                    code = 0,
                    user = new
                    {
                        uid = res.result.Value<string>("idstr"),
                        name = res.result.Value<string>("name"),
                        img = res.result.Value<string>("profile_image_url"),
                        token = res.token
                    }
                });
            }

            return View();
        }

        public IActionResult Facebook()
        {
            var res = new Facebook(_contextAccessor).Authorize();

            if (res != null && res.code == 0)
            {
                return RedirectToLogin(new
                {
                    channel = "facebook",
                    code = 0,
                    user = new
                    {
                        uid = res.result.Value<string>("id"),
                        name = res.result.Value<string>("name"),
                        img = res.result["picture"]["data"].Value<string>("url"),
                        token = res.token
                    }
                });
            }

            return View();
        }

        public IActionResult Kakao()
        {
            var res = new Kakao(_contextAccessor).Authorize();

            if (res != null && res.code == 0)
            {
                return RedirectToLogin(new
                {
                    channel = "kakao",
                    code = 0,
                    user = new
                    {
                        uid = res.result.Value<string>("uid"),
                        name = res.result.Value<string>("nickname"),
                        img = res.result.Value<string>("thumbnail_image"),
                        token = res.token
                    }
                });
            }

            return View();
        }

        RedirectResult RedirectToLogin(object _entity)
        {
            var OAuthResult = JsonConvert.SerializeObject(_entity);
            // 跳转的页面，union参数后面是编码后的用户数据
            var url = "/login?union=" + WebUtility.UrlEncode(OAuthResult);
            return Redirect(url);
        }
    }
}