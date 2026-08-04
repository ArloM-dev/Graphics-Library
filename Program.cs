using System.Numerics;
using Graphics_Library.Rendering;
using Graphics_Library.Objects;
using SDL3;
using System.Collections;
using System.Text.Json;

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
            float frame = 0;


            Cube cube = new Cube(100, new Vector3(300,300,300),new Vector3(0, (float)0.5, (float)0.5));
            while (running)
            {
                running = !canvas.CheckQuit();
                canvas.ClearCanvas();
                cube.rotation += new Vector3((float)0.01,0,0);
                Rasterizer.DrawMesh(cube, canvas);
                canvas.UpdateCanvas();
                frame += 1;
            }
            canvas.DestroyCanvas();
        }
    }
}