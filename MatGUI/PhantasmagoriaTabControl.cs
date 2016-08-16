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
    enum DragPosition
    {
        Top,
        Bottom,
        Left,
        Right,
        Center
    }

    public class PhantasmagoriaTabControl : TabControl
    {
        Rectangle maskRect;
        Rectangle posRect;
        DragPosition dragTarget;

        const double MIN_WIDTH = 80.0;
        const double MIN_HEIGHT = 80.0;

        static PhantasmagoriaTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhantasmagoriaTabControl), new FrameworkPropertyMetadata(typeof(PhantasmagoriaTabControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            maskRect = GetTemplateChild("IBMaskRect") as Rectangle;
            posRect = GetTemplateChild("IBPosRect") as Rectangle;

            DragEnter += PhantasmagoriaTabControl_DragEnter;
            maskRect.DragOver += MaskRect_DragOver;
            maskRect.Drop += MaskRect_Drop;
            maskRect.DragLeave += MaskRect_DragLeave;
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
        }


        private void PhantasmagoriaTabControl_DragEnter(object sender, DragEventArgs e)
        {
            PhantasmagoriaTabItem source = e.Data.GetData(typeof(PhantasmagoriaTabItem)) as PhantasmagoriaTabItem;
            if (source == null) return;

            maskRect.Visibility = Visibility.Visible;
        }

        private void MaskRect_DragOver(object sender, DragEventArgs e)
        {
            PhantasmagoriaTabItem source = e.Data.GetData(typeof(PhantasmagoriaTabItem)) as PhantasmagoriaTabItem;
            if (source == null) return;

            posRect.Visibility = Visibility.Visible;
            Point pos = e.GetPosition(maskRect);

            if(pos.X > maskRect.ActualWidth * 0.8)
            {
                dragTarget = DragPosition.Right;
                posRect.HorizontalAlignment = HorizontalAlignment.Right;
                posRect.VerticalAlignment = VerticalAlignment.Stretch;
                posRect.Width = maskRect.ActualWidth * 0.2;
                posRect.Height = double.NaN;
            }
            else if(pos.X < maskRect.ActualWidth * 0.2)
            {
                dragTarget = DragPosition.Left;
                posRect.HorizontalAlignment = HorizontalAlignment.Left;
                posRect.VerticalAlignment = VerticalAlignment.Stretch;
                posRect.Width = maskRect.ActualWidth * 0.2;
                posRect.Height = double.NaN;
            }
            else
            {
                if(pos.Y > maskRect.ActualHeight * 0.8)
                {
                    dragTarget = DragPosition.Bottom;
                    posRect.HorizontalAlignment = HorizontalAlignment.Stretch;
                    posRect.VerticalAlignment = VerticalAlignment.Bottom;
                    posRect.Width = double.NaN;
                    posRect.Height = maskRect.ActualHeight * 0.2;
                }
                else if(pos.Y < maskRect.ActualHeight * 0.2)
                {
                    dragTarget = DragPosition.Top;
                    posRect.HorizontalAlignment = HorizontalAlignment.Stretch;
                    posRect.VerticalAlignment = VerticalAlignment.Top;
                    posRect.Width = double.NaN;
                    posRect.Height = maskRect.ActualHeight * 0.2;
                }
                else
                {
                    dragTarget = DragPosition.Center;
                    posRect.HorizontalAlignment = HorizontalAlignment.Center;
                    posRect.VerticalAlignment = VerticalAlignment.Center;
                    posRect.Width = maskRect.ActualWidth * 0.6;
                    posRect.Height = maskRect.ActualHeight * 0.6;
                }
            }
        }

        private void MaskRect_Drop(object sender, DragEventArgs e)
        {
            posRect.Visibility = Visibility.Collapsed;
            maskRect.Visibility = Visibility.Collapsed;

            PhantasmagoriaTabItem source = e.Data.GetData(typeof(PhantasmagoriaTabItem)) as PhantasmagoriaTabItem;

            if (source != null)
            {
                PhantasmagoriaTabControl sourcesParent = source.Parent as PhantasmagoriaTabControl;
                if (sourcesParent == null) throw new Exception("MatPhantasmagoriaTabItem のParentが MatPhantasmagoriaTabControl でありません");

                sourcesParent.Items.Remove(source);

                if (sourcesParent.Items.Count == 0)
                {
                    if (sourcesParent != this)
                    {
                        RemoveSource(sourcesParent);
                    }
                    else
                    {
                        sourcesParent.Items.Add(source);
                        return;
                    }
                }

                if (dragTarget == DragPosition.Center)
                {
                    Items.Add(source);
                }
                else
                {
                    PhantasmagoriaTabControl sourcesNewParent = new PhantasmagoriaTabControl();
                    sourcesNewParent.Items.Add(source);
                    Grid sourcesNewGrid = new Grid();
                    sourcesNewGrid.Children.Add(sourcesNewParent);

                    PhantasmagoriaTabControl me = this;
                    Grid myParent = Parent as Grid;
                    if (myParent == null) throw new Exception("MatPhantasmagoriaTabControl のParentが Grid でありません");
                    myParent.Children.Remove(me);
                    Grid myNewGrid = new Grid();
                    myNewGrid.Children.Add(me);

                    switch (dragTarget)
                    {
                        case DragPosition.Top:
                            PutToTop(myParent, myNewGrid, sourcesNewGrid);
                            break;

                        case DragPosition.Bottom:
                            PutToBottom(myParent, myNewGrid, sourcesNewGrid);
                            break;

                        case DragPosition.Left:
                            PutToLeft(myParent, myNewGrid, sourcesNewGrid);
                            break;

                        case DragPosition.Right:
                            PutToRight(myParent, myNewGrid, sourcesNewGrid);
                            break;

                        default:
                            break;
                    }
                }

            }
        }

        private void MaskRect_DragLeave(object sender, DragEventArgs e)
        {
            posRect.Visibility = Visibility.Collapsed;
            maskRect.Visibility = Visibility.Collapsed;
        }

        public static void PutToTop(Grid parent, Grid me, Grid source)
        {
            parent.ColumnDefinitions.Clear();
            parent.RowDefinitions.Clear();

            PhantasmagoriaSplitter splitter = new PhantasmagoriaSplitter()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 8,
                Width = double.NaN
            };

            parent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(parent.ActualHeight * 0.5), MinHeight = MIN_HEIGHT });
            parent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            me.SetValue(Grid.RowProperty, 1);
            me.SetValue(Grid.RowSpanProperty, 1);
            me.Margin = new Thickness(0, 3, 0, 0);
            splitter.SetValue(Grid.RowProperty, 1);
            splitter.SetValue(Grid.RowSpanProperty, 1);
            splitter.Margin = new Thickness(0, -4, 0, 0);
            source.SetValue(Grid.RowProperty, 0);
            source.SetValue(Grid.RowSpanProperty, 1);
            source.Margin = new Thickness(0, 0, 0, 3);

            parent.Children.Add(me);
            parent.Children.Add(source);
            parent.Children.Add(splitter);
        }

        public static void PutToBottom(Grid parent, Grid me, Grid source)
        {
            parent.ColumnDefinitions.Clear();
            parent.RowDefinitions.Clear();

            PhantasmagoriaSplitter splitter = new PhantasmagoriaSplitter()
            {
                VerticalAlignment = VerticalAlignment.Bottom,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Height = 8,
                Width = double.NaN
            };

            parent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            parent.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(parent.ActualHeight * 0.5), MinHeight = MIN_HEIGHT });

            me.SetValue(Grid.RowProperty, 0);
            me.SetValue(Grid.RowSpanProperty, 1);
            me.Margin = new Thickness(0, 0, 0, 3);
            splitter.SetValue(Grid.RowProperty, 0);
            splitter.SetValue(Grid.RowSpanProperty, 1);
            splitter.Margin = new Thickness(0, 0, 0, -4);
            source.SetValue(Grid.RowProperty, 1);
            source.SetValue(Grid.RowSpanProperty, 1);
            source.Margin = new Thickness(0, 3, 0, 0);

            parent.Children.Add(me);
            parent.Children.Add(source);
            parent.Children.Add(splitter);
        }

        public static void PutToLeft(Grid parent, Grid me, Grid source)
        {
            parent.ColumnDefinitions.Clear();
            parent.RowDefinitions.Clear();

            PhantasmagoriaSplitter splitter = new PhantasmagoriaSplitter()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = double.NaN,
                Width = 8
            };

            parent.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(parent.ActualHeight * 0.5), MinWidth = MIN_WIDTH });
            parent.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            me.SetValue(Grid.ColumnProperty, 1);
            me.SetValue(Grid.ColumnSpanProperty, 1);
            me.Margin = new Thickness(3, 0, 0, 0);
            splitter.SetValue(Grid.ColumnProperty, 1);
            splitter.SetValue(Grid.ColumnSpanProperty, 1);
            splitter.Margin = new Thickness(-4, 0, 0, 0);
            source.SetValue(Grid.ColumnProperty, 0);
            source.SetValue(Grid.ColumnSpanProperty, 1);
            source.Margin = new Thickness(0, 0, 3, 0);

            parent.Children.Add(me);
            parent.Children.Add(source);
            parent.Children.Add(splitter);
        }

        public static void PutToRight(Grid parent, Grid me, Grid source)
        {
            parent.ColumnDefinitions.Clear();
            parent.RowDefinitions.Clear();

            PhantasmagoriaSplitter splitter = new PhantasmagoriaSplitter()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Right,
                Height = double.NaN,
                Width = 8
            };

            parent.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            parent.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(parent.ActualHeight * 0.5), MinWidth = MIN_WIDTH });

            me.SetValue(Grid.ColumnProperty, 0);
            me.SetValue(Grid.ColumnSpanProperty, 1);
            me.Margin = new Thickness(0, 0, 3, 0);
            splitter.SetValue(Grid.ColumnProperty, 0);
            splitter.SetValue(Grid.ColumnSpanProperty, 1);
            splitter.Margin = new Thickness(0, 0, -4, 0);
            source.SetValue(Grid.ColumnProperty, 1);
            source.SetValue(Grid.ColumnSpanProperty, 1);
            source.Margin = new Thickness(3, 0, 0, 0);

            parent.Children.Add(me);
            parent.Children.Add(source);
            parent.Children.Add(splitter);
        }

        public static void RemoveSource(PhantasmagoriaTabControl sourcesParent)
        {
            Grid sourcesGrid = sourcesParent.Parent as Grid;
            if (sourcesGrid == null) throw new Exception("MatPhantasmagoriaTabControl のParentが Grid でありません");
            Grid sourcesGridGrid = sourcesGrid.Parent as Grid;
            if (sourcesGridGrid == null) throw new Exception("Grid のParentが Grid でありません");
            sourcesGridGrid.Children.Remove(sourcesGrid);

            PhantasmagoriaSplitter s = null;
            Grid g = null;

            foreach (object o in sourcesGridGrid.Children)
            {
                if (s == null) s = o as PhantasmagoriaSplitter;
                if (g == null) g = o as Grid;
            }

            if (s != null)
            {
                sourcesGridGrid.Children.Remove(s);
            }

            if (g != null)
            {
                PhantasmagoriaTabControl t = g.Children[0] as PhantasmagoriaTabControl;
                Grid gg = g.Parent as Grid;

                if (t != null)
                {
                    g.Children.Remove(t);
                    sourcesGridGrid.Children.Remove(g);
                    sourcesGridGrid.ColumnDefinitions.Clear();
                    sourcesGridGrid.RowDefinitions.Clear();
                    sourcesGridGrid.SetValue(Grid.ColumnProperty, 0);
                    sourcesGridGrid.SetValue(Grid.RowProperty, 0);
                    sourcesGridGrid.Children.Add(t);
                }

                if (gg != null)
                {
                    g.SetValue(Grid.ColumnProperty, 0);
                    g.SetValue(Grid.RowProperty, 0);
                    g.Margin = new Thickness(0);
                    gg.ColumnDefinitions.Clear();
                    gg.RowDefinitions.Clear();
                    gg.Children.Remove(g);

                    Grid ggg = gg.Parent as Grid;
                    if (ggg != null)
                    {
                        ggg.Children.Remove(gg);

                        if (g.Children.Count != 0)
                        {
                            ggg.Children.Add(g);
                        }
                        else
                        {
                            Grid _g = null;
                            foreach(object o in ggg.Children)
                            {
                                if (_g == null) _g = o as Grid;
                            }
                            if (_g != null)
                            {
                                if ((int)_g.GetValue(Grid.ColumnProperty) == 0)
                                    sourcesGridGrid.SetValue(Grid.ColumnProperty, 1);
                                if ((int)_g.GetValue(Grid.RowProperty) == 0)
                                    sourcesGridGrid.SetValue(Grid.RowProperty, 1);
                            }

                            ggg.Children.Add(sourcesGridGrid);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// i1 と i2 の場所を入れ替えます
        /// </summary>
        /// <param name="i1"></param>
        /// <param name="i2"></param>
        public void ReplaceItems(PhantasmagoriaTabItem i1, PhantasmagoriaTabItem i2)
        {
            int index_ti1 = Items.IndexOf(i1);
            int index_ti2 = Items.IndexOf(i2);

            Items.Remove(i1);
            Items.Remove(i2);

            if (index_ti1 > index_ti2)
            {
                Items.Insert(index_ti1 - 1, i2);
                Items.Insert(index_ti2, i1);
            }
            else
            {
                Items.Insert(index_ti1, i2);
                Items.Insert(index_ti2, i1);
            }
        }
    }
}
