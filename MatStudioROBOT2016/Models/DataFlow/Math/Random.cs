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
            myControl.DataContext = this;
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

        public void Calc(double sigma)
        {
            double sum = 0.0;
            System.Random signalSource = new System.Random();
            double[] noise = new double[114514];
            int falseCount = 0;

            histogram.Clear();

            for (double i = -6.0; i < 6.0; i += 0.1)
            {
                histogram.Add(new Coord2D(i, 0.0));
            }

            for (int i = 0; i < 114514; i++)
            {
                byte signal = (byte)signalSource.Next(0, 2);
                noise[i] = MatRandom.GetRandom(6, -6) * System.Math.Sqrt(sigma);

                double realSignal = signal + noise[i];

                if (signal == 0)
                {
                    if (realSignal > 0.5)
                        falseCount++;
                }
                else
                {
                    if (realSignal <= 0.5)
                        falseCount++;
                }

                sum += noise[i];

                foreach (Coord2D value in histogram)
                {
                    if (noise[i] - value.X < 0.1 && noise[i] >= value.X)
                    {
                        value.Y += 1.0;
                        break;
                    }
                }
            }

            double abe = sum / 114514.0;

            double hensa = 0.0;
            foreach (double xi in noise)
            {
                hensa += (xi - abe) * (xi - abe);
            }

            double bunsan = hensa / 114514;
            double errorRatio = (double)falseCount / 114514.0;

            myControl.SetHistogram(histogram);
            myControl.SetResultString("平均：" + abe.ToString("f5") + "  分散：" + bunsan.ToString("f5") + "  誤り率：" + errorRatio.ToString("f5"));
        }
    }
}
