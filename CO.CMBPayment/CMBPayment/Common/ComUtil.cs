using System;
using System.Diagnostics;
using System.Reflection;
using System.Configuration;
using System.IO;
using System.Globalization;


namespace Common
{

    /// <summary>
    ///     工具类
    /// </summary>
    /// <remarks>
    /// </remarks>
    public partial class ComUtil
    {

        /// <summary>
        /// 从数据库读取的数据，变换成文字列。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defText">null的場合显示文字列</param>
        public static string DbDataToCtlText(object obj, string defText)
        {
            if (obj == DBNull.Value)
                return defText;
            string objType = obj.GetType().Name;
            switch (objType)
            {
                case "DateTime":
                    {
                        return FormatDateToStringWithMark((DateTime)obj);
                    }
                case "String":
                    {
                        return obj.ToString();
                    }
                case "Int32":
                    {
                        return obj.ToString();
                    }
                default:
                    {
                        return obj.ToString();
                    }
            }
        }

        /// <summary>
        /// 乱数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Random(int min, int max)
        {

            int seed = BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0);
            Random rand = new Random(seed);
            int v = rand.Next(min, max);

            return v;
        }

        /// <summary>
        /// 自方法名取得
        /// </summary>
        /// <param ></param>
        /// <returns></returns>
        public static String GetCurrentMethodName()
        {
            MethodBase methodBase = new StackTrace().GetFrame(1).GetMethod();

            return methodBase.Name;
        }

        /// <summary>
        /// 数字的变换
        /// </summary>
        /// <param name="inNum">变换数字</param>
        /// <param name="defaultValue">默认值</param>
        ///  <returns></returns>
        public static int ConvertToNum(String inNum, int defaultValue)
        {

            if (inNum == null) return defaultValue;
            string s = inNum.Trim().Replace(Constant.FormatInfo.COMMA, "");
            if (string.IsNullOrEmpty(s))
            { // 空的場合
                return defaultValue;
            }

            try
            {
                int value = defaultValue;
                if (int.TryParse(s, out value)) return value;
            }
            catch { }

            return defaultValue;
        }

        /// <summary>
        /// 数字的变换
        /// </summary>
        /// <param name="inNum">变换数字</param>
        /// <param name="defaultValue">默认值</param>
        ///  <returns></returns>
        public static double ConvertToDouble(String inNum, double defaultValue)
        {

            if (string.IsNullOrEmpty(inNum))
            {
                return defaultValue;
            }
            string s = inNum.Trim().Replace(Constant.FormatInfo.COMMA, "");
            if (string.IsNullOrEmpty(s))
            { // 空的場合
                return defaultValue;
            }

            try
            {
                double value = defaultValue;
                if (double.TryParse(s, out value))
                    return value;
            }
            catch { }

            return defaultValue;
        }

        /// <summary>
        /// 指定文件的删除
        /// </summary>
        /// <param name="inFilePath">文件路径</param>
        public static void DeleteServerFile(string inFilePath)
        {
            // 文件删除
            if (File.Exists(inFilePath))
            {
                File.Delete(inFilePath);
            }
            // 当文件夹下中文件不存在、文件夹删除
            string folderPath = Path.GetDirectoryName(inFilePath);
            if (Directory.Exists(folderPath))
            {
                string[] allFiles = Directory.GetFiles(folderPath);
                if (allFiles.Length == 0)
                {
                    Directory.Delete(folderPath);
                }
            }
        }

        /// <summary>
        /// 指定文件的移動
        /// ※引数：文件夹路径(不是文件路径)
        /// </summary>
        /// <param name="inFolderPath">移動元</param>
        /// <param name="inDestFolderPath">移動先</param>
        public static void MoveServerFile(string inFilePath, string inDestFilePath, bool inDelEmptyFolder)
        {
            string destFolder = Path.GetDirectoryName(inDestFilePath);
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            if (File.Exists(inDestFilePath))
            {
                File.Delete(inDestFilePath);
            }

            File.Move(inFilePath, inDestFilePath);
            if (inDelEmptyFolder)
            {
                string folderPath = Path.GetDirectoryName(inFilePath);
                if (Directory.Exists(folderPath))
                {
                    string[] allFiles = Directory.GetFiles(folderPath);
                    if (allFiles.Length == 0)
                    {
                        Directory.Delete(folderPath);
                    }
                }
            }
        }

    }

}
