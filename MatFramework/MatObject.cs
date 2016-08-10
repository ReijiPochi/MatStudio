using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework
{
    public class MatObject
    {
        public MatObject()
        {
            MatApp.ApplicationLog.Log(new LogData(LogCondition.Action, "クラスを生成", "クラス名：" + this.GetType().Name));
        }

        public MatObject(bool logging)
        {
            if(logging)
            {
                MatApp.ApplicationLog.Log(new LogData(LogCondition.Action, "クラスを生成", "クラス名：" + this.GetType().Name));
            }
        }
    }
}
