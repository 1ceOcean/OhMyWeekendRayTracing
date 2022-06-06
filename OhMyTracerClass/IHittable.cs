using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyTinyRayTrace.OhMyTracerClass
{
    using point3 = Vec3;
    struct HitRecord
    {
        public point3 p;
        public Vec3 normal;
        public double t;
        public bool frontFace;

        public void SetFaceNormal(Ray ray,Vec3 outwardNormal) 
        {
            frontFace = Vec3.Dot(ray.GetDirection(), outwardNormal) < 0;
            normal = frontFace ? outwardNormal : -outwardNormal;
        }
    }

    internal interface IHittable
    {
        public bool hit(Ray ray,double tMin,double tMax,ref HitRecord record);
    }
}
