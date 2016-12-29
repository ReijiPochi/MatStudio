using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SlimDX;

namespace MatFramework.Graphics._3D.Objects
{
    internal class ObjLoader
    {
        public static MatVertexDataPNT[] FromFile(string path, out int[] indexBuffer)
        {
            List<Vector3> positions = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector2> texCoords = new List<Vector2>();
            List<Indices> indices = new List<Indices>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(' ');

                    switch (line[0])
                    {
                        case "v":
                            positions.Add(ToVector3(line[1], line[2], line[3]));
                            break;

                        case "vn":
                            normals.Add(ToVector3(line[1], line[2], line[3]));
                            break;

                        case "vt":
                            texCoords.Add(ToVector2(line[1], line[2]));
                            break;

                        case "f":
                            indices.Add(new Indices(line[1]));
                            indices.Add(new Indices(line[2]));
                            indices.Add(new Indices(line[3]));
                            break;

                        default:
                            break;
                    }
                }
            }

            //IndexType biggest = IndexType.TexCoord;

            //if (positions.Count > normals.Count)
            //{
            //    if (normals.Count > texCoords.Count) biggest = IndexType.Position;
            //}
            //else if (normals.Count > texCoords.Count) biggest = IndexType.Normal;

            MatVertexDataPNT[] result = null;
            indexBuffer = new int[indices.Count];

            result = Build(indices, positions, normals, texCoords, ref indexBuffer);

            //switch (biggest)
            //{
            //    case IndexType.TexCoord:
            //        result = BuildFromTexCoord(indices, positions, normals, texCoords, ref indexBuffer);
            //        break;

            //    case IndexType.Position:
            //        result = BuildFromPosition(indices, positions, normals, texCoords, ref indexBuffer);
            //        break;

            //    case IndexType.Normal:
            //        result = Build(indices, positions, normals, texCoords, ref indexBuffer);
            //        break;

            //    default:
            //        break;
            //}

            return result;
        }

        //private static MatVertexDataPNT[] BuildFromTexCoord(List<Indices> indices, List<Vector3> positions, List<Vector3> normals, List<Vector2> texCoords, ref int[] indexBuffer)
        //{
        //    MatVertexDataPNT[] result = new MatVertexDataPNT[texCoords.Count];
        //    int count = 0;

        //    foreach(Indices index in indices)
        //    {
        //        result[index.texCoord] = new MatVertexDataPNT()
        //        {
        //            position = new Vector3(positions[index.position].X, positions[index.position].Y, positions[index.position].Z),
        //            normal = new Vector3(normals[index.normal].X, normals[index.normal].Y, normals[index.normal].Z),
        //            texCoord = new Vector2(texCoords[index.texCoord].X, 1.0f - texCoords[index.texCoord].Y)
        //        };

        //        indexBuffer[count] = index.texCoord;
        //        count++;
        //    }

        //    return result;
        //}

        //private static MatVertexDataPNT[] BuildFromPosition(List<Indices> indices, List<Vector3> positions, List<Vector3> normals, List<Vector2> texCoords, ref int[] indexBuffer)
        //{
        //    MatVertexDataPNT[] result = new MatVertexDataPNT[positions.Count];
        //    int count = 0;

        //    foreach (Indices index in indices)
        //    {
        //        result[index.position] = new MatVertexDataPNT()
        //        {
        //            position = new Vector3(positions[index.position].X, positions[index.position].Y, positions[index.position].Z),
        //            normal = new Vector3(normals[index.normal].X, normals[index.normal].Y, normals[index.normal].Z),
        //            texCoord = new Vector2(texCoords[index.texCoord].X, 1.0f - texCoords[index.texCoord].Y)
        //        };

        //        indexBuffer[count] = index.position;
        //        count++;
        //    }

        //    return result;
        //}

        private static MatVertexDataPNT[] Build(List<Indices> indices, List<Vector3> positions, List<Vector3> normals, List<Vector2> texCoords, ref int[] indexBuffer)
        {
            MatVertexDataPNT[] result = new MatVertexDataPNT[indices.Count];
            int count = 0;

            foreach (Indices index in indices)
            {
                result[count] = new MatVertexDataPNT()
                {
                    position = new Vector3(positions[index.position].X, positions[index.position].Y, positions[index.position].Z),
                    normal = new Vector3(normals[index.normal].X, normals[index.normal].Y, normals[index.normal].Z),
                    texCoord = new Vector2(texCoords[index.texCoord].X, 1.0f - texCoords[index.texCoord].Y)
                };

                indexBuffer[count] = count;
                count++;
            }

            return result;
        }

        private static Vector2 ToVector2(string x, string y)
        {
            float X, Y;

            if (float.TryParse(x, out X) && float.TryParse(y, out Y))
                return new Vector2(X, Y);
            else
                return new Vector2(0.0f);
        }

        private static Vector3 ToVector3(string x, string y, string z)
        {
            float X, Y, Z;

            if (float.TryParse(x, out X) && float.TryParse(y, out Y) && float.TryParse(z, out Z))
                return new Vector3(X, Y, Z);
            else
                return new Vector3(0.0f);
        }

        private enum IndexType
        {
            Position,
            Normal,
            TexCoord
        }

        private class Indices
        {
            public Indices(string indices)
            {
                string[] index = indices.Split('/');
                
                if(int.TryParse(index[0],out position)&&int.TryParse(index[1],out texCoord)&&int.TryParse(index[2],out normal))
                {
                    position--;
                    normal--;
                    texCoord--;
                }
                else
                {
                    position = 0;
                    normal = 0;
                    texCoord = 0;
                }
            }

            public int position;
            public int normal;
            public int texCoord;
        }
    }
}
