﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {
            int n = InputSignal.Samples.Count;


            List<float> Samples = new List<float>();
            for (int i = 0; i < n; ++i)
            {
                Samples.Add(InputSignal.Samples[i] * InputConstant);
            }
            OutputMultipliedSignal = new Signal(Samples, false);
        }
    }
}
