using System.Drawing;
using System.Numerics;
using Graphics_Library.Objects;

namespace Graphics_Library.Rendering
{
    public static class Rasterizer
    {
        public static Canvas DrawPixel(Vector2 pixel, Color colour, Canvas canvas)
        {
            uint drawpixel =
            (uint)((colour.R << 16) |
            (colour.G << 8) |
            colour.B);
            canvas.frameBuffer[(int)(pixel.Y * canvas.Width + pixel.X)] = drawpixel;
            return canvas;
        }
        public static Canvas DrawTriangle(Triangle triangle, Canvas canvas)
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
                    bool allZero = (e1 == 0) && (e2 == 0) && (e3 == 0);
                    if (allNegative || allPositive || allZero)
                    {
                        canvas = DrawPixel(new Vector2(x,y),triangle.colour, canvas);
                    }
                }
            }
            return canvas;

        }

        public static Canvas DrawMesh(Mesh mesh, Canvas canvas)
        {
            Triangle[] triangles = new Triangle[12];
            float[] depths = new float[12];
            for (int i = 0; i < mesh.index.Length-2; i+=3)
            {
                Vector3 p1 = mesh.points[mesh.index[i]];
                Vector3 p2 = mesh.points[mesh.index[i+1]];
                Vector3 p3 = mesh.points[mesh.index[i+2]];
                float depth = (p1.Z + p2.Z + p3.Z) / 3;
                depths[i/3] = depth;
                Triangle triangle = new Triangle(new Vector2(p1.X,p1.Y),new Vector2(p2.X,p2.Y),new Vector2(p3.X,p3.Y),mesh.colours[i/3]);
                triangles[i/3] = triangle;
            }
            triangles = OrderTriangles(triangles,depths);
            foreach (Triangle triangle in triangles)
            {
                canvas = DrawTriangle(triangle,canvas);
            }
            return canvas;
        }

        private static Triangle[] OrderTriangles(Triangle[] triangles, float[] depths)
        {
            bool unsorted = true;
            while (unsorted)
            {
                int count = 0;
                for (int i = 0; i < 11; i++)
                {
                    if (depths[i] > depths[i+1])
                    {
                        float temp = depths[i];
                        depths[i] = depths[i+1];
                        depths[i+1] = temp;
                        Triangle temp2 = triangles[i];
                        triangles[i] = triangles[i+1];
                        triangles[i+1] = temp2;
                        count = 0;
                    }
                    else if (count == 10)
                    {
                        unsorted = false;
                    }
                    else
                    {
                        count += 1;
                    }
                }
            }
            return triangles;
        }
    }
}