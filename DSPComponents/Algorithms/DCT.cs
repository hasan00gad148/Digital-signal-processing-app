using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            OutputSignal = new Signal(new List<float>(), false);

            int len = InputSignal.Samples.Count;
            for (int k = 0; k <len; ++k)
            {
                int tmp ;

                if (k > 0)tmp = 2;
                else tmp = 1;

                double tmp3 = (double)tmp / (double)len;
                double a= Math.Sqrt(tmp3);
                double tmp2 = 0;

                for (int n = 0; n < len; ++n)
                {
                    tmp2 += InputSignal.Samples[n] * Math.Cos((2 * n + 1) * k * Math.PI / (2 * len));
                }
                OutputSignal.Samples.Add((float)(a*tmp2));
            }

        }
    }
}
