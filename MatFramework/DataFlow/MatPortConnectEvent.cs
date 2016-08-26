using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.DataFlow
{
    public delegate void MatPortConnectEvent(object sender, MatPortConnectEventArgs e);

    public class MatPortConnectEventArgs
    {
        public MatDataPort ConnectTo { get; set; }
    }
}
