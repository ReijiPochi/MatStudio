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

namespace MatFramework.Controls
{
    public class LogConditionIndicator : Control
    {
        static LogConditionIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LogConditionIndicator), new FrameworkPropertyMetadata(typeof(LogConditionIndicator)));
        }

        public LogCondition Condition
        {
            get { return (LogCondition)GetValue(ConditionProperty); }
            set { SetValue(ConditionProperty, value); }
        }
        public static readonly DependencyProperty ConditionProperty =
            DependencyProperty.Register("Condition", typeof(LogCondition), typeof(LogConditionIndicator), new PropertyMetadata(LogCondition.None));


    }
}
