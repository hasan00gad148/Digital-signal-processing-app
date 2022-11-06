using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {

            int n = InputSignal.Samples.Count;
            InputMinRange = InputSignal.Samples.Min();
            InputMaxRange = InputSignal.Samples.Max();
            List<float> Samples = new List<float>();
            for (int i = 0; i < n; ++i)
            {
                Samples.Add((InputSignal.Samples[i]- InputMinRange)/(InputMaxRange - InputMinRange));
            }
            OutputNormalizedSignal = new Signal(Samples, false);
        }
    }
}
