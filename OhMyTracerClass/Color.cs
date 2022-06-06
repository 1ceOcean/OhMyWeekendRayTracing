using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyTinyRayTrace.OhMyTracerClass
{
    internal class Color : Vec3
    {
        public string WriteColor(int samplesPerPixel)
        {
            var r = X();
            var g = Y();
            var b = Z();

            var scale = 1.0 / samplesPerPixel;
            r = Math.Sqrt(scale * r);
            g = Math.Sqrt(scale * g);
            b = Math.Sqrt(scale * b);

            return $"{(int)(256 * Math.Clamp(r,0.0,0.999))} {(int)(256 * Math.Clamp(g, 0.0, 0.999))} {(int)(256 * Math.Clamp(b, 0.0, 0.999))}\n";
        }

        public Color(double e0, double e1, double e2) : base(e0, e1, e2) { }

        public static Color operator +(Color color1, Color color2) 
        {
            return new Color(color1.e[0] + color2.e[0], color1.e[1] + color2.e[1], color1.e[2] + color2.e[2]);
        }
    }

}
