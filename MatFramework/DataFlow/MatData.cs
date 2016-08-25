using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public class MatData<T> : MatData
    {
        public MatData(T value, DateTime time)
        {
            DataValue = value;
            Time = time;
        }

        public DateTime Time { get; protected set; }
        public T DataValue { get; protected set; }
    }

    public abstract class MatData : MatObject
    {
        public MatData()
        {
        }
    }
}
