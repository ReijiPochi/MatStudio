using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatStudioROBOT2016.Views.ControlPanels
{
    public class TerminalCP : MatGUI.MatControlPanelBase
    {
        static TerminalCP()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TerminalCP), new FrameworkPropertyMetadata(typeof(TerminalCP)));
        }
    }
}
