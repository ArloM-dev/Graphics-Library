using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Drawing;
using SDL3;

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

                while (SDL.PollEvent(out var e))
                {
                    // Cast raw event type to the explicit SDL EventType enum
                    if ((SDL.EventType)e.Type == SDL.EventType.Quit)
                    {
                        running = false;
                    }
                }

                Triangle testtriangle = new Triangle(new Vector2(100,100),new Vector2(300,600),new Vector2(500,500),Color.FromArgb(255,100,100));
                canvas = Rasterizer.DrawTriangle(testtriangle,canvas);
                
                canvas.UpdateCanvas();
                frame += 1;
            }
            canvas.DestroyCanvas();
        }
    }
}