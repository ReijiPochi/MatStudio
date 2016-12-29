using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX;
using SlimDX.Direct3D11;
using MatFramework.Graphics._3D.Effects;

namespace MatFramework.Graphics._3D.Objects
{
    public class MatMeshFloor : Mat3DObject
    {
        public MatMeshFloor()
        {
            _DrawMode = PrimitiveTopology.LineList;
            Color = new Color4(0.7f, 0.7f, 0.7f);

            MatVertexDataPNT[] data = new MatVertexDataPNT[404];
            int i = 0;

            for(int x = -50; x <= 50; x++)
            {
                data[i] = new MatVertexDataPNT() { position = new Vector3(x, 0, -50) };
                i++;
                data[i] = new MatVertexDataPNT() { position = new Vector3(x, 0, 50) };
                i++;
                data[i] = new MatVertexDataPNT() { position = new Vector3(-50, 0, x) };
                i++;
                data[i] = new MatVertexDataPNT() { position = new Vector3(50, 0, x) };
                i++;
            }

            InitVertexBuffer(data);
            VertexCount = 404;
        }

        protected override void OnLoading()
        {
            _EffectPack = new MatSingleColorFx(this);
        }
    }
}
