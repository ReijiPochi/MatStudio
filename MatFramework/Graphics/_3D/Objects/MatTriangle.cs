using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX;

namespace MatFramework.Graphics._3D.Objects
{
    public class MatTriangle : Mat3DObject
    {
        public MatTriangle()
        {
            InitVertexBuffer(new[] {
                new MatVertexDataPNT() { position = new Vector3(0, 0.5f, 0), normal = new Vector3(0, 0, 1) },
                new MatVertexDataPNT() { position = new Vector3(0, 0, 0), normal = new Vector3(0, 0, 1) },
                new MatVertexDataPNT() { position = new Vector3(-0.5f, 0, 0), normal = new Vector3(0, 0, 1) }
                });

            VertexCount = 3;
        }
    }
}
