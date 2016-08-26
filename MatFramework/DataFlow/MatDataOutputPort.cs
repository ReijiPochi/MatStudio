using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public class MatDataOutputPort : MatDataPort
    {
        public MatDataOutputPort(Type t, string name)
        {
            Name = name;
            MatDataType = t;
        }

        public ObservableCollection<MatDataInputPort> SendTo = new ObservableCollection<MatDataInputPort>();

        public void Output(MatData data)
        {
            foreach(MatDataInputPort port in SendTo)
            {
                port.Value = data;
            }
        }


        public override bool CanConnectTo(MatDataPort port)
        {
            MatDataInputPort trg = port as MatDataInputPort;
            if (trg == null) return false;

            return CanConnectToAnything || trg.MatDataType == MatDataType;
        }
    }
}
