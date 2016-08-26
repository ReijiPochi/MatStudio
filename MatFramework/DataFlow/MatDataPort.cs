using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MatFramework.DataFlow
{
    public abstract class MatDataPort : MatNotificationObject
    {

        private string _Name;
        public string Name
        {
            get
            { return _Name; }
            set
            { 
                if (_Name == value)
                    return;
                _Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public bool CanConnectToAnything { get; set; }

        public Type MatDataType { get; protected set; }

        public Control Owner { get; set; }

        public abstract bool CanConnectTo(MatDataPort port);
    }
}
