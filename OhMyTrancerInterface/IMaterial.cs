using OhMyTinyRayTrace.OhMyTracerClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyTinyRayTrace.OhMyTrancerInterface
{
    internal interface IMaterial
    {
        public bool Scatter(Ray rayIn, ref HitRecord record,ref Color attenuation,ref Ray scattred);
    }
}
