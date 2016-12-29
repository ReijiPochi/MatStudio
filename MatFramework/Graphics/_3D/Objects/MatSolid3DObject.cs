using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX.Direct3D11;

namespace MatFramework.Graphics._3D.Objects
{
    public class MatSolid3DObject : Mat3DObject
    {
        public MatSolid3DObject(string objPath, string texPath)
        {
            InitVertexBuffer(ObjLoader.FromFile(objPath, out _Indices));
            InitIndexBuffer(_Indices);
            VertexCount = Vertices.Length;
            _Texture = ShaderResourceView.FromFile(Mat3DView.GraphicsDevice, texPath);
        }
    }
}
