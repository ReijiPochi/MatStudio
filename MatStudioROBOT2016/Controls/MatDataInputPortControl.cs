using MatFramework.DataFlow;
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

namespace MatStudioROBOT2016.Controls
{
    public class MatDataInputPortControl : Control
    {
        static MatDataInputPortControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatDataInputPortControl), new FrameworkPropertyMetadata(typeof(MatDataInputPortControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Bd = GetTemplateChild("PART_Bd") as Border;

            PART_Bd.DragOver += PART_Bd_DragOver;
        }

        public Border PART_Bd;

        public MatDataPort InputPort
        {
            get { return (MatDataPort)GetValue(InputPortProperty); }
            set { SetValue(InputPortProperty, value); }
        }
        public static readonly DependencyProperty InputPortProperty =
            DependencyProperty.Register("InputPort", typeof(MatDataPort), typeof(MatDataOutputPortControl), new PropertyMetadata(null));


        private void PART_Bd_DragOver(object sender, DragEventArgs e)
        {
            
        }

    }
}
