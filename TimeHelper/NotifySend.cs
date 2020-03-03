using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CommonHelper
{
    public static class NotifySend
    {
        //网易云信分配的账号，请替换你在管理后台应用下申请的Appkey
        private static String appKey = "dc8b5d76ca7015f53f3bec361ade561b";

        //网易云信分配的密钥，请替换你在管理后台应用下申请的appSecret
        private static String appSecret = "ea5125c41272";

        //随机数（最大长度128个字符）
        private static String nonce = "12345";
        /// <summary>
        ///发送验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public static Content SendCode(string mobile)
        {
            string msg = "";
            String url = "https://api.netease.im/sms/sendcode.action";
            url += "?templateid=14845511&mobile=" + mobile;//请输入正确的手机号
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            //当前UTC时间戳，从1970年1月1日0点0 分0 秒开始到现在的秒数(String)
            String curTime = ticks.ToString();
            //SHA1(AppSecret + Nonce + CurTime),三个参数拼接的字符串，进行SHA1哈希计算，转化成16进制字符(String，小写)
            String checkSum = getCheckSum(appSecret, nonce, curTime);
            IDictionary<object, String> headers = new Dictionary<object, String>();
            headers["AppKey"] = appKey;
            headers["Nonce"] = nonce;
            headers["CurTime"] = curTime;
            headers["CheckSum"] = checkSum;
            headers["ContentType"] = "application/x-www-form-urlencoded;charset=utf-8";
            //执行Http请求
            msg = HttpClients.HttpPost(url, null, headers);
            return JsonConvert.DeserializeObject<Content>(msg);
        }
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Content CheckCode(string mobile, string code)
        {
            string msg = "";
            String url = "https://api.netease.im/sms/verifycode.action";
            url += "?mobile=" + mobile + "&code=" + code;//请输入正确的手机号
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            //当前UTC时间戳，从1970年1月1日0点0 分0 秒开始到现在的秒数(String)
            String curTime = ticks.ToString();
            //SHA1(AppSecret + Nonce + CurTime),三个参数拼接的字符串，进行SHA1哈希计算，转化成16进制字符(String，小写)
            String checkSum = getCheckSum(appSecret, nonce, curTime);
            IDictionary<object, String> headers = new Dictionary<object, String>();
            headers["AppKey"] = appKey;
            headers["Nonce"] = nonce;
            headers["CurTime"] = curTime;
            headers["CheckSum"] = checkSum;
            headers["ContentType"] = "application/x-www-form-urlencoded;charset=utf-8";
            msg = HttpClients.HttpPost(url, null, headers);
            return JsonConvert.DeserializeObject<Content>(msg);
        }

        /// <summary>
        /// 消息发送，需要封装，验证
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="TravelAgency"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static Content SendMessage(string mobile, string TravelAgency, string Url)
        {
            String url = "https://api.netease.im/sms/sendtemplate.action";
            url += string.Format("?templateid=14842515&mobiles=['{0}']" + "&params=['{1}','{2}']", mobile, TravelAgency, Url);//请输入正确的手机号 
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1);
            Int32 ticks = System.Convert.ToInt32(ts.TotalSeconds);
            String curTime = ticks.ToString();
            String checkSum = getCheckSum(appSecret, nonce, curTime);
            IDictionary<object, String> headers = new Dictionary<object, String>();
            headers["AppKey"] = appKey;
            headers["Nonce"] = nonce;
            headers["CurTime"] = curTime;
            headers["CheckSum"] = checkSum;
            headers["ContentType"] = "application/x-www-form-urlencoded;charset=utf-8";
            var msg = HttpClients.HttpPost(url, null, headers);
            return JsonConvert.DeserializeObject<Content>(msg);
        }

        public class Content
        {
            public string code { get; set; }
            public string desc { get; set; }
        }
        // 计算并获取CheckSum
        public static String getCheckSum(String appSecret, String nonce, String curTime)
        {
            byte[] data = Encoding.Default.GetBytes(appSecret + nonce + curTime);
            byte[] result;

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);

            return getFormattedText(result);
        }
        private static String getFormattedText(byte[] bytes)
        {
            char[] HEX_DIGITS = { '0', '1', '2', '3', '4', '5',
            '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            int len = bytes.Length;
            StringBuilder buf = new StringBuilder(len * 2);
            for (int j = 0; j < len; j++)
            {
                buf.Append(HEX_DIGITS[(bytes[j] >> 4) & 0x0f]);
                buf.Append(HEX_DIGITS[bytes[j] & 0x0f]);
            }
            return buf.ToString();
        }
    }
}
