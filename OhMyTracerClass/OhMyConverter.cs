namespace OhMyTinyRayTrace.OhMyTracerClass
{
    internal class OhMyConvert
    {
        public static Color ConvertToColor(Vec3 value) 
        {
            return new Color(value.e[0], value.e[1], value.e[2]);
        }

        public static double ConvertToRadians(double degrees) 
        {
            return degrees * Math.PI / 180.0;
        }
    }
   
}
