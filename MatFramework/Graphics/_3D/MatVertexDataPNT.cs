using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX;

namespace MatFramework.Graphics._3D
{
    public struct MatVertexDataPNT
    {
        public Vector3 position;
        public Vector3 normal;
        public Vector2 texCoord;

        public static int SizeInBytes
        {
            get { return System.Runtime.InteropServices.Marshal.SizeOf(typeof(MatVertexDataPNT)); }
        }
    }
}
