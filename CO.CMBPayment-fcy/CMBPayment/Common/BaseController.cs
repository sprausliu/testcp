using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Common
{

    public class BaseController
    {

        #region "private变量"

        /// <summary>
        /// 数据库连接
        /// </summary>
        private SqlConnection _conn;

        /// <summary>
        /// 事务
        /// </summary>
        private SqlTransaction _trans;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectString"].ConnectionString;

        #endregion

        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseController()
        {

        }

        #region "属性"
        /// <summary>
        /// 数据库连接
        /// </summary>
        public SqlConnection Conn
        {
            get { return this._conn; }
            set { this._conn = value; }
        }
        /// <summary>
        /// 事务
        /// </summary>
        public SqlTransaction Trans
        {
            get { return this._trans; }
            set { this._trans = value; }
        }

        #endregion

        #region "方法"
        /// <summary>
        /// GetConnection
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        protected SqlConnection GetConnection()
        {
            if (_conn == null)
            {
                this._conn = new SqlConnection(connectionString);
            }
            else if (this._conn.State == ConnectionState.Closed)
            {
                this._conn = new SqlConnection(connectionString);
            }
            return this._conn;
        }

        /// <summary>
        /// GetTransaction
        /// </summary>
        /// <returns>SqlTransaction</returns>
        protected SqlTransaction GetTransaction()
        {
            return this._conn.BeginTransaction();
        }

        /// <summary>
        /// OpenConnection
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        protected void OpenConnection()
        {
            if (this._conn == null)
            {
                GetConnection();
            }
            if (this._conn.State != ConnectionState.Open)
                this._conn.Open();
        }

        /// <summary>
        /// CloseConnection
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void CloseConnection()
        {
            if (this._conn != null)
            {
                if (this._conn.State == ConnectionState.Open)
                {
                    this._conn.Close();
                }
                this._conn.Dispose(); this._conn = null;
            }
        }
        /// <summary>
        /// BeginTrans
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void BeginTrans()
        {
            if (this._conn != null) this._trans = this._conn.BeginTransaction();
        }

        /// <summary>
        /// BeginTrans
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void BeginTrans(IsolationLevel inIsolationLevel)
        {
            if (this._conn != null)
                this._trans = this._conn.BeginTransaction(inIsolationLevel);
        }

        /// <summary>
        /// CommitTrans
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void CommitTrans()
        {
            if (this._trans != null) this._trans.Commit();
        }
        /// <summary>
        /// RollbackTrans
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public void RollbackTrans()
        {
            if (this._trans != null) this._trans.Rollback();
        }
        #endregion

    }
}
