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

using System.Windows.Markup;

namespace MatGUI
{
    public class MatPanelActivatedEventArgs : RoutedEventArgs
    {
        public MatPanelActivatedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }
    }

    public class PhantasmagoriaTabItem : TabItem
    {
        static PhantasmagoriaTabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhantasmagoriaTabItem), new FrameworkPropertyMetadata(typeof(PhantasmagoriaTabItem)));
        }

        public PhantasmagoriaTabItem()
        {
            AllPhantasmagoriaTabItem.Add(this);
        }

        public static List<PhantasmagoriaTabItem> AllPhantasmagoriaTabItem = new List<PhantasmagoriaTabItem>();

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            MaskRect = GetTemplateChild("MatMaskRect") as Rectangle;
            MaskRect.MouseLeave += MatPhantasmagoriaTabItem_MouseLeave;
            MaskRect.DragEnter += MatPhantasmagoriaTabItem_DragEnter;
        }

        public bool IsActivePanel
        {
            get { return (bool)GetValue(IsActivePanelProperty); }
            set { SetValue(IsActivePanelProperty, value); }
        }
        public static readonly DependencyProperty IsActivePanelProperty =
            DependencyProperty.Register("IsActivePanel", typeof(bool), typeof(PhantasmagoriaTabItem), new PropertyMetadata(false));


        public delegate void MatPanelActivatedEventHandler(object sender, MatPanelActivatedEventArgs e);
        public static RoutedEvent MatPanelActivatedEvent = EventManager.RegisterRoutedEvent(
            "PanelActivated", RoutingStrategy.Bubble, typeof(MatPanelActivatedEventHandler), typeof(PhantasmagoriaTabItem));
        public event MatPanelActivatedEventHandler MatPanelActivated
        {
            add { AddHandler(MatPanelActivatedEvent, value); }
            remove { RemoveHandler(MatPanelActivatedEvent, value); }
        }

        /// <summary>
        /// ドラッグ、ドロップ用のマスク
        /// </summary>
        protected Rectangle MaskRect;


        private void MatPhantasmagoriaTabItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(this, this, DragDropEffects.Move);
                IsSelected = true;
            }
        }

        private void MatPhantasmagoriaTabItem_DragEnter(object sender, DragEventArgs e)
        {
            PhantasmagoriaTabItem source = e.Data.GetData(typeof(PhantasmagoriaTabItem)) as PhantasmagoriaTabItem;

            e.Handled = true;

            if (source != null && source != this)
            {
                if(source.Parent == Parent)
                {
                    PhantasmagoriaTabControl p = Parent as PhantasmagoriaTabControl;
                    if (p == null) throw new Exception("MatPhantasmagoriaTabItem のParentが MatPhantasmagoriaTabControl でありません");

                    p.ReplaceItems(this, source);
                    source.IsSelected = true;
                }
            }
        }

        public void RemoveFromParent()
        {
            PhantasmagoriaTabControl p = Parent as PhantasmagoriaTabControl;

            if(p != null)
            {
                p.Items.Remove(this);
            }
        }

        public PhantasmagoriaTabItem Clone()
        {
            string data = XamlWriter.Save(this);
            PhantasmagoriaTabItem i = XamlReader.Parse(data) as PhantasmagoriaTabItem;

            return i;
        }

        public virtual void PanelActivate()
        {
            foreach (PhantasmagoriaTabItem i in AllPhantasmagoriaTabItem)
            {
                i.IsActivePanel = false;
            }

            Window owner = Window.GetWindow(this);
            if (owner != null && owner.IsActive)
            {
                IsActivePanel = true;
                FrameworkElement child = Content as FrameworkElement;
                if (child != null)
                {
                    child.Focus();

                    if (child as MatControlPanelBase != null)
                    {
                        ((MatControlPanelBase)child).IsActivePanel = true;
                    }
                }
            }

            RaiseEvent(new MatPanelActivatedEventArgs(MatPanelActivatedEvent));
        }
    }
}
