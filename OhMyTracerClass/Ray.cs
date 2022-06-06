namespace OhMyTinyRayTrace.OhMyTracerClass
{
    using OhMyTinyRayTrace.OhMyTrancerInterface;
    using System;
    using point3 = Vec3;

    internal class Ray
    {
        public Ray() { }

        /// <summary>
        /// 使用起点和方向向量创建射线
        /// </summary>
        /// <param name="origin">原点</param>
        /// <param name="direction">方向向量</param>
        public Ray(point3 origin, Vec3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        /// <summary>
        /// 返回该射线的原点
        /// </summary>
        /// <returns></returns>
        public point3 GetOrigin() { return Origin; }

        /// <summary>
        /// 返回该射线方向向量
        /// </summary>
        /// <returns></returns>
        public Vec3 GetDirection() { return Direction; }

        /// <summary>
        /// 返回空间直线参数式中参数为t时的点坐标
        /// </summary>
        /// <param name="t">参数值，可为负数</param>
        /// <returns></returns>
        public point3 At(double t) 
        {
            return Origin + t * Direction;
        }

        public Color RayColor(Ray ray, IHittable world,int depth)
        {
            HitRecord record = new HitRecord();

            if (depth <= 0) 
            {
                return new Color(0, 0, 0);
            }

            if (world.hit(ray, 0.001, double.PositiveInfinity, ref record)) 
            {
                Ray scattered = new();
                Color attenuation = new();
                if (record.material.Scatter(ray, ref record, ref attenuation, ref scattered)) 
                {
                    return attenuation * RayColor(scattered, world, depth - 1);
                }

                return new Color(0, 0, 0);
            }
 
            Vec3 unitDirection = Vec3.UnitVector(Direction);
            var t = 0.5 * (unitDirection.Y() + 1.0);
            Vec3 value = (1.0 - t) * new Vec3(1.0, 1.0, 1.0) + t * new Vec3(0.5, 0.7, 1.0);
            return OhMyConvert.ConvertToColor(value);
        }

        public point3 Origin = new point3(0,0,0);
        public Vec3 Direction = new Vec3(0,0,0);
    }
}
