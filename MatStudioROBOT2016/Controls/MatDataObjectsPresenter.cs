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
using System.Windows.Threading;

namespace MatStudioROBOT2016.Controls
{
    public class MatDataObjectsPresenter : Control
    {
        static MatDataObjectsPresenter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatDataObjectsPresenter), new FrameworkPropertyMetadata(typeof(MatDataObjectsPresenter)));
        }

        private MatScrollViewer PART_Viewer;
        private Canvas PART_Canvas;
        private Line PART_Line;

        List<MatDataObjectControl> ctrls = new List<MatDataObjectControl>();
        private bool rockRefresh;

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
            DependencyProperty.Register("MatDataObjects", typeof(ObservableCollection<MatDataObject>), typeof(MatDataObjectsPresenter), new PropertyMetadata(null,OnDataObjectsChanged));

        private static void OnDataObjectsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MatDataObjectsPresenter trg = d as MatDataObjectsPresenter;
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

                Point zero = PART_Canvas.PointToScreen(new Point(0, 0));
                Point to = e.GetPosition(PART_Canvas);

                PART_Line.X1 = outp.PositionOfPART_Bd.X - zero.X;
                PART_Line.Y1 = outp.PositionOfPART_Bd.Y - zero.Y;
                PART_Line.X2 = to.X - 1;
                PART_Line.Y2 = to.Y - 1;
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
            if (PART_Canvas == null || rockRefresh) return;

            rockRefresh = true;

            for (int i = PART_Canvas.Children.Count - 1; i >= 0; i--)
            {
                MatDataObjectControl objc = PART_Canvas.Children[i] as MatDataObjectControl;
                if (objc != null)
                {
                    bool exist = false;

                    foreach (MatDataObject o in MatDataObjects)
                    {
                        if (objc.MyMatDataObject == o)
                            exist = true;
                    }

                    if (!exist)
                    {
                        PART_Canvas.Children.Remove(objc);
                        ctrls.Remove(objc);
                    }
                }
            }

            foreach (MatDataObject o in MatDataObjects)
            {
                bool exist = false;

                foreach (UIElement e in PART_Canvas.Children)
                {
                    MatDataObjectControl objc = e as MatDataObjectControl;
                    if (objc != null && objc.MyMatDataObject == o)
                        exist = true;
                }

                if (!exist)
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

                    ctrl.StateChanged += Ctrl_StateChanged;
                    ctrls.Add(ctrl);
                    PART_Canvas.Children.Add(ctrl);
                }

            }

            Utility.UpdateUI();

            rockRefresh = false;

            RefreshLines();
        }

        private void RefreshLines()
        {
            if (rockRefresh) return;

            for(int i = PART_Canvas.Children.Count - 1; i >= 0; i--)
            {
                Line l = PART_Canvas.Children[i] as Line;
                if (l != null)
                {
                    PART_Canvas.Children.Remove(l);
                }
            }

            PART_Canvas.Children.Add(PART_Line);

            Point zero = PART_Canvas.PointToScreen(new Point(0, 0));

            foreach (MatDataObjectControl ctrl in ctrls)
            {
                foreach (MatDataOutputPort outp in ctrl.MyMatDataObject.outputs)
                {
                    foreach (MatDataInputPort inp in outp.SendTo)
                    {
                        Line line = new Line();
                        line.Stroke = FindResource("MatForeGroundBrush") as SolidColorBrush;

                        if (!(outp.Owner is MatDataOutputPortControl) || !(inp.Owner is MatDataInputPortControl)) break;

                        line.X1 = ((MatDataOutputPortControl)outp.Owner).PositionOfPART_Bd.X - zero.X;
                        line.Y1 = ((MatDataOutputPortControl)outp.Owner).PositionOfPART_Bd.Y - zero.Y;
                        line.X2 = ((MatDataInputPortControl)inp.Owner).PositionOfPART_Bd.X - zero.X;
                        line.Y2 = ((MatDataInputPortControl)inp.Owner).PositionOfPART_Bd.Y - zero.Y;

                        PART_Canvas.Children.Add(line);
                    }
                }
            }
        }

        private void Ctrl_StateChanged(object sender, StateChangedEventArgs e)
        {
            RefreshLines();
        }
    }
}
