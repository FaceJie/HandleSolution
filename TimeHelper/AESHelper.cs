using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CommonHelper
{
    public static class AESHelper
    {
        /// <summary>
        /// Key密钥
        /// </summary>
        //private static readonly string KeySecretkey = "zyqcosta@10$@@+$++@@_$%^^*EasyGo";
        /// <summary>
        /// 测试指定KEY
        /// </summary>
        private static readonly string AssignKey = "XNPsYtRErro2akE9u/dtGQ==";
        /// <summary>
        /// 数据解密密钥
        /// </summary>
        private static readonly string Secretkey = "EasyGozyqcosta@10$@@+$++@@_$%^^*";


        public static bool Verification(string Key)
        {
            if (Key != AssignKey)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///  AES 加密
        /// </summary>
        /// <param name="str">明文（待加密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static string AesEncrypt(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);

            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(Secretkey),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = rm.CreateEncryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        ///  AES 解密
        /// </summary>
        /// <param name="str">明文（待解密）</param>
        /// <param name="key">密文</param>
        /// <returns></returns>
        public static AesDecryptVer AesDecrypt(string str)
        {
            AesDecryptVer model = new AesDecryptVer();
            model.code = ResultTypeI.error;
            model.reason = "为获取到加密字符";
            if (string.IsNullOrEmpty(str)) return model;

            var obj = JsonConvert.DeserializeObject<TViasMatButt>(str);
            var key = obj.key;
            if (key == "")
            {
                model.code = ResultTypeI.KeyError;
                model.reason = "Key为空";
                return model;
            }
            if (!AESHelper.Verification(key))
            {
                model.code = ResultTypeI.KeyError;
                model.reason = "Key错误";
                return model;
            }

            Byte[] toEncryptArray = Convert.FromBase64String(obj.body);
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(Secretkey),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            var body = Encoding.UTF8.GetString(resultArray);


            model.code = ResultTypeI.Success;
            model.body = body;
            return model;
        }
    }

    public class TViasMatButt
    {
        public string key { set; get; }
        public string body { get; set; }
    }

    public class AesDecryptVer
    {
        public ResultTypeI code { get; set; }
        public string body { get; set; }
        public string reason { get; set; }
    }

    public enum ResultTypeI
    {
        /// <summary>
        /// 成功。
        /// </summary>
        Success = 200,
        /// <summary>
        /// Key错误
        /// </summary>
        KeyError = 101,
        /// <summary>
        /// 解密失败。
        /// </summary>
        Info = 301,
        /// <summary>
        ///服务器错误。
        /// </summary>
        error = 500,
        /// <summary>
        ///服务器错误。
        /// </summary>
        Verror = 601
    }
}
