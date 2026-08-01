using System.Numerics;

namespace Graphics_Library.Objects
{
    public class Cube
    {
        Vector3[] points;
        int sideLength;
        Vector3 center;
        Vector3 rotation;
        Mesh cubeMesh;
        public Cube(int sideLength, Vector3 center, Vector3 rotation)
        {
            this.sideLength = sideLength;
            this.center = center;
            this.rotation = rotation;
            Vector3[] originalPoints = [
                center + new Vector3(sideLength/2, sideLength/2, sideLength/2),
                center + new Vector3(-sideLength/2, sideLength/2, sideLength/2),
                center + new Vector3(sideLength/2, -sideLength/2, sideLength/2),
                center + new Vector3(sideLength/2, sideLength/2, -sideLength/2),
                center + new Vector3(-sideLength/2, -sideLength/2, sideLength/2),
                center + new Vector3(-sideLength/2, sideLength/2, -sideLength/2),
                center + new Vector3(sideLength/2, -sideLength/2, -sideLength/2),
                center + new Vector3(-sideLength/2, -sideLength/2, -sideLength/2),
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
            points = rotatedPoints;
            cubeMesh = new Mesh(points);
        }
    }
}