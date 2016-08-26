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


        private bool _IsUsing;
        public bool IsUsing
        {
            get
            { return _IsUsing; }
            set
            { 
                if (_IsUsing == value)
                    return;
                _IsUsing = value;
                RaisePropertyChanged("IsUsing");
            }
        }

        public abstract void SetRecievedData(string data);
    }
}
