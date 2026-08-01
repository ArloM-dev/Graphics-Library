using System.Numerics;
using Graphics_Library.Rendering;
using Graphics_Library.Objects;
using SDL3;
using System.Collections;

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
            int frame = 0;
            double rotz = 0;
            double rotx = 0;
            double roty = 0;
            while (running)
            {
            while (SDL.PollEvent(out var e))
            {
                // Cast raw event type to the explicit SDL EventType enum
                if ((SDL.EventType)e.Type == SDL.EventType.Quit)
                {
                    running = false;
                    return;
                }
                else if ((SDL.EventType)e.Type == SDL.EventType.KeyDown)
                    {
                        switch (e.Key.Scancode)
                        {
                            case SDL.Scancode.A:
                            rotz -= 0.01;
                            break;
                            case SDL.Scancode.D:
                            rotz += 0.05;
                            break;
                            case SDL.Scancode.W:
                            rotx += 0.05;
                            break;
                            case SDL.Scancode.S:
                            rotx -= 0.05;
                            break;
                            case SDL.Scancode.E:
                            roty -= 0.05;
                            break;
                            case SDL.Scancode.R:
                            rotx += 0.05;
                            break;
                        }
                    }
            }                
                canvas.ClearCanvas();
                Cube cube = new Cube(100, new Vector3(300,300,300),new Vector3((float)rotx,(float)roty,(float)rotz));
                Rasterizer.DrawMesh(cube.cubeMesh, canvas);
                
                canvas.UpdateCanvas();
                frame += 1;
            }
            canvas.DestroyCanvas();
        }
    }
}