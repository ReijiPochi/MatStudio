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
        private StackPanel PART_Outputs;
        private StackPanel PART_Inputs;

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


        private void PART_Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            MyMatDataObject.PositionX += e.HorizontalChange;
            MyMatDataObject.PositionY += e.VerticalChange;

            SetDataValues();
        }

        public void Initialize()
        {
            if (PART_Outputs == null || PART_Inputs == null) return;

            PART_Outputs.Children.Clear();
            PART_Inputs.Children.Clear();

            if (MyMatDataObject == null) return;

            SetDataValues();

            ObservableCollection<MatDataPort> outputs = MyMatDataObject.GetOutputPorts();
            foreach (MatDataPort outp in outputs)
            {
                PART_Outputs.Children.Add(new MatDataOutputPortControl() { OutputPort = outp });
            }

            ObservableCollection<MatDataPort> inputs = MyMatDataObject.GetInputPorts();
            foreach (MatDataPort inp in inputs)
            {
                PART_Inputs.Children.Add(new MatDataInputPortControl() { InputPort = inp });
            }
        }

        public void SetDataValues()
        {
            if (MyMatDataObject == null) return;

            SetValue(Canvas.LeftProperty, MyMatDataObject.PositionX);
            SetValue(Canvas.TopProperty, MyMatDataObject.PositionY);
        }
    }
}
