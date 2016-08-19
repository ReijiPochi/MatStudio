using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class MatWorkspace : ContentControl
    {
        static MatWorkspace()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatWorkspace), new FrameworkPropertyMetadata(typeof(MatWorkspace)));
        }

        public MatWorkspace()
        {
            AllMatWorkspace.Add(this);
            Unloaded += MatWorkspace_Unloaded;
            AddHandler(PhantasmagoriaTabItem.MatPanelActivatedEvent, new PhantasmagoriaTabItem.MatPanelActivatedEventHandler(ChildPanelActivated));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Window owner = Window.GetWindow(this);

            if (owner == null) return;

            if (owner == Application.Current.MainWindow)
                IsMainWindowContent = true;

            owner.Activated += MatWorkspace_Activated;
            owner.Deactivated += MatWorkspace_Deactivated;
        }

        private void MatWorkspace_Activated(object sender, EventArgs e)
        {
            if (LastActivePanel != null)
            {
                LastActivePanel.PanelActivate();
            }
        }

        private void MatWorkspace_Deactivated(object sender, EventArgs e)
        {
            if (LastActivePanel != null)
            {
                LastActivePanel.IsActivePanel = false;
            }
        }

        private PhantasmagoriaTabItem LastActivePanel;

        private void MatWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            AllMatWorkspace.Remove(this);
        }

        private void ChildPanelActivated(object sender, MatPanelActivatedEventArgs e)
        {
            LastActivePanel = e.OriginalSource as PhantasmagoriaTabItem;
        }

        [Description("メインウインドウのコンテントであれば true "), Category("IBGUI"), DefaultValue(false)]
        public bool IsMainWindowContent { get; private set; }

        public static List<MatWorkspace> AllMatWorkspace = new List<MatWorkspace>();

        public static void SetToMainwindowContent(MatWorkspace item)
        {
            foreach (MatWorkspace ws in AllMatWorkspace)
            {
                if (ws.IsMainWindowContent)
                {
                    ws.Content = item.Content;
                }
            }
        }
    }
}
