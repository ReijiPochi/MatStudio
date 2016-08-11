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
    public class PhantasmagoriaTabControl : TabControl
    {
        static PhantasmagoriaTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PhantasmagoriaTabControl), new FrameworkPropertyMetadata(typeof(PhantasmagoriaTabControl)));
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);
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
