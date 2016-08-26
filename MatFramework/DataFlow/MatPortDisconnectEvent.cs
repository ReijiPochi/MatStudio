using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public delegate void MatPortDisconnectEvent(object sender, MatPortDisconnectEventArgs e);

    public class MatPortDisconnectEventArgs
    {
        public MatDataPort DisconnectTo { get; set; }
    }
}
