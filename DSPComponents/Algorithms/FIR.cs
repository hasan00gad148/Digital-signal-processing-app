using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{

    /*
     
        FIR Steps:

1 - Get Impulse Equation based on Filter type.
2 - Normalize all frequencies.
3 - Get new cutoff or bands.
4 - Get Window Equation based on Stop Attenuation.
5 - Get N from Window Equation.
6 - Get coefficients of h(n) and W(n) and Multiply them to form samples.
7 - Convolve Samples with input signal and place result in Y(n). 

         */

    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();

            double cutoff = 0, f1 = 0, f2 = 0;
            double deltaf = InputTransitionBand / InputFS;
            if (InputCutOffFrequency != null)
            {
                 cutoff = (double)InputCutOffFrequency / InputFS + (deltaf / 2);
            }
            else
            { 
                f1 = (double)InputF1 / InputFS - (deltaf / 2);
                f2 = (double)InputF2 / InputFS + (deltaf / 2);
            }

            int type = 0;
            int N = 0;

            if (InputStopBandAttenuation > 53)
            {
                type = 4;
                N = Convert.ToInt32( Math.Round(5.5 / deltaf));

            }
            else if (InputStopBandAttenuation > 44)
            {
                type = 3;
                N = Convert.ToInt32(Math.Round(3.3 / deltaf));

            }
            else if (InputStopBandAttenuation > 21)
            {
                type = 2;
                N = Convert.ToInt32(Math.Round(3.1 / deltaf));

            }
            else
            {
                type = 1;
                N = Convert.ToInt32(Math.Round(0.9 / deltaf));

            }
            if (N % 2 == 0)
            {
                N += 1;
            }
           // float[] hn = new float[N];
            List<float> hn = new List<float>(N);
            int[] hnIndices = new int[N];
            List<float> y= new List<float>();
            for(int i =0; i<N; ++i)
            {
                hn.Add(0);
            }

            int N2 = Convert.ToInt32(Math.Floor((double)N / 2));
            for (int i = 0; i <= N2; ++i)
            {
                double hd = FilterEquation(InputFilterType, cutoff, f1, f2, i);
                double w = WindowEquation(type, N, i);
                double tmpHn = (hd * w);
                hn[N2 + i] = (float)tmpHn;
                hn[N2 - i] = (float)tmpHn;
                hnIndices[i] = -N2 + i;
                hnIndices[N-1-i] = N2 - i;
            }

            OutputHn = new Signal(hn, hnIndices.ToList<int>(), false);
            DirectConvolution dc = new DirectConvolution();
            dc.InputSignal1 = InputTimeDomainSignal;
            dc.InputSignal2 = OutputHn;
            dc.Run();
            OutputYn = dc.OutputConvolvedSignal;

        }
        private double FilterEquation(FILTER_TYPES ft, double cutOfFreq, double f1, double f2, int n)
        {


            switch (ft)
            {
                case FILTER_TYPES.LOW:
                    {
                        if (n > 0)
                        {
                            double tmp = 2 * cutOfFreq * Math.Sin(2 * Math.PI * cutOfFreq * n) / (2 * Math.PI *cutOfFreq * n);
                            return tmp;
                        }
                        else
                        {
                            return 2 * cutOfFreq;
                        }
                       
                    }
                case FILTER_TYPES.HIGH:
                    {
                        if (n > 0)
                        {
                           return -2 * cutOfFreq * Math.Sin(2 * Math.PI * cutOfFreq * n) / (2 * Math.PI *cutOfFreq * n);
                        }
                        else
                        {
                            return 1 - (2 * cutOfFreq);
                        }
                     
                    }
                case FILTER_TYPES.BAND_PASS:
                    {
                        if (n > 0)
                        {
                            double pf1 = -2 * f1 * Math.Sin(2 * Math.PI * f1 * n) / (2 * Math.PI * f1 * n);
                            double pf2 = 2 * f2 * Math.Sin(2 * Math.PI * f2 * n) / (2 * Math.PI * f2 * n);
                            return (pf1 + pf2);
                        }
                        else
                        {
                            return 2*(f2 - f1);
                        }
                        
                    }
                case FILTER_TYPES.BAND_STOP:
                    {
                        if (n > 0)
                        {
                            double pf1 = 2 * f1 *Math.Sin(2 * Math.PI * f1 * n) / (2 * Math.PI * f1 * n);
                            double pf2 = -2 * f2 * Math.Sin(2 * Math.PI * f2 * n) / (2 * Math.PI * f2 * n);
                            return (pf1 + pf2);
                        }
                        else
                        {
                            return 1 - 2*(f2 - f1);
                        }
                  
                    }
            }
            return 0;
        }
        private double WindowEquation(int type, int N, int i)
        {
            switch (type)
            {
                case 1:
                    {

                        return 1;
                    }
                case 2:
                    {
                        return  ( 0.5 + 0.5 * Math.Cos(2 * Math.PI * i/N));
                    }
                case 3:
                    {
                        return (0.54 + 0.46 * Math.Cos(2 * Math.PI * i / N));
                      
                    }
                case 4:
                    {
                        return (0.42 + 0.5 * Math.Cos(2 * Math.PI * i / (N - 1)) + 0.08 * Math.Cos(4 * Math.PI * i / (N - 1)));
                    }
            }
            return 0;
        }
    }

}
