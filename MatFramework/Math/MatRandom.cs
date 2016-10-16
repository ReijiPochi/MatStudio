using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.Math
{
    public class MatRandom
    {
        private static Random random = new Random();

        public static double GetRandom(int max, int min)
        {
            int count = max - min;
            if (count < 0)
                return 0;

            double randomValue = 0.0;

            for (int c = 0; c < count; c++)
            {
                randomValue += random.NextDouble();
            }

            randomValue -= count / 2.0;

            return randomValue;
        }
    }
}
