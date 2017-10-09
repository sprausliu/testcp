using log4net;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Common
{

    /// <summary>
    /// LOG输出类
    /// </summary>
    public class LogUtil
    {

        #region "变量和定数"
        /// <summary>
        /// LOG
        /// </summary>
        private static ILog _logger = null;
        #endregion

        #region "属性"
        /// <summary>
        /// LOG
        /// </summary>
        public static ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                }
                return _logger;
            }
        }
        #endregion

        #region "方法"

        #region "异常 LOG"
        /// <summary>
        /// 异常 LOG 输出
        /// </summary>
        /// <param name="exception"></param>
        ///  <returns></returns>
        public static void WriteErrorMessage(Exception exception)
        {
            if (exception.GetType() == typeof(LogicException))
            {
                Logger.Error(((LogicException)exception).MethodName, exception);
            }
            else
            {
                Logger.Error(exception.Message, exception);
            }
        }
        #endregion

        #region "DEBUG LOG"
        /// <summary>
        /// DEBUG LOG 输出(方法执行成功)
        /// </summary>
        ///  <returns></returns>
        public static void WriteDebugMessage()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase mb = frame.GetMethod();
            Logger.Debug(string.Format("{0}正常结束。", mb.Name));
        }

        /// <summary>
        /// DEBUG LOG 输出(指定消息)
        /// </summary>
        /// <param name="msg"></param>
        ///  <returns></returns>
        public static void WriteDebugMessage(String msg)
        {
            Logger.Debug(msg);
        }

        /// <summary>
        /// DEBUG 開始 LOG 输出
        /// </summary>
        /// <returns>无</returns>
        public static void WriteDebugStartMessage()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase mb = frame.GetMethod();
            string methodName = mb.Name + "(" + mb.ReflectedType + ")";

            Logger.Debug(string.Format("{0} Start", methodName));
        }

        /// <summary>
        /// DEBUG 结束 LOG 输出
        /// </summary>
        /// <returns>无</returns>
        public static void WriteDebugEndMessage()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase mb = frame.GetMethod();
            string methodName = mb.Name + "(" + mb.ReflectedType + ")";

            Logger.Debug(string.Format("{0} End", methodName));
        }
        #endregion

        #region "INFO LOG"
        /// <summary>
        /// INFO LOG 输出
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static void WriteInfoMessage(String msg)
        {
            Logger.Info(msg);
        }
        #endregion

        #region "WARNING LOG"
        /// <summary>
        /// WARNING LOG 输出
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static void WriteWarnMessage(String msg)
        {
            Logger.Warn(msg);
        }

        /// <summary>
        /// WARNING LOG 输出
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static void WriteWarnMessage(Exception exception)
        {
            if (exception.GetType() == typeof(LogicException))
            {
                Logger.Warn(((LogicException)exception).MethodName, exception);
            }
            else
            {
                Logger.Warn(exception.Message, exception);
            }
        }
        #endregion

        #region "FATAL 异常"
        /// <summary>
        /// FATAL 异常 LOG 输出
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static void WriteFatalMessage(String msg)
        {
            Logger.Fatal(msg);
        }
        #endregion
        #endregion

    }
}
