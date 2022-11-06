using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;
namespace DSPAlgorithms.Algorithms
{
    public class FastCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            List<float> Outputsig1amp;
            List<float> Outputsig1phase;
            List<float> Outputsig2amp;
            List<float> Outputsig2phase;

            DiscreteFourierTransform dft = new DiscreteFourierTransform();
            if (InputSignal2 == null)
            {
                InputSignal2 = InputSignal1;
            }
            dft.InputTimeDomainSignal = InputSignal1;
            dft.Run();
            Outputsig1amp = dft.OutputFreqDomainSignal.FrequenciesAmplitudes;
            Outputsig1phase = dft.OutputFreqDomainSignal.FrequenciesPhaseShifts;

            dft.InputTimeDomainSignal = InputSignal2;
            dft.Run();
            Outputsig2amp = dft.OutputFreqDomainSignal.FrequenciesAmplitudes;
            Outputsig2phase = dft.OutputFreqDomainSignal.FrequenciesPhaseShifts;

            OutputNonNormalizedCorrelation = new List<float>();
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
            for (int n = 0; n < Math.Min(N1, N2); ++n)
            {
                Complex c = Complex.Multiply(Complex.Conjugate(sig1[n]), sig2[n]);
                sign.Add(c);
            }


            for (int n = 0; n < sign.Count; ++n)
            {
                Complex tmp = 0;
                double theta = 0;

                for (int k = 0; k < sign.Count; ++k)
                {

                    theta = 2 * Math.PI * k * n / sign.Count;
                    tmp += sign[k] * Complex.Pow(Math.E, new Complex(0, -theta));

                }
                tmp /= (sign.Count* sign.Count);

                OutputNonNormalizedCorrelation.Add((float)Math.Round(tmp.Real, 3));


            }

            OutputNormalizedCorrelation = new List<float>();
            float sumSqSig1 = 0;
            for (int i = 0; i < InputSignal1.Samples.Count; ++i)
            {
                sumSqSig1 += (InputSignal1.Samples[i] * InputSignal1.Samples[i]);
            }
            float sumSqSig2 = 0;
            for (int i = 0; i < InputSignal2.Samples.Count; ++i)
            {
                sumSqSig2 += (InputSignal2.Samples[i] * InputSignal2.Samples[i]);
            }
            float normlizer = (float)Math.Sqrt(sumSqSig2 * sumSqSig1) / Math.Max(InputSignal1.Samples.Count, InputSignal2.Samples.Count);
            float tmp4 = 0;
            for (int i = 0; i < OutputNonNormalizedCorrelation.Count; ++i)
            {
                tmp4 = OutputNonNormalizedCorrelation[i] / normlizer;
                OutputNormalizedCorrelation.Add(tmp4);
            }
        }
    }

}
