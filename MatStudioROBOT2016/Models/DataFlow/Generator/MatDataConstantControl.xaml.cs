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

namespace MatStudioROBOT2016.Models.DataFlow.Generator
{
    /// <summary>
    /// Interaction logic for MatDataConstantControl.xaml
    /// </summary>
    public partial class MatDataConstantControl : UserControl
    {
        public MatDataConstantControl()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (DataContext as MatDataConstant == null) return;

            owner = (MatDataConstant)DataContext;

            ConstantBox.DoubleValue = owner.ConstantValue;
            ConstantBox.TextChanged += ConstantBox_TextChanged;
        }

        MatDataConstant owner;

        private void ConstantBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            owner.ConstantValue = ConstantBox.DoubleValue;
        }
    }
}
