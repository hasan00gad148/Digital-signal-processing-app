using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    /*
     Task 4
Add to your application the following features:
1)	The ability to apply Fourier transform to any input signal then display frequency versus amplitude
    and frequency versus phase relations after asking the user to enter the sampling frequency in HZ.

2)	The frequency components should be saved in txt file in polar form (amplitude and phase) 

     */
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
          OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Samples, false);
            List<float> Amps = new List<float>();
            List<float> phases = new List<float>();

            int N = InputTimeDomainSignal.Samples.Count;
           
            for (int k =0; k< N ; ++k)
            {
                float re = 0;
                float im = 0;
                double theta = 0;
                for (int n = 0; n < N; ++n)
                {
                     theta = 2 *Math.PI * k * n / N;
                     re += (float)Math.Cos(theta) * InputTimeDomainSignal.Samples[n];
                     im += -1 * (float)Math.Sin(theta) * InputTimeDomainSignal.Samples[n];
                }
                float re2 = re * re;
                float im2 = im * im;
                
               Amps.Add((float)Math.Sqrt(re2 + im2));
               phases.Add((float)Math.Atan2(im , re) );
            }
            OutputFreqDomainSignal.FrequenciesAmplitudes = Amps;
            OutputFreqDomainSignal.FrequenciesPhaseShifts= phases;
        }
    }
}
