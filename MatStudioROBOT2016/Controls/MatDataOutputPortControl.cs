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

using MatFramework.DataFlow;

namespace MatStudioROBOT2016.Controls
{
    public class MatDataOutputPortControl : Control
    {
        static MatDataOutputPortControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatDataOutputPortControl), new FrameworkPropertyMetadata(typeof(MatDataOutputPortControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Bd = GetTemplateChild("PART_Bd") as Border;

            PART_Bd.MouseLeave += PART_Bd_MouseLeave;
        }

        public Border PART_Bd;

        public MatDataPort OutputPort
        {
            get { return (MatDataPort)GetValue(OutputPortProperty); }
            set { SetValue(OutputPortProperty, value); }
        }
        public static readonly DependencyProperty OutputPortProperty =
            DependencyProperty.Register("OutputPort", typeof(MatDataPort), typeof(MatDataOutputPortControl), new PropertyMetadata(null));


        private void PART_Bd_MouseLeave(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
            }
        }

    }
}
