using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public class MatDataOutputPort<T> : MatDataPort
    {
        public ObservableCollection<MatDataInputPort<T>> SendTo = new ObservableCollection<MatDataInputPort<T>>();

        public void Output(MatData<T> data)
        {
            foreach(MatDataInputPort<T> port in SendTo)
            {
                port.Value = data;
            }
        }
    }
}
