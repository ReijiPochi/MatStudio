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
    public class LogData : MatObject
    {
        public LogData(string message, string description)
        {
            Time = DateTime.Now;
            Condition = LogCondition.None;
            Message = message;
            Description = description;
        }

        public LogData(LogCondition condition, string message, string description)
        {
            Time = DateTime.Now;
            Condition = condition;
            Message = message;
            Description = description;
        }

        public DateTime Time { get; protected set; }
        public LogCondition Condition { get; protected set; }
        public string Message { get; protected set; }
        public string Description { get; protected set; }
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
        public ObservableCollection<LogData> LogList { get; private set; } = new ObservableCollection<LogData>();

        public void Log(LogData log)
        {
            LogList.Add(log);
        }

        public void LogException(string description, Exception ex)
        {
            LogList.Add(new LogData(LogCondition.Error,
                                 "例外がスローされました",
                                 description + "  ...\n例外：" + ex.GetType().Name + "\nスタックトレース：\n" + ex.StackTrace + "\nメッセージ：" + ex.Message));
        }
    }
}
