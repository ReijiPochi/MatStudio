using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MatFramework.Graphics._3D.Objects;
using MatFramework.Graphics._3D;

namespace MatFramework.Graphics
{
    public abstract class MatWorld : MatObject, IDisposable
    {
        public MatWorld()
        {
            Objects = new List<Mat3DObject>();
        }

        public List<Mat3DObject> Objects { get; private set; }

        public MatCamera ActiveCamera { get; set; }

        public virtual void OnControlDataInput(object data)
        {

        }

        public virtual void OnRender(double viewWidth, double viewHeight)
        {
            if (ActiveCamera != null)
            {
                ActiveCamera.ViewPortWidth = viewWidth;
                ActiveCamera.ViewPortHeight = viewHeight;

                ActiveCamera.CameraUpdate();
            }

            foreach(Mat3DObject obj in Objects)
            {
                obj.Draw(this);
            }
        }

        public void Dispose()
        {
            foreach(Mat3DObject obj in Objects)
            {
                obj.Dispose();
            }
        }
    }
}
