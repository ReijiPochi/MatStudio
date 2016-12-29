using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.D3DCompiler;
using SlimDX;

using MatFramework.Graphics._3D.Effects;

namespace MatFramework.Graphics._3D.Objects
{
    public class Mat3DObject : MatObject, IDisposable
    {
        public Mat3DObject()
        {
            Visible = true;
            OnLoading();
        }

        public bool Visible { get; set; }

        public double PositionX { get; set; }
        public double PositionY { get; set; }
        public double PositionZ { get; set; }
        public Color4 Color { get; set; }

        public PrimitiveTopology _DrawMode = PrimitiveTopology.TriangleList;
        public PrimitiveTopology DrawMode
        {
            get { return _DrawMode; }
        }

        public MatEffectPack _EffectPack;
        public MatEffectPack EffectPack
        {
            get { return _EffectPack; }
        }

        public MatVertexDataPNT[] _Vertices;
        public MatVertexDataPNT[] Vertices
        {
            get { return _Vertices; }
        }

        public int VertexCount { get; protected set; }

        public SlimDX.Direct3D11.Buffer _VertexBuffer;
        public SlimDX.Direct3D11.Buffer VertexBuffer
        {
            get { return _VertexBuffer; }
        }

        public int[] _Indices;
        public int[] Indices
        {
            get { return _Indices; }
        }

        public SlimDX.Direct3D11.Buffer _IndexBuffer;
        public SlimDX.Direct3D11.Buffer IndexBuffer
        {
            get { return _IndexBuffer; }
        }

        public ShaderResourceView _Texture;
        public ShaderResourceView Texture
        {
            get { return _Texture; }
        }

        public bool SetEffectPacks(MatEffectPack effectPack)
        {
            if (EffectPack != null) EffectPack.Dispose();
            _EffectPack = effectPack;
            return true;
        }

        protected virtual void OnLoading()
        {
            SetEffectPacks(new MatDefaultFx(this));
        }

        public virtual void Draw(MatWorld world)
        {
            if (!Visible) return;

            Mat3DView.GraphicsDevice.ImmediateContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, MatVertexDataPNT.SizeInBytes, 0));
            Mat3DView.GraphicsDevice.ImmediateContext.InputAssembler.PrimitiveTopology = DrawMode;

            if (EffectPack != null) EffectPack.PrepareToDraw(world.ActiveCamera.CameraMatrix);

            if (Indices == null)
            {
                Mat3DView.GraphicsDevice.ImmediateContext.Draw(VertexCount, 0);
            }
            else
            {
                Mat3DView.GraphicsDevice.ImmediateContext.InputAssembler.SetIndexBuffer(IndexBuffer, Format.R32_UInt, 0);
                Mat3DView.GraphicsDevice.ImmediateContext.DrawIndexed(Indices.Length, 0, 0);
            }
        }

        protected bool InitVertexBuffer(MatVertexDataPNT[] vertices)
        {
            if (Vertices != vertices) _Vertices = vertices;

            using (DataStream vertexStream = new DataStream(vertices, true, true))
            {
                _VertexBuffer = new SlimDX.Direct3D11.Buffer(
                    Mat3DView.GraphicsDevice,
                    vertexStream,
                    new BufferDescription { SizeInBytes = (int)vertexStream.Length, BindFlags = BindFlags.VertexBuffer }
                    );
            }
            
            if (VertexBuffer == null) return false;
            else return true;
        }

        protected void InitIndexBuffer(int[] indices)
        {
            if (Indices != indices) _Indices = indices;

            using(DataStream indexStream = new DataStream(indices, true, true))
            {
                _IndexBuffer = new SlimDX.Direct3D11.Buffer(
                    Mat3DView.GraphicsDevice,
                    indexStream,
                    new BufferDescription { SizeInBytes = (int)indexStream.Length, BindFlags = BindFlags.IndexBuffer }
                    );
            }
        }

        public void Dispose()
        {
            if (EffectPack != null) EffectPack.Dispose();
            if (VertexBuffer != null) VertexBuffer.Dispose();
            if (IndexBuffer != null) IndexBuffer.Dispose();
            if (Texture != null) Texture.Dispose();
        }
    }
}
