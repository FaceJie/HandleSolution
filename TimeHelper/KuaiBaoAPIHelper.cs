using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CommonHelper//地址、人、电话解析
{
    public static class KuaiBaoAPIHelper
    {
        private const string app_id = "104734";
        private const string api_key = "5bc8f9148c4da9ff93d50b83b1f89047857a9d2b";
        private const String host = "https://kop.kuaidihelp.com";
        private const String path = "/api";
        private const String method = "POST";
        /// <summary>
        /// 智能解析
        /// </summary>
        /// <param name="strAddress"></param>
        /// <returns></returns>
        public static JObject GetResolutionAddress(string strAddress)
        {
            var url = "https://kop.kuaidihelp.com/api";
            JObject jdata = new JObject();
            jdata.Add("multimode", false);
            jdata.Add("text", strAddress);
            var result = Post(url, JsonConvert.SerializeObject(jdata));
            var jsonResult = JsonConvert.DeserializeObject<JObject>(result);
            return jsonResult;
        }
        public static string Post(string url, string text)
        {
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            var ts = GetTimeStampToString();
            var sign = MD5Encrypt(app_id + "cloud.address.resolve" + ts + "" + api_key);
            //组合参数
            string bodys = "app_id=" + app_id + "&method=cloud.address.resolve&ts=" + ts + "&sign=" + sign;
            bodys = bodys + "&data=" + text;

            string querys = "";
            if (0 < querys.Length)
            {
                url = url + "?" + querys;
            }

            if (host.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = "post";
            //根据API的要求，定义相对应的Content-Type
            httpRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            if (0 < bodys.Length)
            {
                byte[] data = Encoding.UTF8.GetBytes(bodys);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            try
            {
                var resultStream = (httpRequest.GetResponse() as HttpWebResponse);
                string result = "";
                //获取响应内容
                using (StreamReader reader = new StreamReader(resultStream.GetResponseStream(), Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
                return result;
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            return "";
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        public static string GetTimeStampToString()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        public static string MD5Encrypt(this string text)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
