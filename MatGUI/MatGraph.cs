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
    public enum MatGraphMode
    {
        DistributionMap,
        DistributionMapWithPoints,
        Histgram
    }

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
                glc.MouseMove += Glc_MouseMove;
                glc.MouseLeave += Glc_MouseLeave
                    ;
                tp.InitialDelay = 0;
                tp.ReshowDelay = 0;
                tp.AutoPopDelay = 60000;
                tp.UseAnimation = false;
                tp.ShowAlways = true;

                tp.SetToolTip(glc, "test");
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
        private System.Windows.Forms.ToolTip tp = new System.Windows.Forms.ToolTip();
        private double zoomDispX;
        private double zoomDispY;
        private double mousePosX = double.NaN;
        private Coord2D hitPoint = new Coord2D(double.NaN, double.NaN);

        public List<Coord2D> Data1 { get; set; } = new List<Coord2D>();
        public List<Coord2D> Data2 { get; set; } = new List<Coord2D>();
        public double ZoomX { get; set; } = 1.0;
        public double ZoomY { get; set; } = 1.0;
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


        public MatGraphMode Mode
        {
            get { return (MatGraphMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(MatGraphMode), typeof(MatGraph), new PropertyMetadata(MatGraphMode.DistributionMap));




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

            switch(Mode)
            {
                case MatGraphMode.DistributionMap:
                case MatGraphMode.DistributionMapWithPoints:

                    GL.LineWidth((float)(1.5 * zoomDispX));
                    GL.PointSize((float)(4.0 * zoomDispX));

                    GL.Color4(0.3f, 0.6f, 1.0f, 0.7f);
                    RenderDataLines(Data1, 0.0);
                    if (Mode == MatGraphMode.DistributionMapWithPoints)
                        RenderDataPoints(Data1, 0.1);

                    GL.Color4(1.0f, 0.3f, 0.5f, 0.7f);
                    RenderDataLines(Data2, 1.0);
                    if (Mode == MatGraphMode.DistributionMapWithPoints)
                        RenderDataPoints(Data2, 0.1);

                    GL.LineWidth((float)(1.5 * zoomDispX));
                    GL.Color4(0.0f, 1.0f, 0.3f, 0.7f);
                    if (mousePosX != double.NaN)
                    {
                        GL.Begin(PrimitiveType.LineStrip);
                        GL.Vertex2(mousePosX * ZoomX * zoomDispX, 1000.0);
                        GL.Vertex2(mousePosX * ZoomX * zoomDispX, -1000.0);
                        GL.End();
                    }
                    break;

                case MatGraphMode.Histgram:

                    GL.LineWidth((float)(10.0 * zoomDispX));

                    GL.Color4(0.3f, 0.6f, 1.0f, 0.7f);
                    RenderDataBars(Data1, 0.0);

                    GL.Color4(1.0f, 0.3f, 0.5f, 0.7f);
                    RenderDataBars(Data2, 1.0);

                    if(hitPoint.X != double.NaN && hitPoint.Y != double.NaN)
                    {
                        GL.LineWidth((float)(10.0 * zoomDispX));
                        GL.Color4(0.0f, 1.0f, 0.3f, 0.7f);

                        GL.Begin(PrimitiveType.LineStrip);
                        GL.Vertex3((hitPoint.X - OffsetX) * ZoomX * zoomDispX, 0.0, 10.0);
                        GL.Vertex3((hitPoint.X - OffsetX) * ZoomX * zoomDispX, hitPoint.Y * ZoomY * zoomDispY, 10.0);
                        GL.End();
                    }
                    break;

                default:
                    break;
            }

            glc.SwapBuffers();
        }

        private void Glc_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousePosX = e.X / (ZoomX * zoomDispX);
            glc.Refresh();

            double trgX = mousePosX + OffsetX;

            double x1_1 = 0.0, y1_1 = 0.0, x1_2 = 0.0, y1_2 = 0.0;
            bool valueAct1 = false;

            double x2_1 = 0.0, y2_1 = 0.0, x2_2 = 0.0, y2_2 = 0.0;
            bool valueAct2 = false;

            string y1 = "*", y2 = "*";

            foreach (Coord2D d1 in Data1)
            {
                if(d1.X >= trgX)
                {
                    x1_2 = d1.X;
                    y1_2 = d1.Y;
                    valueAct1 = true;
                    break;
                }

                x1_1 = d1.X;
                y1_1 = d1.Y;
            }

            foreach (Coord2D d2 in Data2)
            {
                if (d2.X >= trgX)
                {
                    x2_2 = d2.X;
                    y2_2 = d2.Y;
                    valueAct2 = true;
                    break;
                }

                x2_1 = d2.X;
                y2_1 = d2.Y;
            }

            hitPoint.X = x1_1;
            hitPoint.Y = y1_1;

            if (valueAct1)
                y1 = (y1_1 + (y1_2 - y1_1) * ((trgX - x1_1) / (x1_2 - x1_1))).ToString("f6");

            if (valueAct2)
                y1 = (y2_1 + (y2_2 - y2_1) * ((trgX - x2_1) / (x2_2 - x2_1))).ToString("f6");

            switch (Mode)
            {
                case MatGraphMode.DistributionMap:
                case MatGraphMode.DistributionMapWithPoints:
                    tp.SetToolTip(glc, "X: " + trgX.ToString("f6") + "\nData1: " + y1 + "\nData2: " + y2);
                    break;

                case MatGraphMode.Histgram:
                    tp.SetToolTip(glc, "X: " + x1_1.ToString("f6") + "\nData1: " + y1_1.ToString("f6") + "\nData2: " + y2_1.ToString("f6"));
                    break;

                default:
                    break;
            }

        }

        private void Glc_MouseLeave(object sender, EventArgs e)
        {
            mousePosX = double.NaN;
            hitPoint.X = double.NaN;
            hitPoint.Y = double.NaN;
            glc.Refresh();
        }

        private void RenderDataPoints(List<Coord2D> dataSet, double layer)
        {
            bool started = false;


            GL.Begin(PrimitiveType.Points);

            for(int i = 0; i < dataSet.Count; i++)
            {
                if (!started && i + 1 < dataSet.Count && dataSet[i + 1].X >= OffsetX)
                {
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, dataSet[i].Y * ZoomY * zoomDispY, layer);
                    started = true;
                }
                else
                {
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, dataSet[i].Y * ZoomY * zoomDispY, layer);

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
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, dataSet[i].Y * ZoomY * zoomDispY, layer);
                    started = true;
                }
                else
                {
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, dataSet[i].Y * ZoomY * zoomDispY, layer);

                    if (dataSet[i].X > EndX)
                    {
                        GL.End();
                        return;
                    }
                }
            }

            GL.End();
        }

        private void RenderDataBars(List<Coord2D> dataSet, double layer)
        {
            bool started = false;


            GL.Begin(PrimitiveType.Lines);

            for (int i = 0; i < dataSet.Count; i++)
            {
                if (!started && i + 1 < dataSet.Count && dataSet[i + 1].X >= OffsetX)
                {
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, 0.0, layer);
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, dataSet[i].Y * ZoomY * zoomDispY, layer);
                    started = true;
                }
                else
                {
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, 0.0, layer);
                    GL.Vertex3((dataSet[i].X - OffsetX) * ZoomX * zoomDispX, dataSet[i].Y * ZoomY * zoomDispY, layer);

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
            glc.MakeCurrent();

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
