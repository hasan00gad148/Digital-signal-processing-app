using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Folder : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputFoldedSignal { get; set; }

        public override void Run()
        {
            List<float> sample = InputSignal.Samples;
            List<int> indices = InputSignal.SamplesIndices;
             
        
                double n = sample.Count / 2;
                int len = sample.Count - 1;
                for (int i = 0; i < Math.Truncate(n); ++i)
                {
                    float tmp = sample[i];
                    sample[i] = sample[len-i];
                    sample[len - i] = tmp;

                 //int tmp2 =- indices[i];
                 //indices[i] =- indices[len - i];
                 //indices[len - i] = tmp2;
                 //indices[i] *= -1;
                }

     

            OutputFoldedSignal = new Signal(sample, false);
            OutputFoldedSignal.SamplesIndices = indices;
        }
    }
}
