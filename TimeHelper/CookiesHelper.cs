using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonHelper
{
    public static class CookiesHelper<T> where T : class
    {

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <returns></returns>
        public static T Current(string cookiesKey)
        {
            try
            {
                T obj = TokenHelper<T>.VlidateToken(GetCookies(cookiesKey));
                return obj;
            }
            catch
            {
                return null;
            }
        }

        public static HttpContextAccessor context = new HttpContextAccessor();
        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>   
        public static void SetCookies(string key, string value, int minutes = 30)
        {
            context.HttpContext.Response.Cookies.Append(key, value, new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(minutes)
            });
        }
        /// <summary>
        /// 设置本地cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>  
        /// <param name="minutes">过期时长，单位：分钟</param>      
        public static void SetCookies(string key, string value)
        {
            context.HttpContext.Response.Cookies.Append(key, value);
        }
        /// <summary>
        /// 删除指定的cookie
        /// </summary>
        /// <param name="key">键</param>
        public static void DeleteCookies(string key)
        {
            context.HttpContext.Response.Cookies.Delete(key);
        }
        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        public static string GetCookies(string key)
        {
            context.HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }
    }
}
