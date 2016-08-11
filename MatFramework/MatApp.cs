using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework
{
    public class MatApp : MatObject
    {
        private static Logger _ApplicationLog;
        public static Logger ApplicationLog
        {
            get
            {
                if(_ApplicationLog == null)
                {
                    _ApplicationLog = new Logger();
                }

                return _ApplicationLog;
            }
        }


    }
}
