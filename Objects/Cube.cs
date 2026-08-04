using System.Drawing;
using System.Numerics;


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

            MeshFromJson("Data/Meshes/cube.json");

            colours = [
                Color.Red, Color.Orange,
                Color.Green, Color.White,
                Color.Blue, Color.MediumTurquoise,
                Color.Yellow, Color.DimGray,
                Color.Purple, Color.DarkKhaki,
                Color.Brown, Color.MediumAquamarine
            ];
        }
    }
}