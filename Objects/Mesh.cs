using System.Drawing;
using System.Numerics;
using Newtonsoft.Json;

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
        public Mesh mesh = new Mesh(new Vector3[1], new int[1][]);
        public Vector3 position;
        public Vector3 rotation;
        public float scale;
        public Color[]? colours;

        public void MeshFromJson(string path)
        {
            string appPath = AppContext.BaseDirectory;
            string data = File.ReadAllText(appPath + path);
            mesh = JsonConvert.DeserializeObject<Mesh>(data) ?? mesh;
        }
    }
}