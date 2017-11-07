namespace Common
{

    /// <summary>
    ///     分页用类
    /// </summary>
    /// <remarks>
    ///     分页用类です
    /// </remarks>
    public class BasePagingBean : BaseInfoBean
    {

        #region 【private变量】

        //输出数据：総画面数
        private int _pageCount = 1;

        //输出数据：当画面№
        private int _pageIndex = 1;

        //输出数据：画面显示件数
        private int _pageSize = 0;

        //输出数据：結果総件数
        private int _itemsCount = 0;

        #endregion

        #region 【属性】
        /// <summary>
        /// 総画面数
        /// </summary>
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                _pageCount = value;
            }
        }

        /// <summary>
        /// 当画面№
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        /// <summary>
        /// １画面显示件数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        /// <summary>
        /// 結果総件数
        /// </summary>
        public int ItemsCount
        {
            get
            {
                return _itemsCount;
            }
            set
            {
                _itemsCount = value;
            }
        }
        #endregion

    }
}
