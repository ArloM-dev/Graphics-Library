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
            canvas.InitCanvas();

            bool running = true;
            uint frame = 0;
            while (running)
            {
                running = !canvas.CheckQuit();
                canvas.ClearCanvas();
                Cube cube = new Cube(100, new Vector3(300,300,300),new Vector3(((float)frame)/400,((float)frame)/400,0));
                Rasterizer.DrawMesh(cube.cubeMesh, canvas);
                
                canvas.UpdateCanvas();
                frame += 1;
            }
            canvas.DestroyCanvas();
        }
    }
}