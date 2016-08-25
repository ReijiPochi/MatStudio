using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.DataFlow;

namespace RobotCoreBase
{
    public abstract class Module : MatDataObject
    {
        public Module(string name) : base(name)
        {
            IsUsing = false;
        }

        public bool IsUsing { get; set; }
    }
}
