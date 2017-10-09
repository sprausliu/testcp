using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Common
{

    public class BaseDataAccess
    {

        #region "private变量"

        /// <summary>
        /// 超时
        /// </summary>
        public const int COMMAND_TIMEOUT = 110;

        /// <summary>
        /// DB的接続
        /// </summary>
        private SqlConnection _conn = null;

        /// <summary>
        /// DB的事务
        /// </summary>
        private SqlTransaction _trans = null;

        /// <summary>
        /// 当数据访问时事务是否使中 判断用 FLAG
        /// </summary>
        private bool _transactionUsed = false;

        #endregion

        #region "属性"

        /// <summary>
        /// DB连接用sqlConnection
        /// </summary>
        public SqlConnection Conn
        {
            get
            {
                return this._conn;
            }
            set
            {
                this._conn = value;
            }
        }

        /// <summary>
        /// 事务
        /// </summary>
        public SqlTransaction Trans
        {
            get
            {
                return this._trans;
            }
            set
            {
                this._trans = value;
            }
        }

        #endregion

        #region "构造方法"

        /// <summary>
        /// 数据库访问的构造方法
        /// </summary>
        /// <param name="conn"></param>
        public BaseDataAccess(SqlConnection conn)
        {
            this._conn = conn;
        }

        /// <summary>
        /// 数据库访问的构造方法
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="trans"></param>
        public BaseDataAccess(SqlConnection conn, SqlTransaction trans)
        {
            this._conn = conn;
            this._trans = trans;
            this._transactionUsed = true;
        }

        #endregion

        #region "方法"

        #region "private"
        /// <summary>
        /// 参数的设定
        /// </summary>
        /// <param name="cmd">SqlCommand</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>无</returns>
        private void FillParameters(SqlCommand cmd, SqlParameter[] parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter parm in parameters)
                {
                    cmd.Parameters.Add(parm);
                }
            }

        }
        #endregion

        #region "SQL执行用"
        /// <summary>
        /// 当前数据访问中是否使用事务
        /// </summary>
        /// <returns>True:使用、False：未使用</returns>
        protected bool IsUsedTransaction()
        {
            return _transactionUsed;
        }

        /// <summary>
        /// 共通方法：SQL文的执行ExecuteNonQuery
        /// </summary>
        /// <returns>影响行数</returns>
        protected int ExecuteNonQuery(string cmdText, SqlParameter[] parameters)
        {

            int result;
            using (SqlCommand sqlCom = new SqlCommand())
            {
                try
                {
                    sqlCom.Connection = this._conn;
                    sqlCom.CommandText = cmdText;
                    sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                    FillParameters(sqlCom, parameters);
                    if (IsUsedTransaction())
                    {
                        sqlCom.Transaction = this._trans;
                    }
                    result = sqlCom.ExecuteNonQuery();
                    sqlCom.Parameters.Clear();
                }
                catch (Exception sqlEx)
                {
                    // SQL文信息LOG中输出
                    LogUtil.WriteFatalMessage(this.GetLogSQL(cmdText, parameters));
                    throw sqlEx;
                }
            }
            return result;
        }

        /// <summary>
        /// 共通方法：SQL文的执行ExecuteReader
        /// </summary>
        /// <returns>取得数据</returns>
        protected SqlDataReader ExecuteReader(string cmdText, SqlParameter[] parameters)
        {

            SqlDataReader result;
            using (SqlCommand sqlCom = new SqlCommand())
            {
                try
                {
                    sqlCom.Connection = this._conn;
                    sqlCom.CommandText = cmdText;
                    sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                    FillParameters(sqlCom, parameters);
                    if (IsUsedTransaction())
                    {
                        sqlCom.Transaction = this._trans;
                    }
                    result = sqlCom.ExecuteReader();
                    sqlCom.Parameters.Clear();
                }
                catch (Exception sqlEx)
                {
                    // SQL文信息LOG中输出
                    LogUtil.WriteFatalMessage(this.GetLogSQL(cmdText, parameters));
                    throw sqlEx;
                }
            }
            return result;
        }

        /// <summary>
        /// 共通方法：SQL文的执行(存储过程)ExecuteScalar
        /// </summary>
        /// <returns>存储过程的执行結果</returns>
        protected object ExecuteScalar(string procName, SqlParameter[] parameters)
        {
            return ExecuteScalar(procName, parameters, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 共通方法：SQL文的执行(存储过程)ExecuteScalar
        /// </summary>
        /// <returns>存储过程的执行結果</returns>
        protected object ExecuteScalar(
            string cmdText, SqlParameter[] parameters, System.Data.CommandType cmdTpye)
        {

            object result;
            using (SqlCommand sqlCom = new SqlCommand())
            {
                try
                {
                    sqlCom.Connection = this._conn;
                    sqlCom.CommandType = cmdTpye;
                    sqlCom.CommandText = cmdText;
                    sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                    FillParameters(sqlCom, parameters);
                    if (IsUsedTransaction())
                    {
                        sqlCom.Transaction = this._trans;
                    }
                    result = sqlCom.ExecuteScalar();
                    sqlCom.Parameters.Clear();
                }
                catch (Exception sqlEx)
                {
                    // SQL文信息LOG中输出
                    if (cmdTpye == CommandType.StoredProcedure)
                    {
                        LogUtil.WriteFatalMessage(this.GetLogSQLProc(cmdText, parameters));
                    }
                    else
                    {
                        LogUtil.WriteFatalMessage(this.GetLogSQL(cmdText, parameters));
                    }
                    throw sqlEx;
                }
            }
            return result;
        }

        /// <summary>
        /// 共通方法：SQL文的执行(存储过程)ExecuteNonQuery
        /// </summary>
        /// <returns>影响行数</returns>
        protected int ExecuteProcNonQuery(string procName, SqlParameter[] parameters)
        {

            int result;
            using (SqlCommand sqlCom = new SqlCommand())
            {
                try
                {
                    sqlCom.Connection = this._conn;
                    sqlCom.CommandType = CommandType.StoredProcedure;
                    sqlCom.CommandText = procName;
                    sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                    FillParameters(sqlCom, parameters);
                    //返回值
                    SqlParameter retProc = new SqlParameter("@PROCRETURNVALUE", SqlDbType.Int);
                    retProc.Direction = ParameterDirection.ReturnValue;
                    sqlCom.Parameters.Add(retProc);
                    if (IsUsedTransaction())
                    {
                        sqlCom.Transaction = this._trans;
                    }
                    sqlCom.ExecuteNonQuery();
                    result = Convert.ToInt32(sqlCom.Parameters[sqlCom.Parameters.Count - 1].Value);
                    sqlCom.Parameters.Clear();
                }
                catch (Exception sqlEx)
                {
                    // SQL文信息LOG中输出
                    LogUtil.WriteFatalMessage(this.GetLogSQLProc(procName, parameters));
                    throw sqlEx;
                }
            }
            return result;
        }

        /// <summary>
        /// 共通方法：SQL文的执行(存储过程)Fill
        /// </summary>
        /// <returns>存储过程的执行結果</returns>
        protected DataTable ExecuteProcWithDataTable(string procName, SqlParameter[] parameters)
        {

            DataTable result = new DataTable();

            using (SqlDataAdapter sqlDA = new SqlDataAdapter())
            {
                using (SqlCommand sqlCom = new SqlCommand())
                {
                    try
                    {
                        sqlDA.SelectCommand = sqlCom;
                        sqlCom.Connection = this._conn;
                        sqlCom.CommandType = CommandType.StoredProcedure;
                        sqlCom.CommandText = procName;
                        sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                        FillParameters(sqlCom, parameters);
                        if (IsUsedTransaction())
                        {
                            sqlCom.Transaction = this._trans;
                        }
                        sqlDA.Fill(result);
                        sqlCom.Parameters.Clear();
                    }
                    catch (Exception sqlEx)
                    {
                        // SQL文信息LOG中输出
                        LogUtil.WriteFatalMessage(this.GetLogSQLProc(procName, parameters));
                        throw sqlEx;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 共通方法：SQL文的执行(存储过程)Fill
        /// </summary>
        /// <returns>DataSet</returns>
        protected DataSet ExecuteProcWithDataSet(string procName, SqlParameter[] parameters)
        {

            DataSet result = new DataSet();

            using (SqlDataAdapter sqlDA = new SqlDataAdapter())
            {
                using (SqlCommand sqlCom = new SqlCommand())
                {
                    try
                    {
                        sqlDA.SelectCommand = sqlCom;
                        sqlCom.Connection = this._conn;
                        sqlCom.CommandType = CommandType.StoredProcedure;
                        sqlCom.CommandText = procName;
                        sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                        FillParameters(sqlCom, parameters);
                        if (IsUsedTransaction())
                        {
                            sqlCom.Transaction = this._trans;
                        }
                        sqlDA.Fill(result);
                        sqlCom.Parameters.Clear();
                    }
                    catch (Exception sqlEx)
                    {
                        // SQL文信息LOG中输出
                        LogUtil.WriteFatalMessage(this.GetLogSQLProc(procName, parameters));
                        throw sqlEx;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 共通方法：SQL文的执行Fill
        /// </summary>
        /// <returns>DataTable</returns>
        protected DataTable ExecuteWithDataTable(string cmdText, SqlParameter[] parameters)
        {

            DataTable result = new DataTable();

            using (SqlDataAdapter sqlDA = new SqlDataAdapter())
            {
                using (SqlCommand sqlCom = new SqlCommand())
                {
                    try
                    {
                        sqlDA.SelectCommand = sqlCom;
                        sqlCom.Connection = this._conn;
                        sqlCom.CommandText = cmdText;
                        sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                        FillParameters(sqlCom, parameters);
                        if (IsUsedTransaction())
                        {
                            sqlCom.Transaction = this._trans;
                        }
                        sqlDA.Fill(result);
                        sqlCom.Parameters.Clear();
                    }
                    catch (Exception sqlEx)
                    {
                        // SQL文信息LOG中输出
                        LogUtil.WriteFatalMessage(this.GetLogSQL(cmdText, parameters));
                        throw sqlEx;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 共通方法：SQL文的执行Fill
        /// </summary>
        /// <returns>DataSet</returns>
        protected DataSet ExecuteWithDataSet(string cmdText, SqlParameter[] parameters)
        {

            DataSet result = new DataSet();

            using (SqlDataAdapter sqlDA = new SqlDataAdapter())
            {
                using (SqlCommand sqlCom = new SqlCommand())
                {
                    try
                    {
                        sqlDA.SelectCommand = sqlCom;
                        sqlCom.Connection = this._conn;
                        sqlCom.CommandText = cmdText;
                        sqlCom.CommandTimeout = COMMAND_TIMEOUT;
                        FillParameters(sqlCom, parameters);
                        if (IsUsedTransaction())
                        {
                            sqlCom.Transaction = this._trans;
                        }
                        sqlDA.Fill(result);
                        sqlCom.Parameters.Clear();
                    }
                    catch (Exception sqlEx)
                    {
                        // SQL文信息LOG中输出
                        LogUtil.WriteFatalMessage(this.GetLogSQL(cmdText, parameters));
                        throw sqlEx;
                    }
                }
            }
            return result;
        }


        #endregion

        #region "参数的设定"
        /// <summary>
        /// 参数空的場合、DBNull中变换
        /// </summary>
        /// <param name="inConvertStr">参数</param>
        /// <returns>变换結果</returns>
        public object GetNullEmpty(string inConvertStr)
        {
            if (string.IsNullOrEmpty(inConvertStr) || inConvertStr == "----/--/--")
            {
                return DBNull.Value;
            }
            else
            {
                return inConvertStr;
            }
        }
        /// <summary>
        /// 参数空的場合、0中变换
        /// </summary>
        /// <param name="inConvertStr">参数</param>
        /// <returns>变换結果</returns>
        public object GetZeroEmpty(string inConvertStr)
        {
            if (string.IsNullOrEmpty(inConvertStr))
            {
                return 0;
            }
            else
            {
                return inConvertStr;
            }
        }
        #endregion

        #region "LOG输出用"
        /// <summary>
        /// LOG输出用SQL文的取得
        /// </summary>
        /// <param name="inSQLP">参数名包含SQL文</param>
        /// <param name="inCmdParms">参数信息</param>
        ///  <returns></returns>
        protected string GetLogSQL(string inSQLP, SqlParameter[] parameters)
        {

            string result = inSQLP;
            StringBuilder paraInfo = new StringBuilder();

            if (parameters != null)
            {
                SqlParameter[] paraArr = new SqlParameter[parameters.Length];
                parameters.CopyTo(paraArr, 0);
                Array.Sort(paraArr, new SqlParameterNameComparer());
                for (int i = paraArr.Length - 1; i >= 0; i--)
                {
                    string paraValue = paraArr[i].Value == DBNull.Value ? "null" : "'" + paraArr[i].Value + "'";
                    result = result.Replace(paraArr[i].ParameterName, paraValue);
                    paraInfo.AppendLine(paraArr[i].ParameterName + ":" + paraValue);
                }
            }

            return result + "\r\n" + paraInfo.ToString();
        }

        /// <summary>
        /// LOG输出用SQL文的取得:存储过程的場合
        /// </summary>
        /// <param name="inSQLP">存储过程名</param>
        /// <param name="inCmdParms">参数信息</param>
        ///  <returns></returns>
        protected string GetLogSQLProc(string inSQLP, SqlParameter[] parameters)
        {

            StringBuilder result = new StringBuilder();
            StringBuilder paraInfo = new StringBuilder();

            result.Append(inSQLP);
            result.Append("(");

            if (parameters != null)
            {
                foreach (SqlParameter parm in parameters)
                {
                    string paraValue = parm.Value == DBNull.Value ? "null" : "'" + parm.Value + "'";
                    result.Append(parm.ParameterName).Append("=").Append(paraValue).Append(",");

                    paraInfo.AppendLine(parm.ParameterName + ":" + paraValue);
                }
            }
            result.Append(")");

            return result.ToString() + "\r\n" + paraInfo.ToString();
        }

        /// <summary>
        /// SQL文执行後的影响件数LOG
        /// </summary>
        /// <param name="count">影响件数</param>
        ///  <returns></returns>
        protected string GetExecuteLog(int count)
        {
            return string.Format("{0}行处理完成。", count);
        }


        /// <summary>
        /// 存储过程执行結果
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="returnCode">存储过程返回值</param>
        /// <returns></returns>
        protected string GetProcResultLog(string procName, int returnCode)
        {
            return string.Format("存储过程[{0}]被执行。返回值 : {1}", procName, returnCode);
        }

        #endregion

        #region "DataTable转换成LIST"
        /// <summary>
        /// DataTable转换成LIST
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected List<T> ConvertToList<T>(DataTable dt)
        {

            List<T> listORM = new List<T>();

            // 无数据場合、返回
            if (dt.Rows.Count == 0)
            {
                return listORM;
            }

            Type typeOfT = typeof(T);

            // 变换先类的各属性取得
            Dictionary<string, PropertyInfo> dicPI = new Dictionary<string, PropertyInfo>();
            PropertyInfo[] pis = typeOfT.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                dicPI.Add(pi.Name.ToUpper(), pi);
            }

            for (int rowIndex = 0; rowIndex < dt.Rows.Count; rowIndex++)
            {
                // 变换先的实例生成
                T objT = (T)Activator.CreateInstance(typeOfT);

                for (int colIndex = 0; colIndex < dt.Columns.Count; colIndex++)
                {
                    string strColName = dt.Columns[colIndex].ColumnName.ToUpper();

                    // 属性的取得
                    if (!dicPI.ContainsKey(strColName))
                    {
                        continue;
                    }
                    PropertyInfo pi = dicPI[strColName];

                    // 属性的值生成
                    object value = dt.Rows[rowIndex][colIndex];
                    if (Convert.IsDBNull(value))
                    {
                        continue;
                    }
                    if (dt.Columns[colIndex].DataType != pi.PropertyType)
                    {
                        if (pi.PropertyType == typeof(Nullable<DateTime>))
                        {
                            if (((Nullable<DateTime>)value).HasValue)
                            {
                                value = ((Nullable<DateTime>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else if (pi.PropertyType == typeof(Nullable<Int32>))
                        {
                            if (((Nullable<Int32>)value).HasValue)
                            {
                                value = ((Nullable<Int32>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else if (pi.PropertyType == typeof(Nullable<Double>))
                        {
                            if (((Nullable<Double>)value).HasValue)
                            {
                                value = ((Nullable<Double>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else if (pi.PropertyType == typeof(Nullable<Decimal>))
                        {
                            if (((Nullable<Decimal>)value).HasValue)
                            {
                                value = ((Nullable<Decimal>)value).Value;
                            }
                            else
                            {
                                value = null;
                            }
                        }
                        else
                        {
                            value = Convert.ChangeType(value, pi.PropertyType);
                        }
                    }
                    pi.SetValue(objT, value, null);
                }
                listORM.Add(objT);
            }

            return listORM;
        }
        #endregion

        #endregion

    }

    public class SqlParameterNameComparer : IComparer
    {

        public int Compare(object x, object y)
        {
            if (x == null || y == null)
            {
                return 0;
            }
            return ((SqlParameter)x).ParameterName.CompareTo(((SqlParameter)y).ParameterName);
        }
    }

}
