using Livet;
using Livet.Commands;
using MatFramework.DataFlow;
using MatGUI;
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

        public MatDataObjectControl(MatDataObjectsPresenter owner)
        {
            Owner = owner;
        }

        private Thumb PART_Thumb;
        public StackPanel PART_Outputs;
        public StackPanel PART_Inputs;
        public MatDataObjectsPresenter Owner { get; private set; }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Thumb = GetTemplateChild("PART_Thumb") as Thumb;
            PART_Outputs = GetTemplateChild("PART_Outputs") as StackPanel;
            PART_Inputs = GetTemplateChild("PART_Inputs") as StackPanel;

            PART_Thumb.DragDelta += PART_Thumb_DragDelta;

            PART_Inputs.Drop += Ports_Drop;
            PART_Outputs.Drop += Ports_Drop;

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

                if (e.NewValue != null)
                {
                    trg.MyMatDataObject.PropertyChanged += trg.MyMatDataObject_PropertyChanged;
                }

                if (e.OldValue != null)
                {
                    ((MatDataObject)e.OldValue).PropertyChanged -= trg.MyMatDataObject_PropertyChanged;
                }
            }
        }

        private void MyMatDataObject_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PositionX" || e.PropertyName == "PositionY")
            {
                SetDataValues();
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

                PART_Outputs.Children.Add(outpc);
            }

            ObservableCollection<MatDataInputPort> inputs = MyMatDataObject.GetInputPorts();
            foreach (MatDataInputPort inp in inputs)
            {
                MatDataInputPortControl inpc = new MatDataInputPortControl();
                inpc.InputPort = inp;

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

            RaiseStateChanged(new StateChangedEventArgs());
        }
    }

    public class MatDataObjectControlVM : ViewModel
    {
        #region CloseCommand
        private ListenerCommand<object> _CloseCommand;

        public ListenerCommand<object> CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new ListenerCommand<object>(Close);
                }
                return _CloseCommand;
            }
        }

        public void Close(object parameter)
        {
            MatMenuItem from = parameter as MatMenuItem;
            if (from == null)
                throw new Exception("コマンドターゲットが不正、または取得できません");
            ContextMenu cm = from.Parent as ContextMenu;
            if (cm == null)
                throw new Exception("コマンドターゲットが不正、または取得できません");
            MatDataObjectControl trg = cm.PlacementTarget as MatDataObjectControl;
            if (trg == null)
                throw new Exception("コマンドターゲットが不正、または取得できません");

            trg.Owner.MatDataObjects.Remove(trg.MyMatDataObject);
        }
        #endregion
    }
}
