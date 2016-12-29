using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.Graphics._3D.Objects;
using SlimDX;

namespace NITNIC
{
    public class MapJudgment
    {
        public static MatSolid3DObject Map { get; set; }

        public static MapJudgmentResult Judge(PositionXYZ position)
        {
            int pointCount = Map.Indices.Length;
            Line l1 = new Line(), l2 = new Line();
            Vector3 centor;
            Vector3 point = new Vector3((float)position.X, (float)position.Y, (float)position.Z);

            MapJudgmentResult result = new MapJudgmentResult() { mapOK = false, floorHeight = 0.0};

            for(int i = 0; i < pointCount; i += 3)
            {
                int i1 = Map.Indices[i], i2 = Map.Indices[i + 1], i3 = Map.Indices[i + 2];
                bool hit = true;

                if (Map.Vertices[i1].position.Y > point.Y - 0.5) continue;

                double cX = (Map.Vertices[i1].position.X + Map.Vertices[i2].position.X + Map.Vertices[i3].position.X) / 3.0;
                double cZ = (Map.Vertices[i1].position.Z + Map.Vertices[i2].position.Z + Map.Vertices[i3].position.Z) / 3.0;

                centor = new Vector3((float)cX, 0.0f, (float)cZ);

                l1.a = Map.Vertices[i1].position;
                l1.b = Map.Vertices[i2].position;
                l2.a = point;
                l2.b = centor;
                if (HitTestLine(l1, l2))
                    hit = false;

                l1.a = Map.Vertices[i1].position;
                l1.b = Map.Vertices[i3].position;
                l2.a = point;
                l2.b = centor;
                if (HitTestLine(l1, l2))
                    hit = false;

                l1.a = Map.Vertices[i2].position;
                l1.b = Map.Vertices[i3].position;
                l2.a = point;
                l2.b = centor;
                if (HitTestLine(l1, l2))
                    hit = false;

                if (hit)
                {
                    result.mapOK = true;

                    if (Map.Vertices[i1].position.Y > result.floorHeight)
                        result.floorHeight = Map.Vertices[i1].position.Y;
                }
            }

            return result;
        }

        private static bool HitTestLine(Line line1, Line line2)
        {
            double a = (line2.aX - line2.bX) * (line1.aZ - line2.aZ) + (line2.aZ - line2.bZ) * (line2.aX - line1.aX);
            double b = (line2.aX - line2.bX) * (line1.bZ - line2.aZ) + (line2.aZ - line2.bZ) * (line2.aX - line1.bX);
            double c = (line1.aX - line1.bX) * (line2.aZ - line1.aZ) + (line1.aZ - line1.bZ) * (line1.aX - line2.aX);
            double d = (line1.aX - line1.bX) * (line2.bZ - line1.aZ) + (line1.aZ - line1.bZ) * (line1.aX - line2.bX);

            return c * d < 0.0 && a * b < 0.0;
        }
    }

    internal class Line
    {
        public Vector3 a;
        public Vector3 b;

        public double aX { get { return a.X; } }
        public double aZ { get { return a.Z; } }
        public double bX { get { return b.X; } }
        public double bZ { get { return b.Z; } }
    }
}
