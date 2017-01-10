using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX.Direct3D11;
using MatFramework.Graphics._3D.Objects;
using SlimDX;
using SlimDX.DXGI;

namespace MatFramework.Graphics._3D.Effects
{
    public class MatDefaultFx : MatEffectPack
    {
        public MatDefaultFx(Mat3DObject target) : base(target)
        {
            
        }

        public EffectMatrixVariable _CameraMatrix;
        public Matrix CameraMatrix
        {
            get { return _CameraMatrix.GetMatrix(); }
            set { _CameraMatrix.SetMatrix(value); }
        }

        public EffectMatrixVariable world;
        public Matrix World
        {
            get { return world.GetMatrix(); }
            set { world.SetMatrix(value); }
        }

        public EffectVectorVariable color;
        public Color4 Color
        {
            get { return color.GetColor(); }
            set { color.Set(value); }
        }

        private bool textureSent = false;

        public EffectResourceVariable _DiffuseTexture;
        public ShaderResourceView DiffuseTexture
        {
            get { return _DiffuseTexture.GetResource(); }
            set
            {
                _DiffuseTexture.SetResource(value);
                textureSent = true;
            }
        }

        protected override void OnLoadShaders(string shaderFileNeme)
        {
            base.OnLoadShaders("default.fx");

            _CameraMatrix = Effect.GetVariableByName("ViewProjection").AsMatrix();
            world = Effect.GetVariableByName("World").AsMatrix();
            color = Effect.GetVariableByName("Color").AsVector();
            _DiffuseTexture = Effect.GetVariableByName("DiffuseTexture").AsResource();
        }

        public override void PrepareToDraw(RenderingContext context)
        {
            if (!textureSent && Target.Texture != null)
                DiffuseTexture = Target.Texture;

            CameraMatrix = context.camMatrix;
            World = Matrix.Translation((float)Target.PositionX, (float)Target.PositionY, (float)Target.PositionZ);
            Color = Target.Color;

            Effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(Mat3DView.GraphicsDevice.ImmediateContext);
            Mat3DView.GraphicsDevice.ImmediateContext.InputAssembler.InputLayout = VertexLayout;
        }
    }
}
