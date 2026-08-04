using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Newtonsoft.Json;

namespace Graphics_Library.Objects
{
    public class Cube : MeshObject
    {
        Vector3[] points = new Vector3[8];
        public Cube(float scale, Vector3 position, Vector3 rotation)
        {
            this.scale = scale;
            this.position = position;
            this.rotation = rotation;

            string data = File.ReadAllText("/home/arlo/Projects/Graphics Library/Objects/Meshes/cube.json");
            mesh = JsonConvert.DeserializeObject<Mesh>(data);

            colours = [
                Color.Red, Color.Red,
                Color.Green, Color.Green,
                Color.Blue, Color.Blue,
                Color.Yellow, Color.Yellow,
                Color.Purple, Color.Purple,
                Color.Brown, Color.Brown
            ];
        }
    }
}