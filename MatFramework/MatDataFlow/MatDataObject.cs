using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.MatDataFlow
{
    public abstract class MatDataObject : INotifyPropertyChanged
    {
        public MatDataObject(string name)
        {
            _Name = name;
        }

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


        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}