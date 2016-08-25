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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Viewer = GetTemplateChild("PART_Viewer") as MatScrollViewer;
            PART_Canvas = GetTemplateChild("PART_Canvas") as Canvas;

            Loaded += MatDataObjectPresenter_Loaded;

            PART_Canvas.Drop += PART_Canvas_Drop;
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
            
        }


        private void MatDataObjectPresenter_Loaded(object sender, RoutedEventArgs e)
        {
            PART_Viewer.ScrollToHorizontalOffset(PART_Viewer.ScrollableWidth / 2.0);
            PART_Viewer.ScrollToVerticalOffset(PART_Viewer.ScrollableHeight / 2.0);
        }

        private void PART_Canvas_Drop(object sender, DragEventArgs e)
        {
            string[] type = e.Data.GetFormats();
            MatDataObject o = e.Data.GetData(type[0]) as MatDataObject;

            if (o != null && MatDataObjects != null)
            {
                MatDataObjects.Add(o);
                RefreshCanvas();
            }

        }

        public void RefreshCanvas()
        {
            foreach(MatDataObject o in MatDataObjects)
            {
                PART_Canvas.Children.Add(new MatDataObjectControl() { MyMatDataObject = o});
            }
        }
    }
}
