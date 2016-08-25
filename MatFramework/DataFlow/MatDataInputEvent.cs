using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public delegate void MatDataInputEventHandler(object sender, MatDataInputEventArgs e);

    public class MatDataInputEventArgs
    {
        public MatDataInputEventArgs(MatData newValue, MatData oldValue)
        {
            NewValue = newValue;
            OldValue = oldValue;
        }

        public MatData NewValue { get; set; }
        public MatData OldValue { get; private set; }
    }
}
