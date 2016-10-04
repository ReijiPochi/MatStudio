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

            PresentationSource s = PresentationSource.FromVisual(this);
            zoomDispX = s.CompositionTarget.TransformToDevice.M11;
            zoomDispY = s.CompositionTarget.TransformToDevice.M22;

            WindowsFormsHost host = GetTemplateChild("Host") as WindowsFormsHost;
            host.Child = glc;
            host.SizeChanged += Host_SizeChanged;
        }

        private GLControl glc;
        private double zoomDispX;
        private double zoomDispY;

        public List<Coord2D> Data1 { get; private set; } = new List<Coord2D>();
        public List<Coord2D> Data2 { get; private set; } = new List<Coord2D>();

        public double OffsetX { get; set; } = 0;

        public double EndX
        {
            get
            {
                return OffsetX + ActualWidth;
            }
        }

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



        public void RefreshGraph()
        {
            glc.Refresh();
        }


        private void Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            glc.Width = (int)(ActualWidth * zoomDispX);
            glc.Height = (int)(ActualHeight * zoomDispY);
            SetCam();
            glc.Refresh();
        }

        private void Glc_Load(object sender, EventArgs e)
        {
            glc.MakeCurrent();

            GL.Enable(EnableCap.LineSmooth);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            SetCam();
        }

        private void Glc_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            glc.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.LineWidth((float)(1.5 * zoomDispX));
            GL.PointSize((float)(4.0 * zoomDispX));

            GL.Color4(0.3f, 0.6f, 1.0f, 0.7f);
            RenderDataLines(Data1, 0.0);
            //RenderDataPoints(Data1, 0.1);

            GL.Color4(1.0f, 0.3f, 0.5f, 0.7f);
            RenderDataLines(Data2, 1.0);
            //RenderDataPoints(Data2, 1.1);

            glc.SwapBuffers();
        }

        private void RenderDataPoints(List<Coord2D> dataSet, double layer)
        {
            bool started = false;


            GL.Begin(PrimitiveType.Points);

            for(int i = 0; i < dataSet.Count; i++)
            {
                if (!started && i + 1 < dataSet.Count && dataSet[i + 1].X >= OffsetX)
                {
                    GL.Vertex3(dataSet[i].X - OffsetX, dataSet[i].Y, layer);
                    started = true;
                }
                else
                {
                    GL.Vertex3(dataSet[i].X - OffsetX, dataSet[i].Y, layer);

                    if (dataSet[i].X > EndX)
                    {
                        GL.End();
                        return;
                    }
                }
            }

            GL.End();
        }

        private void RenderDataLines(List<Coord2D> dataSet, double layer)
        {
            bool started = false;


            GL.Begin(PrimitiveType.LineStrip);

            for (int i = 0; i < dataSet.Count; i++)
            {
                if (!started && i + 1 < dataSet.Count && dataSet[i + 1].X >= OffsetX)
                {
                    GL.Vertex3(dataSet[i].X - OffsetX, dataSet[i].Y, layer);
                    started = true;
                }
                else
                {
                    GL.Vertex3(dataSet[i].X - OffsetX, dataSet[i].Y, layer);

                    if (dataSet[i].X > EndX)
                    {
                        GL.End();
                        return;
                    }
                }
            }

            GL.End();
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
