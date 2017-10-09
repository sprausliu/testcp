
namespace Constant {

    /// <summary>
    /// 画面ID
    /// </summary>
    public class PagesURL {

        /// <summary>
        /// 错误画面迁移的画面ID用KEY
        /// </summary>
        public const string PARAM_PAGEID = "pageid";
        /// <summary>
        /// 错误画面迁移的処理名用KEY
        /// </summary>
        public const string PARAM_PROCCESS_NAME = "proccessname";

        // -------------------------
        // 菜单以外的画面
        // -------------------------

        // SESSION超时
        public const string URL_SESSION_TIME_OUT = "timeout";
        // 错误显示
        public const string URL_ERROR_PAGE = "error";
        // 登录画面
        public const string URL_LOGIN = "login";

        // -------------------------
        // 菜单关联的画面
        // -------------------------
        public const string URL_MAKER = "maker";
        public const string URL_BATCH = "batch";
        public const string URL_PAYMENTS = "payments";

    }

}
