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
    public class PhantasmagoriaSplitter : GridSplitter
    {
        static PhantasmagoriaSplitter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhantasmagoriaSplitter), new FrameworkPropertyMetadata(typeof(PhantasmagoriaSplitter)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            DragEnter += PhantasmagoriaSplitter_DragEnter;
            Drop += PhantasmagoriaSplitter_Drop;
            DragLeave += PhantasmagoriaSplitter_DragLeave;
        }

        private void PhantasmagoriaSplitter_DragEnter(object sender, DragEventArgs e)
        {
            PhantasmagoriaTabItem source = e.Data.GetData(typeof(PhantasmagoriaTabItem)) as PhantasmagoriaTabItem;
            if (source == null) return;

            Background = new SolidColorBrush(Color.FromArgb(100, 2, 129, 255));
        }

        private void PhantasmagoriaSplitter_Drop(object sender, DragEventArgs e)
        {
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
                targetGrid = SearchGridInGridsChildren(myParent, 0, 1);
                if(targetGrid != null)
                {
                    GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToLeft(targetGrid, g1, g2);
                }
            }
            else if (HorizontalAlignment == HorizontalAlignment.Right)
            {
                targetGrid = SearchGridInGridsChildren(myParent, 0, 0);
                if (targetGrid != null)
                {
                    GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToRight(targetGrid, g1, g2);
                }
            }
            else if (VerticalAlignment == VerticalAlignment.Top)
            {
                targetGrid = SearchGridInGridsChildren(myParent, 1, 0);
                if (targetGrid != null)
                {
                    GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToTop(targetGrid, g1, g2);
                }
            }
            else if (VerticalAlignment == VerticalAlignment.Bottom)
            {
                targetGrid = SearchGridInGridsChildren(myParent, 0, 0);
                if (targetGrid != null)
                {
                    GridCopyToGrid(targetGrid, g1);
                    PhantasmagoriaTabControl.PutToBottom(targetGrid, g1, g2);
                }
            }
        }

        private void PhantasmagoriaSplitter_DragLeave(object sender, DragEventArgs e)
        {
            Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }


        public static Grid SearchGridInGridsChildren(Grid target, int row, int column)
        {
            Grid g = null;

            foreach (object o in target.Children)
            {
                g = o as Grid;
                if (g != null)
                {
                    if ((int)g.GetValue(Grid.ColumnProperty) == column && (int)g.GetValue(Grid.RowProperty) == row)
                    {
                        return g;
                    }
                }
            }

            // 見つからなかったら、最後のGridを返す
            return g;
        }

        public static void GridCopyToGrid(Grid from, Grid to)
        {
            UIElement[] temp = new UIElement[from.Children.Count];
            from.Children.CopyTo(temp, 0);
            from.Children.Clear();

            for (int i = 0; i < temp.Length; i++)
            {
                to.Children.Add(temp[i]);
            }

            to.ColumnDefinitions.Clear();
            to.RowDefinitions.Clear();

            ColumnDefinition[] tempColumns = new ColumnDefinition[from.ColumnDefinitions.Count];
            RowDefinition[] tempRows = new RowDefinition[from.RowDefinitions.Count];

            from.ColumnDefinitions.CopyTo(tempColumns, 0);
            from.RowDefinitions.CopyTo(tempRows, 0);

            from.ColumnDefinitions.Clear();
            from.RowDefinitions.Clear();

            for(int i = 0; i < tempColumns.Length; i++)
            {
                to.ColumnDefinitions.Add(tempColumns[i]);
            }

            for(int i= 0; i < tempRows.Length; i++)
            {
                to.RowDefinitions.Add(tempRows[i]);
            }

            to.Margin = new Thickness(from.Margin.Left, from.Margin.Top, from.Margin.Right, from.Margin.Bottom);
        }
    }
}
