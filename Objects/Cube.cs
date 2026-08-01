using System.Numerics;

namespace Graphics_Library.Objects
{
    public class Cube
    {
        Vector3[] points = new Vector3[8];
        int sideLength;
        Vector3 center;
        Vector3 rotation;
        public Mesh cubeMesh;
        public Cube(int sideLength, Vector3 center, Vector3 rotation)
        {
            this.sideLength = sideLength;
            this.center = center;
            this.rotation = rotation;
            Vector3[] originalPoints = [
                new Vector3(sideLength/2, sideLength/2, sideLength/2),
                new Vector3(-sideLength/2, sideLength/2, sideLength/2),
                new Vector3(sideLength/2, -sideLength/2, sideLength/2),
                new Vector3(-sideLength/2, -sideLength/2, sideLength/2),
                new Vector3(sideLength/2, -sideLength/2, -sideLength/2),
                new Vector3(-sideLength/2, -sideLength/2, -sideLength/2),
                new Vector3(sideLength/2, sideLength/2, -sideLength/2),
                new Vector3(-sideLength/2, sideLength/2, -sideLength/2),
            ];

            Vector3[] rotatedPoints = new Vector3[8];
            for (int i = 0; i < 8; i++)
            {
                Vector3 point = originalPoints[i];

                // X rotation
                double y1 = (point.Y * Math.Cos(rotation.X)) - (point.Z * Math.Sin(rotation.X));
                double z1 = (point.Y * Math.Sin(rotation.X)) + (point.Z * Math.Cos(rotation.X));
                // Y rotation
                double x1 = (point.X * Math.Cos(rotation.Y)) + (z1 * Math.Sin(rotation.Y));
                double z2 = (-point.X * Math.Sin(rotation.Y)) + (z1 * Math.Cos(rotation.Y));
                // Z rotation
                double x2 = (x1 * Math.Cos(rotation.Z)) - (y1 * Math.Sin(rotation.Z));
                double y2 = (x1 * Math.Sin(rotation.Z)) + (y1 * Math.Cos(rotation.Z));
                
                rotatedPoints[i] = new Vector3((float)x2, (float)y2, (float)z2);
            }

            for (int i = 0; i < 8; i++)
            {
                points[i] = center + rotatedPoints[i];
            }

            int[] index = [
                0,1,2, 1,2,3,
                2,3,4, 3,4,5,
                4,5,6, 5,6,7,
                0,1,7, 7,0,6,
                0,6,4, 0,4,2,
                1,7,5, 1,5,3
            ];

            cubeMesh = new Mesh(points, index);
        }
    }
}