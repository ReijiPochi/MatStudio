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
using MatFramework;

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
                glc = new GLControl(new GraphicsMode(GraphicsMode.Default.AccumulatorFormat, 24, 0, 4));
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

        public List<Coord2D> Data { get; private set; } = new List<Coord2D>();

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
            SetCam();
            glc.Refresh();
        }

        private void Glc_Load(object sender, EventArgs e)
        {
            glc.MakeCurrent();

            GL.Enable(EnableCap.LineSmooth);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);

            SetCam();
        }

        private void Glc_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            glc.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.LineWidth(1.5f);

            GL.Color4(1.0f, 0.2f, 0.4f, 0.7f);
            GL.Begin(PrimitiveType.LineStrip);
            {
                GL.Vertex3(-10.0, -10.0, 0.0);
                GL.Vertex3(10.0, 10.0, 0.0);
                GL.Vertex3(100.0, 20.0, 0.0);
            }
            GL.End();

            GL.Color4(0.2f, 0.6f, 1.0f, 0.7f);
            GL.Begin(PrimitiveType.LineStrip);
            {
                GL.Vertex3(-10.0, -5.0, 1.0);
                GL.Vertex3(20.0, 10.0, 1.0);
                GL.Vertex3(110.0, 30.0, 1.0);
            }
            GL.End();

            glc.SwapBuffers();
        }

        private void SetCam()
        {
            GL.Viewport(0, 0, glc.Width, glc.Height);

            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 proj = Matrix4.CreateOrthographic(glc.Width, glc.Height, 0.01f, 64.0f);
            GL.LoadMatrix(ref proj);

            GL.MatrixMode(MatrixMode.Modelview);

            float x = (float)(glc.Width / 2.0);

            Matrix4 look = Matrix4.LookAt(new Vector3(x, 0, 32.0f), new Vector3(x, 0, 0.0f), Vector3.UnitY);
            GL.LoadMatrix(ref look);
        }
    }
}
