using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MatFramework.Converters
{
    public class DataConverter
    {
        [StructLayout(LayoutKind.Explicit)]
        struct IntFloat
        {
            [FieldOffset(0)]
            public byte Byte1;

            [FieldOffset(1)]
            public byte Byte2;

            [FieldOffset(2)]
            public byte Byte3;

            [FieldOffset(3)]
            public byte Byte4;

            [FieldOffset(0)]
            public float Float;
        }

        public static double BitsStringToDouble(string bits)
        {
            if (bits.Length != 5) return 0.0;

            IntFloat value = new IntFloat();

            value.Byte1 = (byte)bits[0];
            value.Byte2 = (byte)bits[1];
            value.Byte3 = (byte)bits[2];
            value.Byte4 = (byte)bits[3];

            return (double)value.Float;
        }
    }
}
