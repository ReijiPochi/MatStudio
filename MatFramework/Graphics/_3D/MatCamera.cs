using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using MatFramework.Graphics._3D.Objects;

namespace MatFramework.Graphics._3D
{
    public class MatCamera
    {
        public RenderTargetView RenderTarget { get; set; }

        protected double _ViewPortWidth;
        public double ViewPortWidth
        {
            get { return _ViewPortWidth; }
            set { _ViewPortWidth = value; ValueChanged = true; }
        }

        protected double _ViewPortHeight;
        public double ViewPortHeight
        {
            get { return _ViewPortHeight; }
            set { _ViewPortHeight = value; ValueChanged = true; }
        }

        public bool ValueChanged { get; protected set; }

        protected Vector3 _Eye;
        public Vector3 Eye
        {
            get { return _Eye; }
            set { _Eye = value; ValueChanged = true; }
        }

        protected Vector3 _Target;
        public Vector3 Target
        {
            get { return _Target; }
            set { _Target = value; ValueChanged = true; }
        }

        protected Vector3 _Up;
        public Vector3 Up
        {
            get { return _Up; }
            set { _Up = value; ValueChanged = true; }
        }

        protected double _FieldOfView;
        public double FieldOfView
        {
            get { return _FieldOfView; }
            set { _FieldOfView = value; ValueChanged = true; }
        }

        public Matrix CameraMatrix { get; protected set; }

        protected virtual void CameraUpdate()
        {
            if (!ValueChanged || Mat3DView.GraphicsDevice == null) return;

            Mat3DView.GraphicsDevice.ImmediateContext.Rasterizer.SetViewports(new Viewport
            {
                Width = (int)ViewPortWidth,
                Height = (int)ViewPortHeight,
                MaxZ = 1.0f
            });

            Matrix view = Matrix.LookAtRH(_Eye, _Target, _Up);
            Matrix projection = Matrix.PerspectiveFovRH((float)(System.Math.PI * (_FieldOfView / 180.0)), (float)(ViewPortWidth / ViewPortHeight), 0.1f, 1000.0f);
            CameraMatrix = view * projection;

            ValueChanged = false;
        }

        public virtual void Render(MatWorld world)
        {
            CameraUpdate();

            RenderingContext context = new RenderingContext()
            {
                camMatrix = CameraMatrix
            };

            foreach (Mat3DObject obj in world.Objects)
            {
                obj.Draw(context);
            }
        }

    }
}
