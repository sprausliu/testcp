using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common {

    /// <summary>
    /// 暗号化・復号化
    /// </summary>
    public partial class ComUtil {

        #region "暗号化"

        /// <summary>
        /// MD5暗号化
        /// </summary>
        /// <param name="insStrSecret">要暗号化文字列</param>
        ///  <returns></returns>
        public static string SecretWithMd5 (string insStrSecret) {
            string returnStr;
            byte[] byteSecret = Encoding.Unicode.GetBytes(insStrSecret);
            MD5 md5 = new MD5CryptoServiceProvider();
            returnStr = Convert.ToBase64String(md5.ComputeHash(byteSecret));
            return returnStr;
        }

        #region "復号化可的暗号化"

        /// <summary>
        /// 暗号化(復号化可)
        /// </summary>
        /// <param name="inEncryptStr">要暗号化文字列</param>
        /// <param name="inPublicKey">公開KEY</param>
        ///  <returns>暗号化后的文字列</returns>
        public static string Encrypt (string inEncryptStr, string inPublicKey) {
            CspParameters parameters = new CspParameters {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(0x400, parameters);
            provider.FromXmlString(inPublicKey);
            byte[] bytes = Encoding.UTF8.GetBytes(inEncryptStr);
            return Convert.ToBase64String(provider.Encrypt(bytes, false));
        }

        /// <summary>
        /// 復号化
        /// </summary>
        /// <param name="inEncryptStr">暗号化文字列</param>
        /// <param name="inPublicKey">公開KEY</param>
        ///  <returns>暗号化后的文字列</returns>
        public static string Decrypt (string inEncryptStr, string inPublicKey) {
            CspParameters parameters = new CspParameters {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(0x400, parameters);
            provider.FromXmlString(inPublicKey);
            byte[] rgb = Convert.FromBase64String(inEncryptStr);
            byte[] bytes = provider.Decrypt(rgb, false);
            return Encoding.UTF8.GetString(bytes);
        }

        private static string[] encryptDecryptKeys;

        /// <summary>
        /// 暗号化和復号化用KEY
        /// </summary>
        public static string[] Pub_Pri_Keys {
            get {
                if (encryptDecryptKeys == null) {
                    encryptDecryptKeys = CreateKeys();
                } else if (encryptDecryptKeys.Length == 0) {
                    encryptDecryptKeys = CreateKeys();
                }
                return encryptDecryptKeys;
            }
        }

        /// <summary>
        /// 暗号化和復号化用KEY的生成
        /// </summary>
        ///  <returns>KEY的配列</returns>
        public static string[] CreateKeys () {
            CspParameters parameters = new CspParameters {
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(0x400, parameters);
            string outEncryptStr = provider.ToXmlString(false);
            string outDecryptStr = provider.ToXmlString(true);
            string[] returnArr = new string[3];
            returnArr[0] = outEncryptStr;
            returnArr[1] = outDecryptStr;
            return returnArr;
        }

        #endregion

        #endregion


        #region "TOP画面用"
        /// <summary>
        /// Key
        /// </summary>
        public static string Key = "DKMAB5DE";
        /// <summary>
        /// 暗号化
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string TreeValueEncrypt (string pToEncrypt) {
            if (string.IsNullOrEmpty(pToEncrypt)) {
                return string.Empty;
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(Key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray()) {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        /// <summary>
        /// 復号化
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <returns></returns>
        public static string TreeValueDecrypt (string pToDecrypt) {
            if (string.IsNullOrEmpty(pToDecrypt)) {
                return string.Empty;
            }
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++) {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(Key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(Key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.ASCII.GetString(ms.ToArray());
        }

        /// <summary>
        /// 密码作成(8位～30位)
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomPassword () {
            string[] digit = new string[] 
            { 
              "A", "B", "C", "D", "E", "F", "G", 
              "H", "I", "J", "K", "L", "M", "N", 
              "O", "P", "Q", "R", "S", "T", "U", 
              "V", "W", "X", "Y", "Z", 
              "a", "b", "c", "d", "e", "f", "g",
              "h", "i", "j", "k", "l", "m", "n",
              "o", "p", "q", "r", "s", "t", "u",
              "v", "w", "x", "y", "z"
            };
            string[] number = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] mark = new string[] { "!", "#", "$", "%", "@" };
            Random random = new Random();
            int passwordLength = 8;

            int digitLength = random.Next(1, passwordLength - 2);
            int numberLength = passwordLength - digitLength - 1;
            int markLength = 1;

            //アルファベット
            string[] password = new string[passwordLength];
            for (int i = 0; i < digitLength; i++) {
                password[i] = digit[random.Next(digit.Length - 1)];
            }

            //数字
            for (int i = 0; i < numberLength; i++) {
                password[digitLength + i] = number[random.Next(number.Length - 1)];
            }

            //記号
            for (int i = 0; i < markLength; i++) {
                password[digitLength + numberLength + i] = mark[random.Next(mark.Length - 1)];
            }

            //組み合わせ
            List<int> list = new List<int>(passwordLength);
            while (list.Count < passwordLength) {
                int index = random.Next(1, passwordLength + 1) - 1;
                if (!list.Contains(index)) {
                    list.Add(index);
                }
            }
            string pwdResult = string.Empty;
            for (int i = 0; i < list.Count; i++) {
                pwdResult += "{" + list[i] + "}";
            }

            return string.Format(pwdResult, password);
        }

        #endregion

        /// <summary>
        /// sql注入的防止
        /// </summary>
        /// <param name="inConvertStr">项目</param>
        ///  <returns>变换后的sql</returns>
        public static object ConvertToSQLInjection (string inConvertStr) {
            string returnReslut = inConvertStr;
            if (string.IsNullOrEmpty(inConvertStr)) {
                return "";
            }
            returnReslut = returnReslut.Replace("'", "''");

            return returnReslut;
        }
    }

}
