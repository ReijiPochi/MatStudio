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
    public class MatSingleColorFx : MatEffectPack
    {
        public MatSingleColorFx(Mat3DObject target) : base(target)
        {
        }

        public EffectMatrixVariable _CameraMatrix;
        public Matrix CameraMatrix
        {
            get { return _CameraMatrix.GetMatrix(); }
            set { _CameraMatrix.SetMatrix(value); }
        }

        public EffectVectorVariable color;
        public Color4 Color
        {
            get { return color.GetColor(); }
            set { color.Set(value); }
        }

        protected override void OnLoadShaders(string shaderFileNeme)
        {
            base.OnLoadShaders("singleColor.fx");

            _CameraMatrix = Effect.GetVariableByName("ViewProjection").AsMatrix();
            color = Effect.GetVariableByName("Color").AsVector();
        }

        public override void PrepareToDraw(RenderingContext context)
        {
            CameraMatrix = context.camMatrix;
            Color = Target.Color;

            Effect.GetTechniqueByIndex(0).GetPassByIndex(0).Apply(Mat3DView.GraphicsDevice.ImmediateContext);
            Mat3DView.GraphicsDevice.ImmediateContext.InputAssembler.InputLayout = VertexLayout;
        }
    }
}
