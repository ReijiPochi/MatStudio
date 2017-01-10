using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatFramework
{
    /// <summary>
    /// 様々な情報を格納します
    /// </summary>
    public class LogData
    {
        public LogData(LogCondition condition, string message, string description, object source)
        {
            Time = DateTime.Now;
            Condition = condition;
            Message = message;
            Description = description;
            Assembly = source.GetType().Assembly.GetName().Name;
            Source = source.GetType().Name;
        }

        public LogData(LogCondition condition, string message, string description, string source, string assembly)
        {
            Time = DateTime.Now;
            Condition = condition;
            Message = message;
            Description = description;
            Assembly = assembly;
            Source = source;
        }

        public DateTime Time { get; protected set; }
        public LogCondition Condition { get; protected set; }
        public string Message { get; protected set; }
        public string Description { get; protected set; }
        public string Assembly { get; protected set; }
        public string Source { get; protected set; }
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
    public class Logger
    {
        public Logger()
        {
            Log(new LogData(LogCondition.Action, "ロギングを開始しました", "", this));
        }

        public MatObservableSynchronizedCollection<LogData> LogList { get; private set; } = new MatObservableSynchronizedCollection<LogData>();

        public void Log(LogData log)
        {
            LogList.Insert(0, log);
        }

        public void LogException(string description, Exception ex, object source)
        {
            LogList.Insert(0, new LogData(LogCondition.Error,
                                 "例外がスローされました",
                                 description + "  ...\n例外：" + ex.GetType().Name + "\nスタックトレース：\n" + ex.StackTrace + "\nメッセージ：" + ex.Message,
                                 source));
        }
    }
}
