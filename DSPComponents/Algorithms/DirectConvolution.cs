using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            int len1 = InputSignal1.Samples.Count ;
            int len2 = InputSignal2.Samples.Count;
            List<float> sample = new List<float>();
            int n = 0;
            int k = 0;
            float tmp = 0;
            for (n = 0; n < len1+len2-1; n++)
            {
                tmp = 0;
                for ( k = 0; k <len1 ; k++)
                {

                    if (n - k < 0  )
                    {
                        break;
                    }
                    else if ( n - k >= len2)
                    {
                        continue;
                    }
                    else
                    {
                        tmp += InputSignal1.Samples[k] * InputSignal2.Samples[n - k];
                    }
                }

                sample.Add(tmp);
            }
            if (sample[sample.Count - 1] ==0)
            {
                sample.RemoveAt(sample.Count - 1);
            }
                   List<int> ind =InputSignal1.SamplesIndices;
                   int min_ind2 = InputSignal2.SamplesIndices.Min();
            

            for (int i = 0; i<len1;i++)
            {
                ind[i] += min_ind2;
            }
            int max_ind1 = InputSignal1.SamplesIndices.Max();
            for (int i = 1; i <= sample.Count-len1; i++)
            {
                ind.Add(max_ind1+i);
            }
            OutputConvolvedSignal = new Signal(sample, ind,false);
        }
    }
}
