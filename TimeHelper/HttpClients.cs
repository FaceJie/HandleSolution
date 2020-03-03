using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace CommonHelper
{
    public class HttpClients
    {


        #region GET，用？连接参数
        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="url">Get方法的URL</param>
        /// <returns></returns>
        private static string Get(string url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.ContentType = "multipart/form-data";
                req.Accept = "*/*";
                req.UserAgent = "";
                req.Timeout = 10000;
                req.Method = "Get";
                req.KeepAlive = true;
                HttpWebResponse response = req.GetResponse() as HttpWebResponse;
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                strResult = ex.ToString();
            }
            return strResult;
        }
        #endregion

        #region Post方法，请求体包含在body中
        public static string Post(string url, Dictionary<string, string> dic, string contentType)
        {
            string resultCryptoStr = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                using (var content = new MultipartFormDataContent())
                {
                    var formDatas = GetFormDataByteArrayContent(GetNameValueCollection(dic));
                    Action<List<ByteArrayContent>> act = (dataContents) =>
                    {
                        foreach (var byteArrayContent in dataContents)
                        {
                            content.Add(byteArrayContent);
                        }
                    };
                    act(formDatas);
                    try
                    {
                        HttpResponseMessage httpResponseMessage = client.PostAsync(url, content).Result;
                        resultCryptoStr = httpResponseMessage.Content.ReadAsStringAsync().Result;
                    }
                    catch (Exception ex)
                    {
                        resultCryptoStr = ex.ToString();
                    }
                }
            }
            return resultCryptoStr;
        }
        /// <summary>
        /// 获取键值集合对应的ByteArrayContent集合
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private static List<ByteArrayContent> GetFormDataByteArrayContent(NameValueCollection collection)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var key in collection.AllKeys)
            {
                var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(collection[key]));
                dataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = key
                };
                list.Add(dataContent);
            }
            return list;
        }
        /// <summary>
        /// 从DataGridView中获取键值对集合
        /// </summary>
        /// <param name="gv"></param>
        /// <returns></returns>
        private static NameValueCollection GetNameValueCollection(Dictionary<string, string> dics)
        {
            NameValueCollection collection = new NameValueCollection();
            foreach (var dic in dics)
            {
                if (dic.Value != null)
                {
                    collection.Add(dic.Key.ToString(),
                        dic.Value == null ? string.Empty : dic.Value.ToString());
                }
            }
            return collection;
        }
        #endregion


        //发起Http请求
        public static string HttpPost(string url, Stream data, IDictionary<object, string> headers = null)
        {
            System.Net.WebRequest request = HttpWebRequest.Create(url);
            request.Method = "POST";
            if (data != null)
                request.ContentLength = data.Length;
            //request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            if (headers != null)
            {
                foreach (var v in headers)
                {
                    if (v.Key is HttpRequestHeader)
                        request.Headers[(HttpRequestHeader)v.Key] = v.Value;
                    else
                        request.Headers[v.Key.ToString()] = v.Value;
                }
            }
            HttpWebResponse response = null;
            try
            {
                // Get the response.
                response = (HttpWebResponse)request.GetResponse();
                // Display the status.
                Console.WriteLine(response.StatusDescription);
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);
                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
                return responseFromServer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(response.StatusDescription);
                return e.Message;
            }
        }


        /// <summary>
        /// Post/get 提交调用抓取
        /// </summary>
        /// <param name="url">提交地址</param>
        /// <param name="param">参数</param>
        /// <returns>string</returns>
        public static string WebRequestPostOrGet(string sUrl, string sParam)
        {
            byte[] bt = System.Text.Encoding.UTF8.GetBytes(sParam);

            Uri uriurl = new Uri(sUrl);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uriurl);
            req.Method = "Post";
            req.Timeout = 120 * 1000;
            req.ContentType = "application/x-www-form-urlencoded;";
            req.ContentLength = bt.Length;

            using (Stream reqStream = req.GetRequestStream())//using 使用可以释放using段内的内存
            {
                reqStream.Write(bt, 0, bt.Length);
                reqStream.Flush();
            }
            try
            {
                using (WebResponse res = req.GetResponse())
                {
                    //在这里对接收到的页面内容进行处理 
                    Stream resStream = res.GetResponseStream();
                    StreamReader resStreamReader = new StreamReader(resStream, System.Text.Encoding.UTF8);
                    string resLine;
                    System.Text.StringBuilder resStringBuilder = new System.Text.StringBuilder();
                    while ((resLine = resStreamReader.ReadLine()) != null)
                    {
                        resStringBuilder.Append(resLine + System.Environment.NewLine);
                    }
                    resStream.Close();
                    resStreamReader.Close();
                    return resStringBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 根据
        /// </summary>
        /// <param name="url">Get方法的URL</param>
        /// <param name="timeout">超时时间默认2秒</param>
        /// <returns></returns>
        public static string GetInfoByURL(string url, int timeout = 2000)
        {
            string strResult = "";
            try
            {
                HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
                req.ContentType = "multipart/form-data";
                req.Accept = "*/*";
                req.UserAgent = "";
                req.Timeout = timeout;
                req.Method = "GET";
                req.KeepAlive = true;
                HttpWebResponse response = req.GetResponse() as HttpWebResponse;
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                strResult = ex.ToString();
            }
            return strResult;
        }
    }
}
