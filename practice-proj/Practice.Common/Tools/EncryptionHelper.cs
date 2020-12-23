using System;
using System.Security.Cryptography;
using System.Text;

namespace Practice.Common.Tools
{
    /// <summary>
    /// 加密帮助类
    /// </summary>
    public static class EncryptionHelper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5Hash(string str)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.ASCII.GetBytes(str));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
