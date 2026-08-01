using System.Drawing;
using System.Numerics;

namespace Graphics_Library.Objects
{
    public class Triangle
    {
        public Vector2 p1, p2, p3;
        public Color colour = new Color();
        public Triangle(Vector2 p1, Vector2 p2, Vector2 p3, Color colour)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.colour = colour;
        }
    }
}