using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            List<float> Outputsig1amp;
            List<float> Outputsig1phase;
            List<float> Outputsig2amp;
            List<float> Outputsig2phase;

            DiscreteFourierTransform dft = new DiscreteFourierTransform();

            dft.InputTimeDomainSignal = InputSignal1;
            dft.Run();
            Outputsig1amp = dft.OutputFreqDomainSignal.FrequenciesAmplitudes;
            Outputsig1phase = dft.OutputFreqDomainSignal.FrequenciesPhaseShifts;

            dft.InputTimeDomainSignal = InputSignal2;
            dft.Run();
            Outputsig2amp = dft.OutputFreqDomainSignal.FrequenciesAmplitudes;
            Outputsig2phase = dft.OutputFreqDomainSignal.FrequenciesPhaseShifts;

 
            List<Complex> sig1 = new List<Complex>();

            int N1 = InputSignal1.Samples.Count;
            for (int n = 0; n < N1; ++n)
            {
                float tmp1 = Outputsig1amp[n] * (float)Math.Cos(Outputsig1phase[n]);
                float tmp2 = Outputsig1amp[n] * (float)Math.Sin(Outputsig1phase[n]);
                Complex c = new Complex(tmp1, tmp2);
                sig1.Add(c);
            }

            List<Complex> sig2 = new List<Complex>();

            int N2 = InputSignal2.Samples.Count;
            for (int n = 0; n < N2; ++n)
            {
                float tmp1 = Outputsig2amp[n] * (float)Math.Cos(Outputsig2phase[n]);
                float tmp2 = Outputsig2amp[n] * (float)Math.Sin(Outputsig2phase[n]);
                Complex c = new Complex(tmp1, tmp2);
                sig2.Add(c);
            }

            List<Complex> sign = new List<Complex>();
            for (int n = 0; n < Math.Min(N1,N2); ++n)
            {
                Complex c = Complex.Multiply(sig1[n], sig2[n]);
                sign.Add(c);
            }


            List<float> tVals = new List<float>();

            for (int n = 0; n < sign.Count; ++n)
            {
                Complex tmp = 0;
                double theta = 0;

                for (int k = 0; k < sign.Count; ++k)
                {

                    theta = 2 * Math.PI * k * n / sign.Count;
                    tmp += sign[k] * Complex.Pow(Math.E, new Complex(0, -theta));

                }
                tmp /= sign.Count;

                tVals.Add((float)Math.Round(tmp.Real, 3));


            }
            OutputConvolvedSignal = new Signal(tVals, true);
        }
    }

}
