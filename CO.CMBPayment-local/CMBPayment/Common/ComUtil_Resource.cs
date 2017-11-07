using System.Web;

namespace Common {

    /// <summary>
    /// 源文件关联
    /// </summary>
    public partial class ComUtil {

        /// <summary>
        /// 源文件取得(全局)
        /// </summary>
        /// <param name="inKey">源文件KEY</param>
        ///  <returns></returns>
        public static string GetGlobalResource (string inKey) {
            object returnValue = HttpContext.GetGlobalResourceObject(Constant.ResourcesKey.RESOURCES_FILE_BASE_NAME, inKey);
            return returnValue == null ? "" : returnValue.ToString();
        }

        /// <summary>
        /// 源文件取得(本地)
        /// </summary>
        /// <param name="inBaseName">源文件文件名(画面ID)</param>
        /// <param name="inKey">源文件KEY</param>
        ///  <returns></returns>
        public static string GetLocalsResource (string inBaseName, string inKey) {
            object returnValue = HttpContext.GetLocalResourceObject("~/webforms/" + inBaseName, inKey);
            return returnValue == null ? "" : returnValue.ToString();
        }

    }
}
