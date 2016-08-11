using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework
{
    /// <summary>
    /// 様々な情報を格納します
    /// </summary>
    public class LogData : MatObject
    {
        public LogData(string message, string discription)
        {
            Time = DateTime.Now;
            Condition = LogCondition.None;
            Message = message;
            Discription = discription;
        }

        public LogData(LogCondition condition, string message, string discription)
        {
            Time = DateTime.Now;
            Condition = condition;
            Message = message;
            Discription = discription;
        }

        public DateTime Time { get; protected set; }
        public LogCondition Condition { get; protected set; }
        public string Message { get; protected set; }
        public string Discription { get; protected set; }
    }

    /// <summary>
    /// ログの状態（注意、警告など）を表します。
    /// </summary>
    public enum LogCondition
    {
        None,
        Action,
        Attention,
        Warning,
        Error
    }

    /// <summary>
    /// ログを記録します。
    /// </summary>
    public class Logger : MatObject
    {
        protected List<LogData> _Log = new List<LogData>();

        public void Log(LogData log)
        {
            _Log.Add(log);
        }

        public void LogException(string discription, Exception ex)
        {
            _Log.Add(new LogData(LogCondition.Error,
                                 "例外がスローされました",
                                 discription + " 例外：" + ex.GetType().Name + " 場所：" + ex.Source + " " + ex.Message));
        }
    }
}
