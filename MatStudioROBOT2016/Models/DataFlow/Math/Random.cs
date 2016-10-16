using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MatFramework.DataFlow;
using MatFramework.Math;
using MatFramework;

namespace MatStudioROBOT2016.Models.DataFlow.Math
{
    public class Random : MatDataObject
    {
        public Random(string name) : base(name)
        {
            double preX;

            for (int i = -60; i < 60; i ++)
            {
                histogram.Add(new Coord2D(i / 10.0, 0.0));
            }

            for (int i = 0; i < 114514; i++)
            {
                double xi = MatRandom.GetRandom(6, -6);
                preX = xi;

                foreach(Coord2D value in histogram)
                {
                    if (xi - value.X < 0.1 && xi >= value.X)
                    {
                        value.Y += 1.0;
                        break;
                    }
                }
            }

            myControl.SetHistogram(histogram);
        }

        private List<Coord2D> histogram = new List<Coord2D>();

        private RandomControl myControl = new RandomControl();

        public override Control GetInterfaceControl()
        {
            return myControl;
        }

        public override MatDataObject GetNewInstance()
        {
            return new Random(Name);
        }
    }
}
