using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX;
using SlimDX.Direct3D11;

namespace MatFramework.Graphics._3D
{
    public class MatCamera3D : MatCamera
    {
        private bool renderLeft;
        private Vector3 leftEye, rightEye, leftTarget, rightTarget;

        protected override void CameraUpdate()
        {
            //base.CameraUpdate();

            if (Mat3DView.GraphicsDevice == null) return;

            if (ValueChanged)
            {
                
            }

            if (renderLeft)
            {
                Mat3DView.GraphicsDevice.ImmediateContext.Rasterizer.SetViewports(new Viewport
                {
                    Width = (int)(ViewPortWidth / 2.0),
                    Height = (int)ViewPortHeight,
                    MaxZ = 1.0f,
                    X = 0
                });
            }
            else
            {
                Mat3DView.GraphicsDevice.ImmediateContext.Rasterizer.SetViewports(new Viewport
                {
                    Width = (int)(ViewPortWidth / 2.0),
                    Height = (int)ViewPortHeight,
                    MaxZ = 1.0f,
                    X = (int)(ViewPortWidth / 2.0)
                });
            }

            Matrix view = Matrix.LookAtRH(_Eye, _Target, _Up);
            Matrix projection = Matrix.PerspectiveFovRH((float)(System.Math.PI * (_FieldOfView / 180.0)), (float)(ViewPortWidth / ViewPortHeight), 0.1f, 1000.0f);
            CameraMatrix = view * projection;

            ValueChanged = false;
        }

        public override void Render(MatWorld world)
        {
            renderLeft = true;
            base.Render(world);

            renderLeft = false;
            base.Render(world);
        }
    }
}
