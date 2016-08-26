using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public class MatData : MatObject
    {
        public MatData(Type t, object value, DateTime time)
        {
            DataValue = value;
            Time = time;
            DataType = t;
        }

        public DateTime Time { get; protected set; }
        public object DataValue { get; protected set; }
        public Type DataType { get; protected set; }
    }
}
