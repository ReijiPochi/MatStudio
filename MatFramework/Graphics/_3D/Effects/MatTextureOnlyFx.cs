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
    public class MatTextureOnlyFx : MatEffectPack
    {
        public MatTextureOnlyFx(Mat3DObject target) : base(target)
        {
            
        }

        public EffectMatrixVariable _CameraMatrix;
        public Matrix CameraMatrix
        {
            get { return _CameraMatrix.GetMatrix(); }
            set { _CameraMatrix.SetMatrix(value); }
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
            base.OnLoadShaders("textureOnly.fx");

            _CameraMatrix = Effect.GetVariableByName("ViewProjection").AsMatrix();
            _DiffuseTexture = Effect.GetVariableByName("DiffuseTexture").AsResource();
        }

        public override void PrepareToDraw(Matrix cam)
        {
            if (!textureSent && Target.Texture != null)
                DiffuseTexture = Target.Texture;

            CameraMatrix = cam;

            Effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(Mat3DView.GraphicsDevice.ImmediateContext);
            Mat3DView.GraphicsDevice.ImmediateContext.InputAssembler.InputLayout = VertexLayout;
        }

        protected override void InitVertexLayout()
        {
            _VertexLayout = new InputLayout(
                Mat3DView.GraphicsDevice,
                Effect.GetTechniqueByIndex(0).GetPassByIndex(0).Description.Signature,
                new[] {
                    new InputElement { SemanticName = "SV_Position", Format = Format.R32G32B32_Float },
                    new InputElement { SemanticName = "NORMAL", Format = Format.R32G32B32_Float, AlignedByteOffset = InputElement.AppendAligned },
                    new InputElement { SemanticName = "TEXCOORD", Format = Format.R32G32_Float, AlignedByteOffset = InputElement.AppendAligned }
                }
                );
        }

        public override void Dispose()
        {
            base.Dispose();

            if (DiffuseTexture != null) DiffuseTexture.Dispose();
        }
    }
}
