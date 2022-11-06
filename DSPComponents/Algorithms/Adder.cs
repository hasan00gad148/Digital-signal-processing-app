using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            int n = Math.Min(InputSignals[0].Samples.Count, InputSignals[1].Samples.Count);
           

            List<float> Samples = new List<float>();
            for (int i = 0; i <n; ++i) {
                Samples.Add(InputSignals[0].Samples[i] + InputSignals[1].Samples[i]) ;
            }
            OutputSignal = new Signal(Samples,false);

        }
    }
}