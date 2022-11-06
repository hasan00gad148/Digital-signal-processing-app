using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{

    class varying_the_sampling_rate : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public int l { get; set; }
        public int m { get; set; }
        public Signal OutputYn { get; set; }
        public override void Run()
        {
           
        }
    }
    
}
