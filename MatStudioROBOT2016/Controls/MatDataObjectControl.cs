using MatFramework.DataFlow;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatStudioROBOT2016.Controls
{
    public class MatDataObjectControl : Control
    {
        static MatDataObjectControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatDataObjectControl), new FrameworkPropertyMetadata(typeof(MatDataObjectControl)));
        }

        private Thumb PART_Thumb;
        public StackPanel PART_Outputs;
        public StackPanel PART_Inputs;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Thumb = GetTemplateChild("PART_Thumb") as Thumb;
            PART_Outputs = GetTemplateChild("PART_Outputs") as StackPanel;
            PART_Inputs = GetTemplateChild("PART_Inputs") as StackPanel;

            PART_Thumb.DragDelta += PART_Thumb_DragDelta;

            Initialize();
        }

        public MatDataObject MyMatDataObject
        {
            get { return (MatDataObject)GetValue(MyMatDataObjectProperty); }
            set { SetValue(MyMatDataObjectProperty, value); }
        }
        public static readonly DependencyProperty MyMatDataObjectProperty =
            DependencyProperty.Register("MyMatDataObject", typeof(MatDataObject), typeof(MatDataObjectControl), new PropertyMetadata(null, OnMyMatDataObjectChanged));

        private static void OnMyMatDataObjectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MatDataObjectControl trg = d as MatDataObjectControl;

            if (trg != null)
            {
                trg.Initialize();
            }
        }

        public event StateChangedEventHandler StateChanged;
        protected void RaiseStateChanged(StateChangedEventArgs e)
        {
            StateChanged?.Invoke(this, e);
        }


        private void PART_Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            MyMatDataObject.PositionX += e.HorizontalChange;
            MyMatDataObject.PositionY += e.VerticalChange;

            SetDataValues();
            RaiseStateChanged(new StateChangedEventArgs());
        }

        public void Initialize()
        {
            if (PART_Outputs == null || PART_Inputs == null) return;

            PART_Outputs.Children.Clear();
            PART_Inputs.Children.Clear();

            if (MyMatDataObject == null) return;

            SetDataValues();

            ObservableCollection<MatDataOutputPort> outputs = MyMatDataObject.GetOutputPorts();
            foreach (MatDataOutputPort outp in outputs)
            {
                MatDataOutputPortControl outpc = new MatDataOutputPortControl();
                outpc.OutputPort = outp;
                outpc.Drop += Ports_Drop;

                PART_Outputs.Children.Add(outpc);
            }

            ObservableCollection<MatDataInputPort> inputs = MyMatDataObject.GetInputPorts();
            foreach (MatDataInputPort inp in inputs)
            {
                MatDataInputPortControl inpc = new MatDataInputPortControl();
                inpc.InputPort = inp;
                inpc.Drop += Ports_Drop;

                PART_Inputs.Children.Add(inpc);
            }

            RaiseStateChanged(new StateChangedEventArgs());
        }

        private void Ports_Drop(object sender, DragEventArgs e)
        {
            RaiseStateChanged(new StateChangedEventArgs());
        }

        public void SetDataValues()
        {
            if (MyMatDataObject == null) return;

            SetValue(Canvas.LeftProperty, MyMatDataObject.PositionX);
            SetValue(Canvas.TopProperty, MyMatDataObject.PositionY);
        }
    }
}
