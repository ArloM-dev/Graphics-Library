using System.Numerics;
using System.Drawing;
using Graphics_Library.Rendering;
using Graphics_Library.Objects;

namespace Graphics_Library
{
    internal class Program
    {
        const int Length = 2000;
        static void Main()
        {
            Random random = new Random();

            Canvas canvas = new Canvas(800, 600, "mycanvas");

            bool running = true;
            uint frame = 0;
            while (running)
            {
                running = !canvas.CheckQuit();
                Vector2 p1 = new Vector2(random.Next(0,801),random.Next(0,601));
                Vector2 p2 = new Vector2(random.Next(0,801),random.Next(0,601));
                Vector2 p3 = new Vector2(random.Next(0,801),random.Next(0,601));
                Color colour = Color.FromArgb(random.Next(0,256),random.Next(0,256),random.Next(0,256));
                Triangle triangle = new Triangle(p1,p2,p3,colour);
                canvas = Rasterizer.DrawTriangle(triangle, canvas);
                
                canvas.UpdateCanvas();
                frame += 1;
            }
            canvas.DestroyCanvas();
        }
    }
}