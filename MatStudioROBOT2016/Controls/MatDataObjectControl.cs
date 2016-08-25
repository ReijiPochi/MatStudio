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
    public class MatDataObjectControl : Control
    {
        static MatDataObjectControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatDataObjectControl), new FrameworkPropertyMetadata(typeof(MatDataObjectControl)));
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
                trg.SetDataValues();
            }
        }

        public void SetDataValues()
        {
            SetValue(Canvas.LeftProperty, MyMatDataObject.PositionX);
            SetValue(Canvas.TopProperty, MyMatDataObject.PositionY);
        }
    }
}
