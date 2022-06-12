using SixLabors.ImageSharp;
using System.Diagnostics;
using OhMyTinyRayTrace.OhMyTracerClass;

namespace OhMyTinyRayTrace
{
    using point3 = Vec3;
    class Program
    {
        static void Main(string[] args)
        {

            //Image
            const double aspectRatio = 16.0 / 9.0;
            const int imageWidth = 400;
            int imageHeight = Convert.ToInt32(imageWidth / aspectRatio);
            const int samplePerPixel = 100;
            const int maxDepth = 50;

            //World
            HittableList world = new HittableList();

            var materialGround = new Lambertian(new OhMyTracerClass.Color(0.8, 0.8, 0.0));
            var materialCenter = new Lambertian(new OhMyTracerClass.Color(0.1, 0.2, 0.5));
            //var materialLeft = new Metal(new OhMyTracerClass.Color(0.8, 0.8, 0.8), 0.3);
            var materialLeft = new Dielectric(1.5);

            var materialRight = new Metal(new OhMyTracerClass.Color(0.8, 0.6, 0.2), 0.0);

            //var materialRight = new Dielectric(1.5);


            world.Add(new Sphere(new point3(0.0, -100.5, -1.0), 100.0, materialGround));
            world.Add(new Sphere(new point3(0.0, 0.0, -1.0), 0.5, materialCenter));
            world.Add(new Sphere(new point3(-1.0, 0.0, -1.0), 0.5, materialLeft));
            world.Add(new Sphere(new point3(-1.0, 0.0, -1.0), -0.4, materialLeft));
            world.Add(new Sphere(new point3(1.0, 0.0, -1.0), 0.5, materialRight));


            //Camera
            Camera camera = new Camera();

            using (var file = new FileStream("../../../image.ppm", FileMode.Create, FileAccess.Write))
            {
                using (var fileWriter = new StreamWriter(file))
                {
                    fileWriter.WriteLine("P3");
                    fileWriter.WriteLine($"{imageWidth} {imageHeight}");
                    fileWriter.WriteLine("255");
                    Stopwatch sw = Stopwatch.StartNew();
                    sw.Start();
                    //render
                    for (int i = imageHeight - 1; i >= 0; i--)
                    {
                        for (int j = 0; j < imageWidth; j++)
                        {
                            OhMyTracerClass.Color pixelColor = new OhMyTracerClass.Color(0, 0, 0);
                            for (int k = 0; k < samplePerPixel; k++)
                            {
                                var u = Convert.ToDouble(j + OhMyUtilis.RandomDoule()) / (imageWidth - 1);
                                var v = Convert.ToDouble(i + OhMyUtilis.RandomDoule()) / (imageHeight - 1);
                                Ray ray = camera.GetRay(u, v);
                                pixelColor += ray.RayColor(ray,world,maxDepth);
                            }
                           
                            fileWriter.Write(pixelColor.WriteColor(samplePerPixel));
                        }
                        Console.WriteLine($"Line {imageHeight - i} Done.");
                        Console.SetCursorPosition(0, 0);
                    }

                    //timer
                    sw.Stop();
                    Console.SetCursorPosition(0, 1);
                    Console.WriteLine($"Done. Expense {sw.ElapsedMilliseconds} ms.");
                }
            }

            var image = Image.Load("../../../image.ppm");
            image.SaveAsPng("../../../image.png");
        }

        public static double HitSphere(point3 center, double radius,Ray ray) 
        {
            Vec3 oc = ray.GetOrigin() - center;
            var a = ray.GetDirection().LengthSquared();
            var halfB = Vec3.Dot(oc, ray.GetDirection()) ;
            var c = oc.LengthSquared() - radius * radius;
            var discriminant = halfB * halfB -  a * c;
            if (discriminant < 0)
            {
                return -1;
            }
            else 
            {
                return (-halfB - Math.Sqrt(discriminant)) / a;
            }
        }

    }
}
