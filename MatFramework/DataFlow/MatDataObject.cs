using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public abstract class MatDataObject : MatObject, INotifyPropertyChanged
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

        public abstract ObservableCollection<MatDataPort> GetInputPorts();

        public abstract ObservableCollection<MatDataPort> GetOutputPorts();


        private double _PositionX;
        public double PositionX
        {
            get
            { return _PositionX; }
            set
            { 
                if (_PositionX == value)
                    return;
                _PositionX = value;
                RaisePropertyChanged("PositionX");
            }
        }

        private double _PositionY;
        public double PositionY
        {
            get
            { return _PositionY; }
            set
            { 
                if (_PositionY == value)
                    return;
                _PositionY = value;
                RaisePropertyChanged("PositionY");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}