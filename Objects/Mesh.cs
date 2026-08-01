using System.Numerics;

namespace Graphics_Library.Objects
{
    public class Mesh
    {
        public Vector3[] points;
        public int[] index;
        public Mesh(Vector3[] points, int[] index)
        {
            this.points = points;
            this.index = index;
        }
    }
}