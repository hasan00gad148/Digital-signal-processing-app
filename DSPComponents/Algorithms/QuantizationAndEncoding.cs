using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        /*
         1)	The ability to quantize an input signal (its samples),
        the application should ask the user for the needed levels or number of bits available 
        (in case of number of bits the application should compute from it the appropriate number of levels). 
        Thereafter, the application should display the quantized signal and quantization error besides the encoded signal.
         */
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }

        public override void Run()
        {
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            OutputSamplesError = new List<float>();
            List<float> OutputSamples = new List<float>();
            List<float> intervals = new List<float>();
            List<float> mids = new List<float>();
            float max = InputSignal.Samples.Max();
            float min = InputSignal.Samples.Min();
          


            if (InputLevel==0)
            {
                InputLevel = Convert.ToInt32( Math.Pow(2, InputNumBits));
            }
               else
                {
                 InputNumBits = Convert.ToInt32(Math.Log(InputLevel, 2.0));
               }
            double r = (max - min) / InputLevel;
            r = Math.Round(r, 6);
            float tmp2 = min;
            while (tmp2 <= max)
            {
                intervals.Add( tmp2);
                tmp2 += (float)r;
                tmp2 =(float) Math.Round(tmp2, 6);
            }
            for(int i = 0; i < intervals.Count;++i)
            {
                if(i+1>= intervals.Count)
                {
                    break;
                }
                decimal mid = (decimal)(intervals[i + 1] + intervals[i]) / 2;
                mid = Math.Round(mid, 6);
                mids.Add((float)mid);
            }
            int l = 0;
            for (int i = 0; i < InputSignal.Samples.Count; ++i)
            {
                for (int j = 0; j < intervals.Count; ++j )
                {
                    if (j + 1 >= intervals.Count)
                    {
                        break;
                    }
                    if (InputSignal.Samples[i] >= intervals[j] && InputSignal.Samples[i] <= intervals[j+1]){

                        l = j;
                        break;
                    }
                }
              
                int remainder;
                string result = string.Empty;
                int tmp3 = InputNumBits;
                int tmp4 = l;
                while (tmp3 > 0)
                {
                    remainder = tmp4 % 2;
                    tmp4 /= 2;
                    result = remainder.ToString() + result;
                    tmp3-=1;
                }
                

                OutputIntervalIndices.Add(l+1);
                OutputSamples.Add(mids[l]);
                //OutputEncodedSignal.Add(Convert.ToString(l,2));
                OutputEncodedSignal.Add(result);
                OutputSamplesError.Add(mids[l] - InputSignal.Samples[i] );
               
            }
            OutputQuantizedSignal = new Signal(OutputSamples, false);
        }
    }
}
