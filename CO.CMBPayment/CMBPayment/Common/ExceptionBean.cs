using System;
using System.Diagnostics;

namespace Common
{

    public class BaseException : Exception
    {

        #region "private变量"
        // 方法名
        private string methodName;
        #endregion

        #region "属性"
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName
        {
            get { return methodName; }
            set { methodName = value; }
        }
        #endregion

        /// <summary>
        /// 父类例外処理
        /// </summary>
        /// <param name="inMessage">消息内容</param>
        /// <param name="exception">异常信息</param>
        ///  <returns></returns>
        public BaseException(string inMessage, Exception inEx)
            : base(inMessage, inEx)
        {

        }

        /// <summary>
        /// 父类例外処理
        /// </summary>
        /// <param name="methodName">异常発生的方法名</param>
        /// <param name="inMessage">消息内容</param>
        /// <param name="exception">异常信息</param>
        ///  <returns></returns>
        public BaseException(string inMethodName, string inMessage, Exception inEx)
            : base(inMessage, inEx)
        {
            this.MethodName = inMethodName;
        }

    }

    /// <summary>
    ///     数据访问例外処理用类
    /// </summary>
    /// <remarks>
    ///     数据访问例外処理用类
    /// </remarks>
    public class DataAccessException : BaseException
    {

        /// <summary>
        /// 数据访问例外処理
        /// </summary>
        /// <param name="methodName">异常発生的方法名</param>
        /// <param name="exception">异常</param>
        ///  <returns></returns>
        public DataAccessException(string methodName, Exception exception)
            : base(methodName, "发生数据访问错误。", exception)
        {
        }

        /// <summary>
        /// 数据访问例外処理
        /// </summary>
        /// <param name="exception">异常信息</param>
        ///  <returns></returns>
        public DataAccessException(Exception exception)
            : base("发生数据访问错误。", exception)
        {
            StackFrame frame = new StackFrame(1);
            System.Reflection.MethodBase mb = frame.GetMethod();
            base.MethodName = mb.Name + "(" + mb.ReflectedType + ")";
        }
    }

    /// <summary>
    /// 逻辑例外処理用类
    /// </summary>
    /// <remarks>
    /// 逻辑例外処理用类です
    /// </remarks>
    public class LogicException : BaseException
    {
        /// <summary>
        /// 逻辑例外処理
        /// </summary>
        /// <param name="methodName">异常発生的方法名</param>
        /// <param name="exception">异常信息</param>
        ///  <returns></returns>
        public LogicException(string methodName, Exception exception)
            : base(methodName, "发生逻辑错误。", exception)
        {
        }

        /// <summary>
        /// 逻辑例外処理
        /// </summary>
        /// <param name="exception">异常信息</param>
        ///  <returns></returns>
        public LogicException(Exception exception)
            : base("发生逻辑错误。", exception)
        {
            StackFrame frame = new StackFrame(1);
            System.Reflection.MethodBase mb = frame.GetMethod();
            base.MethodName = mb.Name + "(" + mb.ReflectedType + ")";
        }

    }
    /// <summary>
    /// 文件格式処理用类
    /// </summary>
    public class FileFormatException : BaseException
    {
        /// <summary>
        /// 文件格式例外処理
        /// </summary>
        /// <param name="methodName">异常発生的方法名</param>
        /// <param name="exception">异常信息</param>
        ///  <returns></returns>
        public FileFormatException(string methodName, Exception exception)
            : base(methodName, "发生逻辑错误。", exception)
        {
        }

        /// <summary>
        /// 文件格式例外処理
        /// </summary>
        /// <param name="exception">异常信息</param>
        ///  <returns></returns>
        public FileFormatException(Exception exception)
            : base("发生逻辑错误。", exception)
        {
            StackFrame frame = new StackFrame(1);
            System.Reflection.MethodBase mb = frame.GetMethod();
            base.MethodName = mb.Name + "(" + mb.ReflectedType + ")";
        }

    }

}
