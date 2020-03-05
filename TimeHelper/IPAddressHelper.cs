using DnsClient;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SharedHelper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class IPAddressHelper
    {
        public static string GetRequestIP(HttpContext context, bool tryUseXForwardHeader = true)
        {
            string ip = null;

            // todo support new "Forwarded" header (2014) https://en.wikipedia.org/wiki/X-Forwarded-For

            // X-Forwarded-For (csv list):  Using the First entry in the list seems to work
            // for 99% of cases however it has been suggested that a better (although tedious)
            // approach might be to read each IP from right to left and use the first public IP.
            // http://stackoverflow.com/a/43554000/538763
            //
            if (tryUseXForwardHeader)
                ip = GetHeaderValueAs<string>(context, "X-Forwarded-For").SplitCsv().FirstOrDefault();

            if (ip.IsNullOrWhitespace() && context?.Connection?.RemoteIpAddress != null)
                ip = context.Connection.RemoteIpAddress.ToString();

            if (ip.IsNullOrWhitespace())
                ip = GetHeaderValueAs<string>(context, "REMOTE_ADDR");

            if (ip.IsNullOrWhitespace())
                throw new Exception("Unable to determine caller's IP.");

            return ip;
        }

        public static T GetHeaderValueAs<T>(HttpContext context, string headerName)
        {
            StringValues values;

            if (context.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                string rawValues = values.ToString();   // writes out as Csv when there are multiple.

                if (!string.IsNullOrEmpty(rawValues))
                    return (T)Convert.ChangeType(values.ToString(), typeof(T));
            }
            return default(T);
        }
    }

    public static class DnsHelper
    {
        /// <summary>
        /// 获取本地的IP地址
        /// </summary>
        /// <param name="ipv4">是否ipv4</param>
        /// <returns></returns>
        public static async Task<string> GetIpAddressAsync(bool ipv4 = true)
        {
            var client = new LookupClient();
            var hostEntry = await client.GetHostEntryAsync(Dns.GetHostName());
            IPAddress ipaddress = null;
            if (ipv4)
            {
                ipaddress = (from ip in hostEntry.AddressList
                             where
(!IPAddress.IsLoopback(ip) && ip.AddressFamily == AddressFamily.InterNetwork)
                             select ip)
                             .FirstOrDefault();
            }
            else
            {
                ipaddress = (from ip in hostEntry.AddressList
                             where
(!IPAddress.IsLoopback(ip) && ip.AddressFamily == AddressFamily.InterNetworkV6)
                             select ip)
                             .FirstOrDefault();
            }
            if (ipaddress != null)
            {
                return ipaddress.ToString();
            }

            return string.Empty;
        }
    }
}
