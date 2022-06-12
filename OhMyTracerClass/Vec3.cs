namespace OhMyTinyRayTrace.OhMyTracerClass
{
    internal class Vec3
    {
        /// <summary>
        /// 使用三个分量构造向量
        /// </summary>
        /// <param name="e0">x分量</param>
        /// <param name="e1">y分量</param>
        /// <param name="e2">z分量</param>
        public Vec3(double e0, double e1, double e2) 
        {
            e[0] = e0;
            e[1] = e1;
            e[2] = e2;
        }

        protected Vec3() { }

        /// <summary>
        /// 返回向量的x分量
        /// </summary>
        /// <returns></returns>
        public double X() { return e[0]; }

        /// <summary>
        /// 返回向量的y分量
        /// </summary>
        /// <returns></returns>
        public double Y() { return e[1]; }

        /// <summary>
        /// 返回向量的z分量
        /// </summary>
        /// <returns></returns>
        public double Z() { return e[2]; }

        /// <summary>
        /// 返回两个向量的点积
        /// </summary>
        /// <param name="vec1"></param>
        /// <param name="vec2"></param>
        /// <returns></returns>
        public static double Dot(Vec3 vec1,Vec3 vec2) 
        {
            return vec1.e[0] * vec2.e[0] +
                   vec1.e[1] * vec2.e[1] +
                   vec1.e[2] * vec2.e[2];
        }

        public static Vec3 Cross(Vec3 vec1,Vec3 vec2) 
        {
            return new Vec3(vec1.e[1] * vec2.e[2] - vec1.e[2] * vec2.e[1],
                            vec1.e[2] * vec2.e[0] - vec1.e[0] * vec2.e[2],
                            vec1.e[0] * vec2.e[1] - vec1.e[1] * vec2.e[0]);
        }

        /// <summary>
        /// 返回向量的模长的平方
        /// </summary>
        /// <returns></returns>
        public double LengthSquared()
        {
            return e[0] * e[0] + e[1] * e[1] + e[2] * e[2];
        }

        /// <summary>
        /// 返回向量模长
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(LengthSquared());
        }

        public double this[int i] 
        {
            get => e[i];
            set => e[i] = value;
        }

        /// <summary>
        /// 返回该向量方向的单位向量
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vec3 UnitVector(Vec3 vec) 
        {
            return vec / vec.Length();
        }

        public static Vec3 Random() 
        {
            return new Vec3(OhMyUtilis.RandomDoule(), OhMyUtilis.RandomDoule(), OhMyUtilis.RandomDoule());
        }

        public static Vec3 Random(double min, double max) 
        {
            return new Vec3(OhMyUtilis.RandomDoule(min, max), OhMyUtilis.RandomDoule(min, max), OhMyUtilis.RandomDoule(min, max));
        }

        public static Vec3 RandomInUnitSphere() 
        {
            while (true) 
            {
                var p = Random(-1, 1);
                if (p.Length() >= 1) continue;
                return p;
            }
        }

        public static Vec3 RandomUnitVector() 
        {
            return UnitVector(RandomInUnitSphere());
        }

        public static Vec3 RandomInHemisphere(Vec3 normal) 
        {
            Vec3 inUnitSphere = RandomInUnitSphere();
            if (Dot(inUnitSphere, normal) > 0.0)
            {
                return inUnitSphere;
            }
            else 
            {
                return -inUnitSphere; 
            }
        }

        public static Vec3 RandomInUnitDisk() 
        {
            while (true) 
            {
                var p = new Vec3(OhMyUtilis.RandomDoule(-1, 1), OhMyUtilis.RandomDoule(-1, 1), 0);
                if (p.LengthSquared() >= 1) 
                {
                    continue;
                }
                return p;
            }
        }

        public bool NearZero() 
        {
            const double s = 1e-8;
            return (Math.Abs(e[0]) < s && Math.Abs(e[1]) < s && Math.Abs(e[2]) < s);
        }

        public static Vec3 Reflect(Vec3 vec , Vec3 normal) 
        {
            return vec - 2 * Dot(vec, normal) * normal;
        }

        public static Vec3 Refract(Vec3 uv, Vec3 n, double etaiOverEtat) 
        {
            var cosTheta = Math.Min(Dot(-uv, n), 1.0);
            Vec3 rOutPerp = etaiOverEtat * (uv + cosTheta * n);
            Vec3 rOutParallel = -Math.Sqrt(Math.Abs(1.0 - rOutPerp.LengthSquared())) * n;
            return rOutPerp + rOutParallel;
        }

        public static Vec3 operator +(Vec3 vec1,Vec3 vec2) 
        {
            Vec3 vec = new Vec3();
            vec[0] = vec1[0] + vec2[0];
            vec[1] = vec1[1] + vec2[1];
            vec[2] = vec1[2] + vec2[2]; 
            return vec;
        }

        public static Vec3 operator -(Vec3 vec)
        {
            return new Vec3(-vec[0], -vec[1], -vec[2]);
        }

        public static Vec3 operator -(Vec3 vec1, Vec3 vec2)
        {
            return new Vec3(vec1[0] - vec2[0], vec1[1] - vec2[1], vec1[2] - vec2[2]);
        }

        public static Vec3 operator *(double t,Vec3 vec) 
        {
            return new Vec3(t * vec[0], t * vec[1], t * vec[2]);
        }

        public static Vec3 operator *(Vec3 vec1, Vec3 vec2) 
        {
            return new Vec3(vec1.e[0] * vec2.e[0], vec1.e[1] * vec2.e[1], vec1.e[2] * vec2.e[2]);
        }

        public static Vec3 operator /(Vec3 vec, double t) 
        {
            return (1/t) * vec;
        }

        public override string ToString()
        {
            return $"{this[0]} {this[1]} {this[2]}";
        }

        public double[] e = new double[3];
    }
}
