using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            /*
             Task 4
Add to your application the following features:
1)	Delaying or advancing a signal by k steps
2)	Folding a signal 
3)	Delaying or advancing a folded signal 
4)	Compute moving average y(n) for signal x(n) let the user enter the number of points included in averaging 
5)	Accumulation of input signal   
 
6)	The ability to convolve two signals 

             */
           
            List<float> tVals = new List<float>();
            List<Complex> res = new List<Complex>();

            int N = InputFreqDomainSignal.Frequencies.Count;
            for (int n = 0; n < N; ++n)
            {
                float tmp1 = InputFreqDomainSignal.FrequenciesAmplitudes[n] * (float)Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[n]);
                float tmp2 = InputFreqDomainSignal.FrequenciesAmplitudes[n] * (float)Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[n]);
                Complex c = new Complex(tmp1,tmp2);
                res.Add(c);
            }
                for (int n = 0; n < N; ++n)
            {
                Complex tmp = 0;
                double theta = 0;
                
                for (int k = 0; k < N; ++k)
                {

                    theta = 2 * Math.PI * k * n/ N;
                    tmp += res[k] * Complex.Pow(Math.E,new Complex(0, -theta));
                    
                }
                tmp /= N;
              
                tVals.Add((float)Math.Round(tmp.Real, 3));


            }
            tVals.Sort();
            OutputTimeDomainSignal = new Signal(tVals, true);
        }
    }
    
}
