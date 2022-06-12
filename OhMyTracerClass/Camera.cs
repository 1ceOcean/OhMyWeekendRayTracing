namespace OhMyTinyRayTrace.OhMyTracerClass
{
    using point3 = Vec3;
    internal class Camera
    {
        public Camera(point3 lookfrom, point3 lookat, Vec3 vup, double vfov, double aspectRatio, double aperture, double focusDist) 
        {
            double theta = OhMyConvert.ConvertToRadians(vfov);
            double h = Math.Tan(theta / 2);
            double viewportHeight = 2.0 * h;
            double viewportWidth = aspectRatio * viewportHeight;

            w = Vec3.UnitVector(lookfrom - lookat);
            u = Vec3.UnitVector(Vec3.Cross(vup, w));
            v = Vec3.Cross(w, u);

            origin = lookfrom;
            horizontal = focusDist * viewportWidth * u;
            vertical = focusDist * viewportHeight * v;
            lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - focusDist * w;
            lensRadius = aperture / 2;
        }

        public Ray GetRay(double u, double v) 
        {
            Vec3 rd = lensRadius * Vec3.RandomInUnitDisk();
            Vec3 offset = rd.X() * this.u + rd.Y() * this.v;
            var direction = lowerLeftCorner + u * horizontal + v * vertical - origin - offset;
            return new Ray(origin + offset, direction);
        }

        private point3 origin;
        private point3 lowerLeftCorner;
        private Vec3 horizontal;
        private Vec3 vertical;
        private Vec3 u, v, w;
        double lensRadius;

    }
}
