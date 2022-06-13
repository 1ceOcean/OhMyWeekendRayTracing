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
            const double aspectRatio = 3.0 / 2.0;
            const int imageWidth = 1200;
            int imageHeight = Convert.ToInt32(imageWidth / aspectRatio);
            const int samplePerPixel = 500;
            const int maxDepth = 50;

            //World
            var world = RandomSence();

            //Camera
            point3 lookfrom = new point3(13, 2, 3);
            point3 lookat = new point3(0, 0, 0);
            Vec3 vup = new Vec3(0, 1, 0);
            var distToFocus = 10.0;
            var aperture = 0.1;
            Camera camera = new Camera(lookfrom, lookat, vup, 20.0, aspectRatio, aperture, distToFocus);

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


        public static HittableList RandomSence() 
        {
            HittableList World = new HittableList();

            var groundMaterial = new Lambertian(new OhMyTracerClass.Color(0.5, 0.5, 0.5));

            World.Add(new Sphere(new point3(0, -1000, 0), 1000, groundMaterial));

            for (int a = -11; a < 11; a++)
            {
                for (int b = -11; b < 11; b++)
                {
                    var chooseMatrial = OhMyUtilis.RandomDoule();
                    point3 center = new point3(a + 0.9 * OhMyUtilis.RandomDoule(), 0.2, b + OhMyUtilis.RandomDoule());

                    if ((center - new point3(4, 0.2, 0)).Length() > 0.9) 
                    {
                        OhMyTrancerInterface.IMaterial sphereMatrial;

                        if (chooseMatrial < 0.8)
                        {
                            var albedo = OhMyTracerClass.Color.RandomColor() * OhMyTracerClass.Color.RandomColor();
                            sphereMatrial = new Lambertian(albedo);
                            World.Add(new Sphere(center, 0.2, sphereMatrial));
                        }
                        else if (chooseMatrial < 0.95)
                        {
                            var albedo = OhMyTracerClass.Color.RandomColor(0.5, 1);
                            var fuzz = OhMyUtilis.RandomDoule(0, 0.5);
                            sphereMatrial = new Metal(albedo, fuzz);
                            World.Add(new Sphere(center, 0.2, sphereMatrial));
                        }
                        else 
                        {
                            sphereMatrial = new Dielectric(1.5);
                            World.Add(new Sphere(center, 0.2, sphereMatrial));
                        }
                    }
                }
            }

            var material1 = new Dielectric(1.5);
            World.Add(new Sphere(new point3(0, 1, 0), 1.0, material1));

            var material2 = new Lambertian(new OhMyTracerClass.Color(0.4, 0.2, 0.1));
            World.Add(new Sphere(new point3(-4, 1, 0), 1.0, material2));

            var material3 = new Metal(new OhMyTracerClass.Color(0.7, 0.6, 0.5), 0.0);
            World.Add(new Sphere(new point3(4, 1, 0), 1.0, material3));

            return World;
        } 
    }
}
