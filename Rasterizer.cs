using System.Drawing;
using System.Numerics;

namespace Graphics_Library
{
    public static class Rasterizer
    {
        public static uint[] DrawPixel(Vector2 pixel, Color colour, uint[] pixels)
        {
            uint drawpixel =
            (uint)((colour.R << 16) |
            (colour.G << 8) |
            colour.B);
            pixels[(int)(pixel.Y * Program.Width + pixel.X)] = drawpixel;
            return pixels;
        }
        public static uint[] DrawTriangle(Triangle triangle, uint[] pixels)
        {
            int minX = (int)Math.Min(triangle.p1.X, Math.Min(triangle.p2.X, triangle.p3.X));
            int minY = (int)Math.Min(triangle.p1.Y, Math.Min(triangle.p2.Y, triangle.p3.Y));
            int maxX = (int)Math.Max(triangle.p1.X, Math.Max(triangle.p2.X, triangle.p3.X));
            int maxY = (int)Math.Max(triangle.p1.Y, Math.Max(triangle.p2.Y, triangle.p3.Y));

            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                
                    // Edge 1: AB
                    double e1 = (x - triangle.p1.X) * (triangle.p2.Y - triangle.p1.Y) - (y - triangle.p1.Y) * (triangle.p2.X - triangle.p1.X);
                    
                    // Edge 2: BC
                    double e2 = (x - triangle.p2.X) * (triangle.p3.Y - triangle.p2.Y) - (y - triangle.p2.Y) * (triangle.p3.X - triangle.p2.X);
                    
                    // Edge 3: CA
                    double e3 = (x - triangle.p3.X) * (triangle.p1.Y - triangle.p3.Y) - (y - triangle.p3.Y) * (triangle.p1.X - triangle.p3.X);

                    // True if all signs match (all positive or all negative)
                    bool allPositive = e1 >= 0 && e2 >= 0 && e3 >= 0;
                    bool allNegative = e1 <= 0 && e2 <= 0 && e3 <= 0;
                    if (allNegative || allPositive)
                    {
                        pixels = DrawPixel(new Vector2(x,y),triangle.colour, pixels);
                    }
                }
            }
            return pixels;

        }

        public static uint[] DrawMesh(Mesh mesh, uint[] pixels)
        {
            return pixels;
        }
    }
}