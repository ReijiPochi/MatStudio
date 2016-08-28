using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public abstract class MatDataObject : MatNotificationObject
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

        public ObservableCollection<MatDataInputPort> inputs = new ObservableCollection<MatDataInputPort>();

        public virtual ObservableCollection<MatDataInputPort> GetInputPorts()
        {
            return inputs;
        }

        public ObservableCollection<MatDataOutputPort> outputs = new ObservableCollection<MatDataOutputPort>();

        public virtual ObservableCollection<MatDataOutputPort> GetOutputPorts()
        {
            return outputs;
        }

        public abstract MatDataObject GetNewInstance();


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

        public void DisconnectAll()
        {
            foreach(MatDataInputPort inp in inputs)
            {
                if (inp.SendFrom != null)
                {
                    inp.SendFrom.SendTo.Remove(inp);
                    inp.SendFrom = null;
                }
            }

            foreach(MatDataOutputPort outp in outputs)
            {
                foreach(MatDataInputPort to in outp.SendTo)
                {
                    to.SendFrom = null;
                }

                outp.SendTo.Clear();
            }
        }
    }
}