using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse
{
    public enum LogType
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
    public class LogHelper
    {
        static LogHelper()
        {
            log4net.Config.XmlConfigurator.Configure();

        }
        public static void WriteLog(LogType logType, Exception exp, Type type)
        {
            WriteLogObject(logType, exp, type);
        }
        public static void WriteLog(LogType logType, string msg, Type type)
        {
            WriteLogObject(logType, msg, type);
        }
        private static void WriteLogObject(LogType logType, object obj, Type type)
        {
            ILog log = LogManager.GetLogger(type.Assembly, type);
            switch (logType)
            {
                case LogType.Debug:
                    log.Debug(obj);
                    break;
                case LogType.Error:
                    log.Error(obj);
                    break;
                case LogType.Fatal:
                    log.Fatal(obj);
                    break;
                case LogType.Info:
                    log.Info(obj);
                    break;
                case LogType.Warn:
                    log.Warn(obj);
                    break;
            }
        }
    }
}
