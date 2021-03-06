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

        public static Color RandomColor() 
        {
            return new Color(OhMyUtilis.RandomDoule(0, 1), OhMyUtilis.RandomDoule(0, 1), OhMyUtilis.RandomDoule(0, 1));
        }

        public static Color RandomColor(double min, double max)
        {
            return new Color(OhMyUtilis.RandomDoule(min, max), OhMyUtilis.RandomDoule(min, max), OhMyUtilis.RandomDoule(min, max));
        }

        public Color() { }

        public Color(double e0, double e1, double e2) : base(e0, e1, e2) { }

        public static Color operator +(Color color1, Color color2) 
        {
            return new Color(color1.e[0] + color2.e[0], color1.e[1] + color2.e[1], color1.e[2] + color2.e[2]);
        }

        public static Color operator *(Color color1, Color color2) 
        {

            return new Color(color1.e[0] * color2.e[0], color1.e[1] * color2.e[1], color1.e[2] * color2.e[2]);
        
        }

    }

}
