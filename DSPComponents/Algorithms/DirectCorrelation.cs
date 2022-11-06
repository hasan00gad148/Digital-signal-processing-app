using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            if(InputSignal2 == null)
            {
                InputSignal2 = InputSignal1;
            }
            int len1 = InputSignal1.Samples.Count;
            int len2 = InputSignal2.Samples.Count;

            int maxLen = Math.Max(len1, len2);
            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();

            float tmp = 0;

            int Len = 0;
            //           if (InputSignal1.Periodic==true&& InputSignal2.Periodic == true)
            //           {
            //               Len = maxLen;
            //           }
            //           else
            //           {
            //               Len = minLen;
            //           }

            if (len1 != len2)
            {
               
                for (int n = 0; n< len1-1; n++)
                {
                    tmp = 0;
                    for (int k = 0; k <= n; k++)
                    {

                        if (n + k >= len2 )
                        {
                            break;
                        }
                        else if (n >= len1 )
                        {
                            break;
                        }
                        else
                        {
                            tmp += InputSignal1.Samples[n-k] * InputSignal2.Samples[(k)%len2];
                        }
                    }

                    OutputNonNormalizedCorrelation.Add(tmp/(len1+len2-1) );
                }
                int tmp3 = len1 - 1;
                for(int i = 0; i < len2; ++i)
                {
                    tmp = 0;
                    for (int k = 0; k < len2-i; ++k)
                    {
                        tmp += InputSignal1.Samples[tmp3 - k] * InputSignal2.Samples[(k) % len2];
                    }
                    OutputNonNormalizedCorrelation.Add(tmp / (len1 + len2 - 1));
                }
            }
         
            else
            {
                for (int k = 0; k < maxLen; k++)
                {
                    tmp = 0;
                    for (int n = 0; n < maxLen; n++)
                    {

                        if (n + k >= len2 && InputSignal1.Periodic == false && InputSignal2.Periodic == false)
                        {
                            break;
                        }
                        else if (n >= len1 && InputSignal1.Periodic == false && InputSignal2.Periodic == false)
                        {
                            break;
                        }
                        else
                        {
                            tmp += (InputSignal1.Samples[n % len1] * InputSignal2.Samples[(n + k) % len2]);
                        }
                    }

                    OutputNonNormalizedCorrelation.Add(tmp / maxLen);
                }
            }
 //           if (OutputNonNormalizedCorrelation[OutputNonNormalizedCorrelation.Count - 1] == 0)
 //           {
 //               OutputNonNormalizedCorrelation.RemoveAt(OutputNonNormalizedCorrelation.Count - 1);
 //          }

            float sumSqSig1 = 0;
            for (int i = 0; i < len1; ++i)
            {
                sumSqSig1 += (InputSignal1.Samples[i] * InputSignal1.Samples[i]);
            }
            float sumSqSig2 = 0;
            for (int i = 0; i < len2; ++i)
            {
                sumSqSig2 += (InputSignal2.Samples[i] * InputSignal2.Samples[i]);
            }
            float normlizer = (float)Math.Sqrt(sumSqSig2 * sumSqSig1) / maxLen;
            float tmp2 = 0;
            for (int i =0; i < OutputNonNormalizedCorrelation.Count; ++i)
            {
                tmp2 = OutputNonNormalizedCorrelation[i] / normlizer;
                OutputNormalizedCorrelation.Add(tmp2);
            }
        }
    }
}
/*
 Task 5
Add to your application the following features:
1)	The ability to compute normalized cross-correlation of two signals or normalized auto-correlation of a signal.
2)	The ability to perform time delay analysis, given two periodic signals and the sampling period , find approximately the delay between them.
3)	The ability to perform time delay analysis, given two periodic signals and the sampling period Ts, find approximately the delay between them. 
(make a (TimeDelay.cs) for this requirement @ Algorithms folder to write code through)
 */
/*
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
 */