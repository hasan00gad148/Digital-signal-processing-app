using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Shifter : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int ShiftingValue { get; set; }
        public Signal OutputShiftedSignal { get; set; }

        public override void Run()
        {
          //  InputSignal.Frequencies.Add(0);

            List<float> sample = InputSignal.Samples ;
            List<int> indices = InputSignal.SamplesIndices;

            if (sample[0]< sample[sample.Count - 1])
            {
                for(int i  = 0; i < sample.Count; ++i)
                {
                    indices[i] -= (ShiftingValue);
                }

            }
            else
            {
                for (int i = 0; i < sample.Count; ++i)
                {
                    indices[i] += (ShiftingValue);
                }
            }
            OutputShiftedSignal = new Signal(sample,false);
            OutputShiftedSignal.SamplesIndices = indices;


    }
}
}
