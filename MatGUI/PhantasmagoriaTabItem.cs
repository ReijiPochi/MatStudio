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

            PreviewMouseDown += PhantasmagoriaTabItem_PreviewMouseDown;

            MaskRect = GetTemplateChild("MatMaskRect") as Rectangle;
            MaskRect.MouseDown += MaskRect_MouseDown;
            MaskRect.MouseLeave += MatPhantasmagoriaTabItem_MouseLeave;
            MaskRect.DragEnter += MatPhantasmagoriaTabItem_DragEnter;

            PanelActivate();
            IsSelected = true;
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



        private void PhantasmagoriaTabItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PanelActivate();
        }

        private void MaskRect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PanelActivate();
        }

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
                    //child.Focus();

                    if (child as MatControlPanelBase != null)
                    {
                        ((MatControlPanelBase)child).IsActivePanel = true;
                    }
                }
            }

            RaiseEvent(new MatPanelActivatedEventArgs(MatPanelActivatedEvent));
        }
    }

    public class TabItemVM : MatViewModelBase
    {
        #region CloseCommand
        private MatListenerCommand<object> _CloseCommand;

        public MatListenerCommand<object> CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = new MatListenerCommand<object>(Close);
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
            PhantasmagoriaTabItem trg = cm.PlacementTarget as PhantasmagoriaTabItem;
            if (trg == null)
                throw new Exception("コマンドターゲットが不正、または取得できません");

            PhantasmagoriaTabControl tc = trg.Parent as PhantasmagoriaTabControl;
            if (tc == null)
                throw new Exception("コマンドターゲットが不正、または取得できません");
            //IBPanel panel = tc.Parent as IBPanel;
            //if (panel == null)
            //    throw new IBDisableCommandException("コマンドターゲットが不正、または取得できません");


            trg.RemoveFromParent();

            if (tc.Items.Count == 0)
            {
                PhantasmagoriaTabControl.RemoveSource(tc);
            }
        }
        #endregion
    }
}
