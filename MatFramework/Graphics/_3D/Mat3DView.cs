using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MatFramework.Graphics._3D
{
    public class Mat3DView : IDisposable
    {
        private static SlimDX.Direct3D11.Device _GraphicsDevice;
        public static SlimDX.Direct3D11.Device GraphicsDevice
        {
            get { return _GraphicsDevice; }
        }

        private SwapChain _SwapChain;
        public SwapChain SwapChain
        {
            get { return _SwapChain; }
        }

        public RenderTargetView RenderTarget { get; private set; }
        public DepthStencilView DepthStencil { get; private set; }

        public Window Host { get; private set; }
        public Border ViewArea { get; private set; }

        public MatWorld CurrentWorld { get; set; }

        public MatTimer AnimationClock { get; private set; }

        public Mat3DView()
        {
            Host = new Window() { Background = new LinearGradientBrush(Color.FromRgb(30,30,60),Color.FromRgb(10,10,10),90) };
            ViewArea = new Border();
            Host.WindowStyle = WindowStyle.None;
            Host.WindowState = WindowState.Maximized;
            Host.Content = ViewArea;
            Host.SizeChanged += Host_SizeChanged;
            Host.Closing += Host_Closing;

            Host.Show();

            CreateDeviceAndSwapChain(out _GraphicsDevice, out _SwapChain);

            InitRenderTarget();
            InitDepthStencil();
            GraphicsDevice.ImmediateContext.OutputMerger.SetTargets(DepthStencil, RenderTarget);

            AnimationClock = new MatTimer(1000.0 / 60.0);
            AnimationClock.MatTickEvent += Clock_MatTickEvent;
            AnimationClock.Start();
        }

        private void Host_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        private void Host_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AnimationClock.Dispose();
        }

        private void CreateDeviceAndSwapChain(out SlimDX.Direct3D11.Device device, out SwapChain swapChain)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(Host);

            SlimDX.Direct3D11.Device.CreateWithSwapChain(
                DriverType.Hardware,
                DeviceCreationFlags.None,
                new SwapChainDescription
                {
                    BufferCount = 2,
                    OutputHandle = source.Handle,
                    IsWindowed = true,
                    SampleDescription = new SampleDescription
                    {
                        Count = 8,
                        Quality = 0
                    },
                    ModeDescription = new ModeDescription
                    {
                        Width = (int)(ViewArea.ActualWidth),
                        Height = (int)(ViewArea.ActualHeight),
                        RefreshRate = new Rational(60, 1),
                        Format = Format.R8G8B8A8_UNorm
                    },
                    Usage = Usage.RenderTargetOutput
                },
                out device,
                out swapChain);
        }

        private void InitRenderTarget()
        {
            using (Texture2D backBuffer = SlimDX.Direct3D11.Resource.FromSwapChain<Texture2D>(SwapChain, 0))
            {
                RenderTarget = new RenderTargetView(GraphicsDevice, backBuffer);
            }
        }

        private void InitDepthStencil()
        {
            Texture2DDescription depthBufferDesc = new Texture2DDescription
            {
                ArraySize = 1,
                BindFlags = BindFlags.DepthStencil,
                Format = Format.D32_Float,
                Width = (int)(ViewArea.ActualWidth),
                Height = (int)(ViewArea.ActualHeight),
                MipLevels = 1,
                SampleDescription = new SampleDescription(8, 0)
            };

            using (Texture2D depthBuffer = new Texture2D(GraphicsDevice, depthBufferDesc))
            {
                DepthStencil = new DepthStencilView(GraphicsDevice, depthBuffer);
            }
        }

        private void Clock_MatTickEvent(object sender)
        {
            GraphicsDevice.ImmediateContext.ClearRenderTargetView(RenderTarget, new Color4(1.0f, 0.1f, 0.1f, 0.1f));
            GraphicsDevice.ImmediateContext.ClearDepthStencilView(DepthStencil, DepthStencilClearFlags.Depth, 1.0f, 0);

            if(CurrentWorld != null)
            {
                CurrentWorld.OnRender(ViewArea.ActualWidth, ViewArea.ActualHeight);
            }

            SwapChain.Present(0, PresentFlags.None);
        }

        public void Dispose()
        {
            GraphicsDevice.Dispose();
            SwapChain.Dispose();
            RenderTarget.Dispose();
            DepthStencil.Dispose();
        }
    }
}
