using System.Numerics;
using System.Drawing;
using Graphics_Library.Rendering;

namespace Graphics_Library
{
    internal class Program
    {
        const int Length = 2000;
        static void Main()
        {

            Canvas canvas = new Canvas(800, 600, "mycanvas");

            bool running = true;
            uint frame = 0;
            while (running)
            {
                running = !canvas.CheckQuit();

                Objects.Triangle testtriangle = new Objects.Triangle(new Vector2(100,100),new Vector2(300,600),new Vector2(500,500),Color.FromArgb(255,100,100));
                canvas = Rasterizer.DrawTriangle(testtriangle,canvas);
                
                canvas.UpdateCanvas();
                frame += 1;
            }
            canvas.DestroyCanvas();
        }
    }
}