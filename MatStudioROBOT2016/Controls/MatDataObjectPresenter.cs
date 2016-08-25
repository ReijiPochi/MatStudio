using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MatGUI;
using RobotCoreBase;

namespace MatStudioROBOT2016.Controls
{
    public class MatDataObjectPresenter : Control
    {
        static MatDataObjectPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatDataObjectPresenter), new FrameworkPropertyMetadata(typeof(MatDataObjectPresenter)));
        }

        private MatScrollViewer PART_Viewer;
        private Canvas PART_Canvas;
        private Line PART_Line;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Viewer = GetTemplateChild("PART_Viewer") as MatScrollViewer;
            PART_Canvas = GetTemplateChild("PART_Canvas") as Canvas;
            PART_Line = GetTemplateChild("PART_Line") as Line;

            Loaded += MatDataObjectPresenter_Loaded;

            PART_Canvas.Drop += PART_Canvas_Drop;
            PART_Canvas.DragOver += PART_Canvas_DragOver;
        }

        public ObservableCollection<MatDataObject> MatDataObjects
        {
            get { return (ObservableCollection<MatDataObject>)GetValue(MatDataObjectsProperty); }
            set { SetValue(MatDataObjectsProperty, value); }
        }
        public static readonly DependencyProperty MatDataObjectsProperty =
            DependencyProperty.Register("MatDataObjects", typeof(ObservableCollection<MatDataObject>), typeof(MatDataObjectPresenter), new PropertyMetadata(null,OnDataObjectsChanged));

        private static void OnDataObjectsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MatDataObjectPresenter trg = d as MatDataObjectPresenter;
            if (trg != null)
            {
                trg.RefreshCanvas();
            }
        }


        private void MatDataObjectPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            PART_Viewer.ScrollToHorizontalOffset(PART_Viewer.ScrollableWidth / 2.0);
            PART_Viewer.ScrollToVerticalOffset(PART_Viewer.ScrollableHeight / 2.0);
        }

        private void PART_Canvas_DragOver(object sender, DragEventArgs e)
        {
            string[] type = e.Data.GetFormats();
            MatDataOutputPortControl outp = e.Data.GetData(type[0]) as MatDataOutputPortControl;

            if (outp != null)
            {
                PART_Line.Visibility = Visibility.Visible;

                Point source = outp.PART_Bd.PointToScreen(new Point(outp.PART_Bd.ActualWidth / 2, outp.PART_Bd.ActualHeight / 2));
                Point zero = PART_Canvas.PointToScreen(new Point(0, 0));
                Point to = e.GetPosition(PART_Canvas);

                PART_Line.X1 = source.X - zero.X;
                PART_Line.Y1 = source.Y - zero.Y;
                PART_Line.X2 = to.X;
                PART_Line.Y2 = to.Y;
            }
        }

        private void PART_Canvas_Drop(object sender, DragEventArgs e)
        {
            PART_Line.Visibility = Visibility.Hidden;

            string[] type = e.Data.GetFormats();
            MatDataObject o = e.Data.GetData(type[0]) as MatDataObject;

            if (o == null) return;

            Module m = o as Module;

            if (m != null && MatDataObjects != null)
            {
                m.PositionX = e.GetPosition(PART_Canvas).X - 20;
                m.PositionY = e.GetPosition(PART_Canvas).Y - 10;
                MatDataObjects.Add(m);
                m.IsUsing = true;

                RefreshCanvas();
            }
            else if (o != null && MatDataObjects != null)
            {
                MatDataObject newObj = o.GetNewInstance();
                newObj.PositionX = e.GetPosition(PART_Canvas).X;
                newObj.PositionY = e.GetPosition(PART_Canvas).Y;
                MatDataObjects.Add(newObj);
                
                RefreshCanvas();
            }

        }

        public void RefreshCanvas()
        {
            if (PART_Canvas == null) return;

            PART_Canvas.Children.Clear();
            PART_Canvas.Children.Add(PART_Line);

            foreach(MatDataObject o in MatDataObjects)
            {
                MatDataObjectControl ctrl = new MatDataObjectControl()
                {
                    MyMatDataObject = o
                };

                Module m = o as Module;
                if (m != null)
                {
                    ctrl.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 200, 100, 100));
                }

                PART_Canvas.Children.Add(ctrl);
            }
        }
    }
}
