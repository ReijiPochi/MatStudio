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

            PART_Bd.DragEnter += PART_Bd_DragEnter;
            PART_Bd.DragLeave += PART_Bd_DragLeave;
            PART_Bd.Drop += PART_Bd_Drop;
            PART_Bd.MouseLeave += PART_Bd_MouseLeave;

            bgBrush = PART_Bd.Background;
            borderBrush = PART_Bd.BorderBrush;
        }

        public Border PART_Bd;
        public Brush bgBrush;
        public Brush borderBrush;

        public Point PositionOfPART_Bd
        {
            get { return PART_Bd.PointToScreen(new Point(PART_Bd.ActualWidth / 2, PART_Bd.ActualHeight / 2)); }
        }

        public MatDataInputPort InputPort
        {
            get { return (MatDataInputPort)GetValue(InputPortProperty); }
            set { SetValue(InputPortProperty, value); }
        }
        public static readonly DependencyProperty InputPortProperty =
            DependencyProperty.Register("InputPort", typeof(MatDataInputPort), typeof(MatDataInputPortControl), new PropertyMetadata(null, OnInputPortChanged));

        private static void OnInputPortChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MatDataInputPortControl trg = d as MatDataInputPortControl;

            if (trg != null)
            {
                if (e.NewValue != null)
                {
                    trg.InputPort.Owner = trg;
                }
                else
                {
                    ((MatDataInputPort)(e.OldValue)).Owner = null;
                }
            }
        }

        private void PART_Bd_DragEnter(object sender, DragEventArgs e)
        {
            string[] type = e.Data.GetFormats();
            MatDataOutputPortControl outp = e.Data.GetData(type[0]) as MatDataOutputPortControl;

            if (outp != null && InputPort.CanConnectTo(outp.OutputPort))
            {
                PART_Bd.Background = new SolidColorBrush(Color.FromArgb(255, 50, 150, 255));
            }
        }

        private void PART_Bd_Drop(object sender, DragEventArgs e)
        {
            PART_Bd.Background = bgBrush;

            string[] type = e.Data.GetFormats();
            MatDataOutputPortControl outp = e.Data.GetData(type[0]) as MatDataOutputPortControl;

            if (outp != null && InputPort.CanConnectTo(outp.OutputPort))
            {
                outp.OutputPort.SendTo.Add(InputPort);
                InputPort.SendFrom = outp.OutputPort;
            }
        }

        private void PART_Bd_DragLeave(object sender, DragEventArgs e)
        {
            PART_Bd.Background = bgBrush;
        }

        private void PART_Bd_MouseLeave(object sender, MouseEventArgs e)
        {
            if(e.LeftButton== MouseButtonState.Pressed && InputPort.SendFrom != null)
            {
                InputPort.SendFrom.SendTo.Remove(InputPort);
                MatDataOutputPortControl outpc = InputPort.SendFrom.Owner as MatDataOutputPortControl;
                InputPort.SendFrom = null;

                DragDrop.DoDragDrop(this, outpc, DragDropEffects.Move);
            }
        }

    }
}
