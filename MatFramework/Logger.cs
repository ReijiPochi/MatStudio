using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework
{
    public class LogData
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

    public enum LogCondition
    {
        None,
        Action,
        Attention,
        Warning
    }

    public class Logger
    {
        protected List<LogData> _Log = new List<LogData>();

        public void Log(LogData log)
        {
            _Log.Add(log);
        }
    }
}
