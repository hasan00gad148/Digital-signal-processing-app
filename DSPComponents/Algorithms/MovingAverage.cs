using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            int len = InputSignal.Samples.Count-2;
            double tmp = InputWindowSize / 2;
            int iw = Convert.ToInt32(Math.Floor(tmp));
            List<float> sample = InputSignal.Samples;



        for (int i =iw; i < len; i += 1)
             {
                for (int j = 1; j<= iw; j += 1)
                { 
                    sample[i] += (sample[i + j] + sample[i - j]);
                }
                sample[i] /= InputWindowSize;
            }
            for (int i = 0; i < iw; ++i)
            {
                sample.RemoveAt(0);
                sample.RemoveAt(sample.Count - 1);
            }
            OutputAverageSignal = new Signal(sample, false);
        }
       
    }
}
