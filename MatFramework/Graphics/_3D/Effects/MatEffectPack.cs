using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using SlimDX.Direct3D11;
using SlimDX.DXGI;
using SlimDX.D3DCompiler;
using SlimDX;

using MatFramework.Graphics._3D.Objects;

namespace MatFramework.Graphics._3D.Effects
{
    public abstract class MatEffectPack : IDisposable
    {
        public MatEffectPack(Mat3DObject target)
        {
            Target = target;
            OnLoadShaders(null);
        }

        public Mat3DObject Target { get; private set; }

        public Effect _Effect;
        public Effect Effect
        {
            get { return _Effect; }
        }

        public InputLayout _VertexLayout;
        public InputLayout VertexLayout
        {
            get { return _VertexLayout; }
        }

        protected virtual void OnLoadShaders(string shaderFileNeme)
        {
            _Effect = Compile(LoadShaderString(shaderFileNeme));
            InitVertexLayout();
        }

        public virtual void PrepareToDraw(RenderingContext context)
        {
        }

        protected string LoadShaderString(string name)
        {
            string shader;
            Assembly a = Assembly.GetExecutingAssembly();
            string[] resources = a.GetManifestResourceNames();
            using (StreamReader sr = new StreamReader(a.GetManifestResourceStream("MatFramework.Graphics._3D.Effects." + name)))
            {
                shader = sr.ReadToEnd();
            }

            return shader;
        }

        protected Effect Compile(string shader)
        {
            using (ShaderBytecode shaderBytecode = ShaderBytecode.Compile(shader, "fx_5_0", ShaderFlags.None, EffectFlags.None))
            {
                return new Effect(Mat3DView.GraphicsDevice, shaderBytecode);
            }
        }

        protected virtual void InitVertexLayout()
        {
            if (Effect == null) return;

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

        public virtual void Dispose()
        {
            if (Effect != null) Effect.Dispose();
            if (VertexLayout != null) VertexLayout.Dispose();
        }
    }
}
