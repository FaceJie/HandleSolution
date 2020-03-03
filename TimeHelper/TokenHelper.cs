using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonHelper
{
    public static class TokenHelper<T> where T : class
    {
        /// <summary>
        /// 密钥
        /// </summary>
        private static readonly string secret = "ABCDEFGHIJK";
        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="t">泛型对象</param>
        /// <returns></returns>
        public static string CreateTokenAuth(T t)
        {
            JsonNetSerializer serializer = new JsonNetSerializer();
            JwtBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            JwtEncoder encoder = new JwtEncoder(new HMACSHA256Algorithm(), serializer, urlEncoder);
            var token = encoder.Encode(t, secret);
            return token;
        }
        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="token">客户端发过来的Token</param>
        public static T VlidateToken(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);//版本问题将影响传入的参数，此处使用JWT 5.31
            T json = decoder.DecodeToObject<T>(token, secret, verify: true);
            return json;
        }
    }
}
