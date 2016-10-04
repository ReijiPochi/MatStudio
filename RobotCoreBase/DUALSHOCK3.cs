using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotCoreBase
{
    public enum DUALSHOCK3Buttons
    {
        UpArrow,
        DownArrow,
        RightArrow,
        LeftArrow,
        Sankaku,
        Batsu,
        Maru,
        Shikaku,
        L1,
        L2,
        R1,
        R2,
        Start,
        Select,
        AnalogL_X,
        AnalogL_Y,
        AnalogR_X,
        AnalogR_Y
    }

    public class DUALSHOCK3Button
    {
        public DUALSHOCK3Button(DUALSHOCK3Buttons btn, int startTime)
        {
            Button = btn;
            StartTime = startTime;
        }

        public DUALSHOCK3Buttons Button { get; private set; }

        public double Value { get; set; }

        public int StartTime { get; private set; }

        private int _EndTime;
        public int EndTime
        {
            get { return _EndTime; }
            set
            {
                if (_EndTime == value) return;

                _EndTime = value;
                EndTimeChanged?.Invoke(this);
            }
        }

        public event EndTimeChangedEventHandler EndTimeChanged;
    }

    public delegate void EndTimeChangedEventHandler(object sender);

    public class DUALSHOCK3
    {
        public DUALSHOCK3()
        {

        }

        public DUALSHOCK3(string bytes)
        {
            if (bytes.Length != 10) return;

            Time = (byte)bytes[0] | (byte)bytes[1] << 8 | (byte)bytes[2] << 16 | (byte)bytes[3] << 24;
            UpArrow = (bytes[4] & 0x01) != 0;
            DownArrow = (bytes[4] & 0x02) != 0;
            RightArrow = (bytes[4] & 0x04) != 0;
            LeftArrow = (bytes[4] & 0x08) != 0;
            Sankaku = (bytes[4] & 0x10) != 0;
            Batsu = (bytes[4] & 0x20) != 0;
            Maru = (bytes[4] & 0x40) != 0;
            Shikaku = (bytes[5] & 0x01) != 0;
            L1 = (bytes[5] & 0x02) != 0;
            L2 = (bytes[5] & 0x04) != 0;
            R1 = (bytes[5] & 0x08) != 0;
            R2 = (bytes[5] & 0x10) != 0;
            AnalogL_X = (sbyte)bytes[6];
            AnalogL_Y = (sbyte)bytes[7];
            AnalogR_X = (sbyte)bytes[8];
            AnalogR_Y = (sbyte)bytes[9];
        }

        public int Time;
        public bool UpArrow;
        public bool DownArrow;
        public bool RightArrow;
        public bool LeftArrow;
        public bool Sankaku;
        public bool Batsu;
        public bool Maru;
        public bool Shikaku;
        public bool L1;
        public bool L2;
        public bool R1;
        public bool R2;
        public bool Start;
        public bool Select;
        public sbyte AnalogL_X;
        public sbyte AnalogL_Y;
        public sbyte AnalogR_X;
        public sbyte AnalogR_Y;

        public byte[] GetBytes()
        {
            byte[] data = new byte[10];

            data[0] = (byte)(Time & 0x000000FF);
            data[1] = (byte)((Time & 0x0000FF00) >> 8);
            data[2] = (byte)((Time & 0x00FF0000) >> 16);
            data[3] = (byte)((Time & 0xFF000000) >> 24);
            data[4] = (byte)(ToInt(UpArrow) | ToInt(DownArrow) << 1 | ToInt(RightArrow) << 2 | ToInt(LeftArrow) << 3 | ToInt(Sankaku) << 4 | ToInt(Batsu) << 5 | ToInt(Maru) << 6);
            data[5] = (byte)(ToInt(Shikaku) | ToInt(L1) << 1 | ToInt(L2) << 2 | ToInt(R1) << 3 | ToInt(R2) << 4);
            data[6] = (byte)AnalogL_X;
            data[7] = (byte)AnalogL_Y;
            data[8] = (byte)AnalogR_X;
            data[9] = (byte)AnalogR_Y;

            return data;
        }

        public List<DUALSHOCK3Buttons> GetPressedButtons()
        {
            List<DUALSHOCK3Buttons> list = new List<DUALSHOCK3Buttons>();

            if (UpArrow) list.Add(DUALSHOCK3Buttons.UpArrow);
            if (DownArrow) list.Add(DUALSHOCK3Buttons.DownArrow);
            if (RightArrow) list.Add(DUALSHOCK3Buttons.RightArrow);
            if (LeftArrow) list.Add(DUALSHOCK3Buttons.LeftArrow);
            if (Sankaku) list.Add(DUALSHOCK3Buttons.Sankaku);
            if (Batsu) list.Add(DUALSHOCK3Buttons.Batsu);
            if (Maru) list.Add(DUALSHOCK3Buttons.Maru);
            if (Shikaku) list.Add(DUALSHOCK3Buttons.Shikaku);
            if (L1) list.Add(DUALSHOCK3Buttons.L1);
            if (L2) list.Add(DUALSHOCK3Buttons.L2);
            if (R1) list.Add(DUALSHOCK3Buttons.R1);
            if (R2) list.Add(DUALSHOCK3Buttons.R2);
            if (Start) list.Add(DUALSHOCK3Buttons.Start);
            if (Select) list.Add(DUALSHOCK3Buttons.Select);
            if (AnalogL_X != 0) list.Add(DUALSHOCK3Buttons.AnalogL_X);
            if (AnalogL_Y != 0) list.Add(DUALSHOCK3Buttons.AnalogL_Y);
            if (AnalogR_X != 0) list.Add(DUALSHOCK3Buttons.AnalogR_X);
            if (AnalogR_Y != 0) list.Add(DUALSHOCK3Buttons.AnalogR_Y);

            return list;
        }

        private int ToInt(bool value)
        {
            if (value)
                return 1;
            else
                return 0;
        }
    }
}
