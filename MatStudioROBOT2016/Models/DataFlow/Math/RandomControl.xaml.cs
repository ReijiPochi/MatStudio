using MatFramework;
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

namespace MatStudioROBOT2016.Models.DataFlow.Math
{
    /// <summary>
    /// RandomControl.xaml の相互作用ロジック
    /// </summary>
    public partial class RandomControl : UserControl
    {
        public RandomControl()
        {
            InitializeComponent();
        }

        public void SetHistogram(List<Coord2D> data)
        {
            Histogram.Data1 = data;
            Histogram.RefreshGraph();
        }

        public void SetResultString(string result)
        {
            ResultTb.Text = result;
        }

        private void Calc_Click(object sender, RoutedEventArgs e)
        {
            Random owner = DataContext as Random;

            if(owner != null)
            {
                owner.Calc(sigma.DoubleValue);
            }
        }
    }
}
