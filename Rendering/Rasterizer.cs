using System.Drawing;
using System.Numerics;
using Graphics_Library.Objects;

namespace Graphics_Library.Rendering
{
    public static class Rasterizer
    {
        public static void DrawPixel(Vector2 pixel, Color colour, Canvas canvas)
        {
            uint drawpixel =
            (uint)((colour.R << 16) |
            (colour.G << 8) |
            colour.B);
            if ((pixel.X < canvas.Width) & (pixel.Y < canvas.Height) & (pixel.X > 0) & (pixel.Y > 0))
            {
                canvas.frameBuffer[(int)(pixel.Y * canvas.Width + pixel.X)] = drawpixel;
            }
        }
        public static void DrawTriangle(Triangle triangle, Canvas canvas)
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
                        DrawPixel(new Vector2(x,y),triangle.colour, canvas);
                    }
                }
            }
        }

        public static void DrawMesh(MeshObject meshobject, Canvas canvas)
        {
            Triangle[] triangles = new Triangle[12];
            float[] depths = new float[12];
            Vector3[] points = GetPoints(meshobject);
            for (int i = 0; i < meshobject.mesh?.index.Length; i+=1)
            {
                Vector3 p1 = points[meshobject.mesh.index[i][0]];
                Vector3 p2 = points[meshobject.mesh.index[i][1]];
                Vector3 p3 = points[meshobject.mesh.index[i][2]];
                float depth = (p1.Z + p2.Z + p3.Z) / 3;
                depths[i] = depth;
                Triangle triangle = new Triangle(new Vector2(p1.X,p1.Y),new Vector2(p2.X,p2.Y),new Vector2(p3.X,p3.Y),meshobject.colours?[i] ?? Color.Purple);
                triangles[i] = triangle;
            }
            triangles = OrderTriangles(triangles,depths);
            foreach (Triangle triangle in triangles)
            {
                DrawTriangle(triangle,canvas);
            }
            //return canvas;
        }

        private static Vector3[] GetPoints(MeshObject meshobject)
        {
            Vector3[] points = new Vector3[8];
            for (int i = 0; i < 8; i++)
            {
                Vector3 point = meshobject.mesh?.points[i] ?? new Vector3();

                // X rotation
                double y1 = (point.Y * Math.Cos(meshobject.rotation.X)) - (point.Z * Math.Sin(meshobject.rotation.X));
                double z1 = (point.Y * Math.Sin(meshobject.rotation.X)) + (point.Z * Math.Cos(meshobject.rotation.X));
                // Y rotation
                double x1 = (point.X * Math.Cos(meshobject.rotation.Y)) + (z1 * Math.Sin(meshobject.rotation.Y));
                double z2 = (-point.X * Math.Sin(meshobject.rotation.Y)) + (z1 * Math.Cos(meshobject.rotation.Y));
                // Z rotation
                double x2 = (x1 * Math.Cos(meshobject.rotation.Z)) - (y1 * Math.Sin(meshobject.rotation.Z));
                double y2 = (x1 * Math.Sin(meshobject.rotation.Z)) + (y1 * Math.Cos(meshobject.rotation.Z));
                
                points[i] = (new Vector3((float)x2, (float)y2, (float)z2) * meshobject.scale) + meshobject.position;
            }
            return points;
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