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
using System.Windows.Shapes;

namespace MatGUI
{
    /// <summary>
    /// Interaction logic for MatWindow.xaml
    /// </summary>
    public partial class MatWindow : Window
    {
        public MatWindow()
        {
            InitializeComponent();

            AllMatWindows.Add(this);
            Resources = Application.Current.Resources;
        }

        private static List<MatWindow> AllMatWindows = new List<MatWindow>();

        public void SetTabItem(PhantasmagoriaTabItem item)
        {
            item.RemoveFromParent();
            MainTabControl.Items.Add(item);
            item.IsSelected = true;
        }

        public static void AllWindowTopmostOn()
        {
            foreach (MatWindow ibw in AllMatWindows)
            {
                ibw.Topmost = true;
            }
        }

        public static void AllWindowTopmostOff()
        {
            foreach (MatWindow ibw in AllMatWindows)
            {
                ibw.Topmost = false;
            }
        }

        public static void AllWindowClose()
        {
            while (AllMatWindows.Count > 0)
            {
                AllMatWindows[0].Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AllMatWindows.Remove(this);
        }
    }
}
