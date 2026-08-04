using System.Drawing;
using System.Numerics;

namespace Graphics_Library.Objects
{
    public class Mesh
    {
        public Vector3[] points {get; set;}
        public int[][] index {get; set;}
        public Mesh(Vector3[] points, int[][] index)
        {
            this.points = points;
            this.index = index;
        }
    }

    public class MeshObject
    {
        public Mesh? mesh;
        public Vector3 position;
        public Vector3 rotation;
        public float scale;
        public Color[]? colours;
    }
}