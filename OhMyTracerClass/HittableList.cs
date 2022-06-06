using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyTinyRayTrace.OhMyTracerClass
{
    internal class HittableList : IHittable
    {
        public HittableList() { }
        public HittableList(IHittable hittable) 
        {
            Add(hittable);
        }

        public void Clear() 
        {
            objects.Clear();
        }

        public void Add(IHittable hittable) 
        {
            objects.Add(hittable);
        }

        public List<IHittable> objects = new List<IHittable>();

        public bool hit(Ray ray, double tMin, double tMax, ref HitRecord record)
        {
            HitRecord tempRecord = new HitRecord();
            bool hitAnything = false;
            var closestSoFar = tMax;

            foreach(var obj in objects) 
            {
                if (obj.hit(ray, tMin, closestSoFar, ref tempRecord)) 
                {
                    hitAnything = true;
                    closestSoFar = tempRecord.t;
                    record = tempRecord;
                }
            }

            return hitAnything;
        }
    }
}
