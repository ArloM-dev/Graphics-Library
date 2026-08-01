using System.Numerics;

namespace Graphics_Library
{
    public class Mesh
    {
        public Vector3[] points;
        public Mesh(params Vector3[] points)
        {
            this.points = points;
        }
    }
}