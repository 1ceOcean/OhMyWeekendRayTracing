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
        public Metal(Color color) 
        { 
            albedo = color;
        }
        
        public bool Scatter(Ray rayIn, ref HitRecord record, ref Color attenuation, ref Ray scattred)
        {
            Vec3 reflected = Vec3.Reflect(Vec3.UnitVector(rayIn.GetDirection()), record.normal);
            scattred = new Ray(record.p, reflected);
            attenuation = albedo;
            return (Vec3.Dot(scattred.GetDirection(), record.normal) > 0);

        }

        public Color albedo;
    }
}
