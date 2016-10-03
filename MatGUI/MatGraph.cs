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
using System.ComponentModel;
using System.Windows.Forms.Integration;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace MatGUI
{
    public class MatGraph : Control
    {
        static MatGraph()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MatGraph), new FrameworkPropertyMetadata(typeof(MatGraph)));
        }

        public MatGraph()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                glc = new GLControl(new GraphicsMode(GraphicsMode.Default.AccumulatorFormat, 24, 0, 0));
                glc.Load += Glc_Load;
                glc.Paint += Glc_Paint;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            WindowsFormsHost host = GetTemplateChild("Host") as WindowsFormsHost;
            host.Child = glc;
            host.SizeChanged += Host_SizeChanged;
        }

        private GLControl glc;

        [Description("ドローイングエリアの背景色"), Category("IBFramework")]
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(MatGraph), new PropertyMetadata(Color.FromArgb(255, 50, 50, 50), new PropertyChangedCallback(OnBackgroundColorChanged)));

        private static void OnBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MatGraph sender = d as MatGraph;
            if (sender != null)
            {
                sender.glc.MakeCurrent();
                GL.ClearColor(sender.BackgroundColor.R / 255.0f, sender.BackgroundColor.G / 255.0f, sender.BackgroundColor.B / 255.0f, 1.0f);
                sender.glc.Refresh();
            }
        }


        private void Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            glc.Width = (int)ActualWidth;
            glc.Height = (int)ActualHeight;
        }

        private void Glc_Load(object sender, EventArgs e)
        {
            glc.MakeCurrent();
        }

        private void Glc_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            glc.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit);

            glc.SwapBuffers();
        }

    }
}
