﻿using MatFramework.DataFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStudioROBOT2016.Models.DataFlow.Generator
{
    public class MatDataConstant : MatDataObject
    {
        public MatDataConstant(string name) : base(name)
        {
            IntValue.MatPortConnect += IntValue_MatPortConnect;
            DoubleValue.MatPortConnect += DoubleValue_MatPortConnect;

            outputs.Add(IntValue);
            outputs.Add(DoubleValue);
        }

        public MatDataOutputPort IntValue = new MatDataOutputPort(typeof(int), "Int");
        private void IntValue_MatPortConnect(object sender, MatPortConnectEventArgs e)
        {
            IntValue.Value = new MatData(typeof(int), (int)ConstantValue);
        }

        public MatDataOutputPort DoubleValue = new MatDataOutputPort(typeof(double), "Double");
        private void DoubleValue_MatPortConnect(object sender, MatPortConnectEventArgs e)
        {
            DoubleValue.Value = new MatData(typeof(double), ConstantValue);
        }

        private double _ConstantValue = 2.35;
        public double ConstantValue
        {
            get { return _ConstantValue; }
            set
            {
                if (_ConstantValue == value)
                    return;

                _ConstantValue = value;

                IntValue.Value = new MatData(typeof(int), (int)value);
                IntValue.Value = new MatData(typeof(double), value);

                RaisePropertyChanged("ConstantValue");
            }
        }

        public override MatDataObject GetNewInstance()
        {
            return new MatDataConstant(Name);
        }
    }
}