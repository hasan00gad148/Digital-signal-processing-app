using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class SinCos: Algorithm
    {
        public string type { get; set; }
        public float A { get; set; }
        public float PhaseShift { get; set; }
        public float AnalogFrequency { get; set; }
        public float SamplingFrequency { get; set; }
        public List<float> samples { get; set; }
        public override void Run()
        {

            // throw new NotImplementedException();
            samples = new List<float>();
            double a = A;
            double ph = PhaseShift;
            double fa = AnalogFrequency;
            double fs = SamplingFrequency;
            double f = fa / fs;
            if (f > 0.5)
            {
                throw new Exception("aliasing");
            }
            for(int i = 0; i < fs; ++i)
            {
                samples.Add((float)SinOrCos(type, f, a, ph, i));
            }
        }
        private double SinOrCos(string type , double f , double a , double ph,int n)
        {
            if(type == "sin")
            {
                double tmp =  a*Math.Sin(2 *Math.PI * f * n + ph);
                return tmp;
            }
            else if (type == "cos")
            {
                double tmp = a * Math.Cos(2 * Math.PI * f * n + ph);
                return tmp;
            }
            else
            {
                throw new Exception("not sin or cos");
            }
            return 0;
        }
    }
}
