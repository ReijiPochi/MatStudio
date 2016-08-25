using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Input;
using System.Windows;
using MatFramework.DataFlow;

namespace MatStudioROBOT2016.Views.ControlPanels
{
    public class ModulesBehaviors : Behavior<Border>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(AssociatedObject, (MatDataObject)AssociatedObject.DataContext, DragDropEffects.Move);
            }
        }

    }
}
