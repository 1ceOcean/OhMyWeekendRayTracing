using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyTinyRayTrace.OhMyTracerClass
{
    using point3 = Vec3;
    internal class Sphere : IHittable
    {
        public Sphere() { }

        public Sphere(point3 center, double radius) 
        {
            this.center = center;
            this.radius = radius;
        }

        public bool hit(Ray ray, double tMin, double tMax,ref HitRecord record)
        {
            Vec3 oc = ray.GetOrigin() - center;
            var a = ray.GetDirection().LengthSquared();
            var halfB = Vec3.Dot(oc, ray.GetDirection());
            var c = oc.LengthSquared() - radius * radius;
            var discriminant = halfB * halfB - a * c;

            if (discriminant < 0) return false;

            var sqrtd = Math.Sqrt(discriminant);

            var root = (-halfB - sqrtd) / a;

            if (root < tMin || tMax < root) 
            {
                root = (-halfB + sqrtd) / a;

                if (root < tMin || tMax < root) 
                {
                    return false;
                }
            }

            record.t = root;
            record.p = ray.At(root);
            var ourwardNormal = (record.p - center) / radius;
            record.SetFaceNormal(ray, ourwardNormal);
            return true;
        }


        public point3 center = new point3(0,0,0);
        public double radius;
    }
}
