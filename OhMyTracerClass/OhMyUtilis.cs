using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyTinyRayTrace.OhMyTracerClass
{
    internal class OhMyUtilis
    {
        public static Random random = new Random((int)DateTime.UtcNow.Ticks);

        public static double RandomDoule() 
        {
            return random.NextDouble();
        }

        public static double RandomDoule(double min, double max)
        {
            return (max - min) * random.NextDouble() + min;
        }

    }
}
