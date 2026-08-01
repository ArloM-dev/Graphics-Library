using System.Drawing;
using System.Numerics;

namespace Graphics_Library.Objects
{
    public class Mesh
    {
        public Vector3[] points;
        public int[] index;
        public Color[] colours;
        public Mesh(Vector3[] points, int[] index, Color[] colours)
        {
            this.points = points;
            this.index = index;
            this.colours = colours;
        }
    }
}