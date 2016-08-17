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

namespace MatGUI
{
    public class PhantasmagoriaWorkspaceFrame : Control
    {
        static PhantasmagoriaWorkspaceFrame()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhantasmagoriaWorkspaceFrame), new FrameworkPropertyMetadata(typeof(PhantasmagoriaWorkspaceFrame)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DragEnter += PhantasmagoriaWorkspaceFrame_DragEnter;
            Drop += PhantasmagoriaWorkspaceFrame_Drop;
            DragLeave += PhantasmagoriaWorkspaceFrame_DragLeave;
        }

        private void PhantasmagoriaWorkspaceFrame_DragEnter(object sender, DragEventArgs e)
        {
            PhantasmagoriaTabItem source = e.Data.GetData(typeof(PhantasmagoriaTabItem)) as PhantasmagoriaTabItem;
            if (source == null) return;

            if(HorizontalAlignment == HorizontalAlignment.Left || HorizontalAlignment == HorizontalAlignment.Right)
            {
                Width = 8.0;
            }
            else if(VerticalAlignment == VerticalAlignment.Top || VerticalAlignment == VerticalAlignment.Bottom)
            {
                Height = 8.0;
            }

            Background = new SolidColorBrush(Color.FromArgb(160, 2, 140, 255));
        }

        private void PhantasmagoriaWorkspaceFrame_Drop(object sender, DragEventArgs e)
        {
            if (HorizontalAlignment == HorizontalAlignment.Left || HorizontalAlignment == HorizontalAlignment.Right)
            {
                Width = 3.0;
            }
            else if (VerticalAlignment == VerticalAlignment.Top || VerticalAlignment == VerticalAlignment.Bottom)
            {
                Height = 3.0;
            }

            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            PhantasmagoriaTabItem source = e.Data.GetData(typeof(PhantasmagoriaTabItem)) as PhantasmagoriaTabItem;
            if (source == null) return;
            PhantasmagoriaTabControl sourcesParent = source.Parent as PhantasmagoriaTabControl;
            if (sourcesParent == null) throw new Exception("MatPhantasmagoriaTabItem のParentが MatPhantasmagoriaTabControl でありません");

            sourcesParent.Items.Remove(source);

            if (sourcesParent.Items.Count == 0)
            {
                PhantasmagoriaTabControl.RemoveSource(sourcesParent);
            }

            Grid myParent = Parent as Grid;
            if (myParent == null)
            {
                sourcesParent.Items.Add(source);
                return;
            }

            Grid targetGrid, g1, g2;

            g1 = new Grid();

            g2 = new Grid();
            PhantasmagoriaTabControl newTabControl = new PhantasmagoriaTabControl();
            newTabControl.Items.Add(source);
            g2.Children.Add(newTabControl);

            if (HorizontalAlignment == HorizontalAlignment.Left)
            {
                targetGrid = PhantasmagoriaSplitter.SearchGridInGridsChildren(myParent, 0, 1);
                if (targetGrid != null)
                {
                    PhantasmagoriaSplitter.GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToLeft(targetGrid, g1, g2);
                }
            }
            else if (HorizontalAlignment == HorizontalAlignment.Right)
            {
                targetGrid = PhantasmagoriaSplitter.SearchGridInGridsChildren(myParent, 0, 0);
                if (targetGrid != null)
                {
                    PhantasmagoriaSplitter.GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToRight(targetGrid, g1, g2);
                }
            }
            else if (VerticalAlignment == VerticalAlignment.Top)
            {
                targetGrid = PhantasmagoriaSplitter.SearchGridInGridsChildren(myParent, 1, 0);
                if (targetGrid != null)
                {
                    PhantasmagoriaSplitter.GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToTop(targetGrid, g1, g2);
                }
            }
            else if (VerticalAlignment == VerticalAlignment.Bottom)
            {
                targetGrid = PhantasmagoriaSplitter.SearchGridInGridsChildren(myParent, 0, 0);
                if (targetGrid != null)
                {
                    PhantasmagoriaSplitter.GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToBottom(targetGrid, g1, g2);
                }
            }
        }

        private void PhantasmagoriaWorkspaceFrame_DragLeave(object sender, DragEventArgs e)
        {
            if (HorizontalAlignment == HorizontalAlignment.Left || HorizontalAlignment == HorizontalAlignment.Right)
            {
                Width = 3.0;
            }
            else if (VerticalAlignment == VerticalAlignment.Top || VerticalAlignment == VerticalAlignment.Bottom)
            {
                Height = 3.0;
            }

            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
    }
}
