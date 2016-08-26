using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatFramework.DataFlow;

namespace RobotCore1.Modules
{
    public class Motor1 : Motor
    {
        public Motor1(string name) : base(name)
        {
        }

        public override void SetRecievedData(string data)
        {
            string[] s = data.Split(':');

            switch (s[0])
            {
                case "Aa":

                    break;

                default:
                    break;
            }
        }
    }
}
