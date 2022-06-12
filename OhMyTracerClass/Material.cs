using OhMyTinyRayTrace.OhMyTrancerInterface;

namespace OhMyTinyRayTrace.OhMyTracerClass
{
    internal class Lambertian : IMaterial
    {
        public Lambertian(Color color) 
        {
            albedo = color;
        }
        
        public bool Scatter(Ray rayIn, ref HitRecord record,ref Color attenuation,ref Ray scattred)
        {
            var scatterDirection = record.normal + Vec3.RandomUnitVector();

            if (scatterDirection.NearZero())
            {
                scatterDirection = record.normal;
            }

            scattred = new Ray(record.p,scatterDirection);
            attenuation = albedo;
            return true;
        }

        public Color albedo;
    }

    internal class Metal : IMaterial
    {
        public Metal(Color color,double f) 
        { 
            albedo = color;
            fuzz = f < 1 ? f : 1;
        }
        
        public bool Scatter(Ray rayIn, ref HitRecord record, ref Color attenuation, ref Ray scattred)
        {
            Vec3 reflected = Vec3.Reflect(Vec3.UnitVector(rayIn.GetDirection()), record.normal);
            scattred = new Ray(record.p, reflected + fuzz * Vec3.RandomInUnitSphere());
            attenuation = albedo;
            return (Vec3.Dot(scattred.GetDirection(), record.normal) > 0);

        }

        public Color albedo;
        public double fuzz;
    }

    internal class Dielectric : IMaterial
    {
        public Dielectric(double indexOfraction) 
        {
            ir = indexOfraction;
        }
        
        public bool Scatter(Ray rayIn, ref HitRecord record, ref Color attenuation, ref Ray scattred)
        {
            attenuation = new Color(1.0, 1.0, 1.0);
            double refractionRatio = record.frontFace ? 1.0 / ir : ir;

            Vec3 unitDirection = Vec3.UnitVector(rayIn.GetDirection());
            var cosTheta = Math.Min(Vec3.Dot(-unitDirection,record.normal), 1.0);
            var sinTheta = Math.Sqrt(1.0 - cosTheta * cosTheta);

            bool cannotRefract = refractionRatio * sinTheta > 1.0;

            Vec3 direction;

            if (cannotRefract || Reflectance(cosTheta,refractionRatio) > OhMyUtilis.RandomDoule())
            {
                direction = Vec3.Reflect(unitDirection, record.normal);
            }
            else 
            {
                direction = Vec3.Refract(unitDirection, record.normal,refractionRatio);
            }

            scattred = new Ray(record.p, direction);
            return true;
        }

        private static double Reflectance(double cosine, double refIdx) 
        {
            var r0 = (1 - refIdx) / (1 + refIdx);
            r0 = r0 * r0;
            return r0 + (1 - r0) * Math.Pow(1 - cosine, 5);
        }

        public double ir;
    }
}
