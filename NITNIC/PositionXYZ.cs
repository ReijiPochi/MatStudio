using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NITNIC
{
    public class PositionXYZ
    {
        public PositionXYZ()
        {

        }

        public PositionXYZ(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public double X;
        public double Y;
        public double Z;

        public static SlimDX.Vector3 ConvertToSlimDXVector3(PositionXYZ position)
        {
            return new SlimDX.Vector3((float)position.X, (float)position.Y, (float)position.Z);
        }
    }
}
