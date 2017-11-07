
namespace Common
{

    /// <summary>
    /// 基本信息用类
    /// </summary>
    public class BaseInfoBean
    {

        #region 【属性】
        /// <summary>
        /// 编辑或者追加
        /// </summary>
        public bool IsEditMode
        {
            get;
            set;
        }

        /// <summary>
        /// 检索FLAG
        /// </summary>
        public bool IsSearched
        {
            get;
            set;
        }

        /// <summary>
        /// 编辑或者追加完成FLAG
        /// </summary>
        public bool IsSaved
        {
            get;
            set;
        }

        /// <summary>
        /// 更新日时:更新的时、作为更新KEY执行
        /// </summary>
        public string UpdateDateKey
        {
            get;
            set;
        }

        /// <summary>
        /// 作成者
        /// </summary>
        public string CreateUserId
        {
            get;
            set;
        }
        /// <summary>
        /// 作成日时
        /// </summary>
        public string CreateDate
        {
            get;
            set;
        }
        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateUserId
        {
            get;
            set;
        }

        /// <summary>
        /// 更新日时
        /// </summary>
        public string UpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 更新者类型
        /// </summary>
        public string UpdateUserType
        {
            get;
            set;
        }

        /// <summary>
        /// 内容有修改FLAG
        /// </summary>
        public bool IsChanged
        {
            get;
            set;
        }

        #endregion

    }
}
