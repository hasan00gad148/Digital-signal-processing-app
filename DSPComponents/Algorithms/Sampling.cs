using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }
        
        


        public override void Run()
        {
            FIR FIR1 = new FIR();
            FIR1.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
            FIR1.InputFS = 8000;
            FIR1.InputStopBandAttenuation = 50;
            FIR1.InputCutOffFrequency = 1500;
            FIR1.InputTransitionBand = 500;



            OutputSignal = InputSignal;
            if (M != 0 & L == 0)
            {
                FIR1.InputTimeDomainSignal = OutputSignal;
                FIR1.Run();
                OutputSignal = FIR1.OutputYn;
                int i = 0;
                while (i < OutputSignal.Samples.Count-1)
                {
                    for (int j = 1; j < M; ++j)
                    {
                        OutputSignal.Samples.RemoveAt(i + 1);
                    }
                    ++i;
                }
                int tmp2 = OutputSignal.SamplesIndices.Count;
                for (int j = OutputSignal.Samples.Count; j < tmp2; ++j)
                {
                    int tmp = OutputSignal.SamplesIndices.Count - 1;
                    OutputSignal.SamplesIndices.RemoveAt(tmp);
                }
            }
            else if (L != 0 & M == 0)
            {
                int i = 0;

                int len = OutputSignal.Samples.Count *L-1;
                while (i < len)
                {
                    for (int j = 1; j < L; ++j)
                    {
                        OutputSignal.Samples.Insert(i + 1, 0);
                    }
                    i+=L;
                }
                int tmp2 = OutputSignal.SamplesIndices.Count;
                for (int j = 1; j <= OutputSignal.Samples.Count - tmp2; ++j)
                {
                    int tmp = OutputSignal.SamplesIndices[OutputSignal.SamplesIndices.Count - 1];
                    OutputSignal.SamplesIndices.Add(tmp + j);
                }
                FIR1.InputTimeDomainSignal = OutputSignal;
                FIR1.Run();
                OutputSignal = FIR1.OutputYn;

            }
            else if (L != 0 & M != 0)
            {
                int i = 0;
                int len = OutputSignal.Samples.Count*L-1 ;
                while (i < len)
                {
                    for (int j = 1; j < L; ++j)
                    {
                        OutputSignal.Samples.Insert( i + 1 ,0);
                    }
                    i += L; 
                }
                int tmp2 = OutputSignal.SamplesIndices.Count;
                for (int j = 1; j <= OutputSignal.Samples.Count -tmp2; ++j)
                {
                    int tmp = OutputSignal.SamplesIndices[OutputSignal.SamplesIndices.Count - 1];
                    OutputSignal.SamplesIndices.Add(tmp + j);
                }
                FIR1.InputTimeDomainSignal = OutputSignal;
                FIR1.Run();
                OutputSignal = FIR1.OutputYn;
                i = 0;
                while (i < OutputSignal.Samples.Count-1)
                {
                    for (int j = 1; j < M; ++j)
                    {
                        OutputSignal.Samples.RemoveAt(i + 1);
                    }
                    ++i;
                }
                 tmp2 = OutputSignal.SamplesIndices.Count;
                for (int j = OutputSignal.Samples.Count; j < tmp2; ++j)
                {
                    int tmp = OutputSignal.SamplesIndices.Count - 1;
                    OutputSignal.SamplesIndices.RemoveAt(tmp);
                }

            }
            else
            {
                throw new Exception("error: m & l == 0 \n");
            }

        }
    }
    
}
