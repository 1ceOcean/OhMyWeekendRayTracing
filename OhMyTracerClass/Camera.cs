using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhMyTinyRayTrace.OhMyTracerClass
{
    using point3 = Vec3;
    internal class Camera
    {
        public Camera() 
        {
            double aspectRatio = 16.0 / 9.0;
            double viewportHeight = 2.0;
            double viewportWidth = aspectRatio * viewportHeight;
            double focalLength = 1.0;

            origin = new point3(0, 0, 0);
            horizontal = new Vec3(viewportWidth, 0, 0);
            vertical = new Vec3(0, viewportHeight, 0);
            lowerLeftCorner = origin - horizontal / 2 - vertical / 2 - new Vec3(0, 0, focalLength);
        }

        public Ray GetRay(double u, double v) 
        {
            var direction = lowerLeftCorner + u * horizontal + v * vertical - origin;
            return new Ray(origin, direction);
        }

        private point3 origin;
        private point3 lowerLeftCorner;
        private Vec3 horizontal;
        private Vec3 vertical;

    }
}
