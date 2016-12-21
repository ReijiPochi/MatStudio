using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using CompositionTarget = System.Windows.Media.CompositionTarget;

using System.Drawing;


//using SlimDX.Direct3D9;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.D3DCompiler;
using System.Windows.Threading;
using System.Net.Sockets;

namespace Hiratoki2016
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SlimDX.Direct3D11.Device graphicsDevice;
        public SwapChain swapChain;
        public RenderTargetView renderTarget;

        Effect effect;
        InputLayout vertexLayout;
        SlimDX.Direct3D11.Buffer vertexBuffer;

        double moveX = 0.0;
        float posX = 0.0f;

        double moveY = 0.0;
        float posY = 0.0f;

        DispatcherTimer animationClock = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 10) };

        UdpClient client;
        bool clientConnected = false;

        Window consoleWindow = new Window() { Width = 300.0, Height = 100.0 };
        TextBlock consoleTb = new TextBlock();

        /// <summary>
        /// 最上位ウインドウを作成
        /// </summary>
        public MainWindow()
        {
            // コンポーネント初期化
            InitializeComponent();

            #region Direct3D9
            /*
            // デバイスを作成
            Device device = new Device(
                new Direct3D(),
                0,
                DeviceType.Hardware,
                IntPtr.Zero,
                CreateFlags.HardwareVertexProcessing,
                new PresentParameters());

            // 光源を設定して有効化
            device.SetLight(0, new Light()
            {
                Type = LightType.Directional,
                Diffuse = Color.White,
                Ambient = Color.GhostWhite,
                Direction = new Vector3(0, -1, 1)
            });
            device.EnableLight(0, true);

            //射影変換を設定
            device.SetTransform(TransformState.Projection,
                Matrix.PerspectiveFovLH((float)(Math.PI / 4),
                (float)(this.Width / this.Height),
                0.1f, 20.0f));

            //ビューを設定
            device.SetTransform(TransformState.View,
                Matrix.LookAtLH(new Vector3(3, 2, -3),
                Vector3.Zero,
                new Vector3(0, 1, 0)));

            //マテリアル設定
            device.Material = new Material()
            {
                Diffuse = new Color4(Color.GhostWhite)
            };


            // 全面灰色でクリア
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, new Color4(0.2f, 0.2f, 0.2f), 1.0f, 0);

            // シーン開始
            device.BeginScene();

            //ティーポットを描画
            device.SetTransform(TransformState.World,
                Matrix.Translation(-3, 0, 5));
            Mesh.CreateTeapot(device).DrawSubset(0);

            //文字列を描画
            device.SetTransform(TransformState.World,
                Matrix.Translation(-4, -1, 0));
            Mesh.CreateText(device, new System.Drawing.Font("Arial", 10), "Hello, world!", 0.001f, 0.001f).DrawSubset(0);

            // シーン終了
            device.EndScene();


            // 画像をロック
            this.directXImage.Lock();

            // バックバッファー領域を指定
            this.directXImage.SetBackBuffer(D3DResourceType.IDirect3DSurface9, device.GetBackBuffer(0, 0).ComPointer);
            this.directXImage.AddDirtyRect(new Int32Rect(0, 0, directXImage.PixelWidth, directXImage.PixelHeight));

            // ロック解除
            this.directXImage.Unlock();

            */
            #endregion

            Loaded += MainWindow_Loaded;
            SizeChanged += MainWindow_SizeChanged;
            MouseDown += MainWindow_MouseDown;
            KeyDown += MainWindow_KeyDown;
            KeyUp += MainWindow_KeyUp;

            animationClock.Tick += AnimationClock_Tick;
            animationClock.Start();

            Application.Current.Exit += Current_Exit;
        }

        private void Current_Exit(object sender, ExitEventArgs e)
        {
            clientConnected = false;
            disposeDevice();
            client.Close();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.D)
                moveX = 0.0;
            if (e.Key == Key.W || e.Key == Key.S)
                moveY = 0.0;
        }

        private async void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Q)
            {
                if (client == null) return;
                var returnMessage = Encoding.UTF8.GetBytes("Goodbye!");
                await client.SendAsync(returnMessage, returnMessage.Length);
            }

            if (e.Key == Key.D)
                moveX = 0.01;
            else if (e.Key == Key.A)
                moveX = -0.01;

            if (e.Key == Key.W)
                moveY = 0.01;
            else if (e.Key == Key.S)
                moveY = -0.01;
        }

        private void AnimationClock_Tick(object sender, EventArgs e)
        {
            if (moveX != 0)
                posX += (float)moveX;
            if (moveY != 0)
                posY += (float)moveY;

            if (moveX != 0 || moveY != 0)
            {
                initVertexBuffer();
                UploadData();
            }

            Draw();
        }

        private async void UploadData()
        {
            if (client == null) return;

            string x = posX >= 0.0 ? posX.ToString("f3") : posX.ToString("f2");
            string y = posY >= 0.0 ? posY.ToString("f3") : posY.ToString("f2");

            var returnMessage = Encoding.UTF8.GetBytes("X" + x + "Y" + y);
            await client.SendAsync(returnMessage, returnMessage.Length);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (graphicsDevice != null)
                initViewport();
        }

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            client = new UdpClient(AddressFamily.InterNetwork);
            client.Connect("reiji-matlab.mydns.jp", 810);
            clientConnected = true;

            ListenMessage();

            initDevice();

            consoleWindow.Content = consoleTb;
            consoleWindow.Show();
        }

        public async void ListenMessage()
        {
            while (clientConnected)
            {
                // データ受信待機
                var result = await client.ReceiveAsync();

                // 受信したデータを変換
                var data = Encoding.UTF8.GetString(result.Buffer);

                consoleWindow.Content = data;

                switch (data)
                {
                    case "SeeYou!":
                        Close();
                        Application.Current.Shutdown();
                        break;

                    default:
                        break;
                }
            }
        }

        private void initDevice()
        {
            MyDirectXHelper.CreateDeviceAndSwapChain(DrawingArea, out graphicsDevice, out swapChain);

            initRenderTarget();
            initViewport();

            LoadContent();
        }

        private void initRenderTarget()
        {
            using (Texture2D backBuffer = SlimDX.Direct3D11.Resource.FromSwapChain<Texture2D>(swapChain, 0))
            {
                renderTarget = new RenderTargetView(graphicsDevice, backBuffer);
                graphicsDevice.ImmediateContext.OutputMerger.SetTargets(renderTarget);
            }
        }

        private void initViewport()
        {
            graphicsDevice.ImmediateContext.Rasterizer.SetViewports(
                new Viewport
                {
                    Width = (int)(DrawingArea.ActualWidth),
                    Height = (int)(DrawingArea.ActualHeight),
                }
                );
        }

        private void disposeDevice()
        {
            UnloadContent();
            graphicsDevice.Dispose();
            swapChain.Dispose();
            renderTarget.Dispose();
        }

        public void Draw()
        {
            graphicsDevice.ImmediateContext.ClearRenderTargetView(renderTarget, new SlimDX.Color4(1, 0, 0, 1));

            initTriangleInputAssembler();
            drawTriangle();

            swapChain.Present(0, PresentFlags.None);
        }

        private void drawTriangle()
        {
            effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(graphicsDevice.ImmediateContext);
            graphicsDevice.ImmediateContext.Draw(3, 0);
        }

        private void initTriangleInputAssembler()
        {
            graphicsDevice.ImmediateContext.InputAssembler.InputLayout = vertexLayout;
            graphicsDevice.ImmediateContext.InputAssembler.SetVertexBuffers(
                0,
                new VertexBufferBinding(vertexBuffer, sizeof(float) * 3, 0)
                );
            graphicsDevice.ImmediateContext.InputAssembler.PrimitiveTopology
                = PrimitiveTopology.TriangleList;
        }



        private void LoadContent()
        {
            initEffect();
            initVertexLayout();
            initVertexBuffer();
        }

        private void initEffect()
        {
            using (ShaderBytecode shaderBytecode = ShaderBytecode.CompileFromFile(
                "myEffect.fx", "fx_5_0",
                ShaderFlags.None,
                EffectFlags.None
                ))
            {
                effect = new Effect(graphicsDevice, shaderBytecode);
            }
        }

        private void initVertexLayout()
        {
            vertexLayout = new InputLayout(
                graphicsDevice,
                effect.GetTechniqueByIndex(0).GetPassByIndex(0).Description.Signature,
                new[] {
                    new InputElement
                    {
                        SemanticName = "SV_Position",
                        Format = Format.R32G32B32_Float
                    }
                    }
                );
        }

        private void initVertexBuffer()
        {
            vertexBuffer = MyDirectXHelper.CreateVertexBuffer(
                graphicsDevice,
                new[] {
                new SlimDX.Vector3(posX, posY + 0.5f, 0),
                new SlimDX.Vector3(posX, posY, 0),
                new SlimDX.Vector3(posX-0.5f, posY, 0),
                });
        }

        private void UnloadContent()
        {
            effect.Dispose();
            vertexLayout.Dispose();
            vertexBuffer.Dispose();
        }

    }

    class MyDirectXHelper
    {
        public static void CreateDeviceAndSwapChain(FrameworkElement w, out SlimDX.Direct3D11.Device device, out SlimDX.DXGI.SwapChain swapChain)
        {
            HwndSource source = (HwndSource)HwndSource.FromVisual(w);

            SlimDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, new SwapChainDescription
            {
                BufferCount = 1,
                OutputHandle = source.Handle,
                IsWindowed = true,
                SampleDescription = new SampleDescription
                {
                    Count = 1,
                    Quality = 0
                },

                ModeDescription = new ModeDescription
                {
                    Width = (int)(w.ActualWidth),
                    Height = (int)(w.ActualHeight),
                    RefreshRate = new SlimDX.Rational(60, 1),
                    Format = Format.R8G8B8A8_UNorm
                },

                Usage = Usage.RenderTargetOutput
            },
            out device,
            out swapChain);
        }

        public static SlimDX.Direct3D11.Buffer CreateVertexBuffer(
        SlimDX.Direct3D11.Device graphicsDevice,
        System.Array vertices
        )
        {
            using (SlimDX.DataStream vertexStream
                = new SlimDX.DataStream(vertices, true, true))
            {
                return new SlimDX.Direct3D11.Buffer(
                    graphicsDevice,
                    vertexStream,
                    new BufferDescription
                    {
                        SizeInBytes = (int)vertexStream.Length,
                        BindFlags = BindFlags.VertexBuffer,
                    }
                    );
            }
        }
    }
}
