using OhMyTinyRayTrace.OhMyTracerClass;

namespace OhMyTinyRayTrace.OhMyTrancerInterface
{
    using point3 = Vec3;
    struct HitRecord
    {
        public point3 p;
        public point3 normal;
        public IMaterial material;
        public double t;
        public bool frontFace;

        public void SetFaceNormal(Ray ray, point3 outwardNormal)
        {
            frontFace = point3.Dot(ray.GetDirection(), outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }

    internal interface IHittable
    {
        public bool hit(Ray ray, double tMin, double tMax, ref HitRecord record);
    }
}
