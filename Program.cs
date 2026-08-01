using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Drawing;
using SDL3;

namespace Graphics_Library
{
    internal class Program
    {
        public const int Width = 800;
        const int Height = 600;
        const int Length = 2000;
        static void Main()
        {

            if (!SDL.Init(SDL.InitFlags.Video))
            {
                Console.WriteLine("first error");
                return;
            }


            if (!SDL.CreateWindowAndRenderer("Blank window", Width, Height, 0, out IntPtr window, out IntPtr renderer))
            {
                Console.WriteLine("second error");
                SDL.Quit();
                return;
            }

            IntPtr texture = SDL.CreateTexture(renderer,SDL.PixelFormat.XRGB8888,SDL.TextureAccess.Streaming,Width,Height);

            uint[] pixels = new uint[Width * Height];
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
                pixels = Rasterizer.DrawTriangle(testtriangle,pixels);
                unsafe
                    {
                        fixed (uint* pPixels = pixels)
                        {
                            SDL.UpdateTexture(
                                texture,
                                IntPtr.Zero,
                                (IntPtr)pPixels,
                                Width * sizeof(uint));
                        }
                    }
                SDL.RenderClear(renderer);   // Clears screen with the draw color
                SDL.RenderTexture(renderer,texture,IntPtr.Zero,IntPtr.Zero);
                SDL.RenderPresent(renderer);
                frame += 1;
            }
            SDL.DestroyTexture(texture);
            SDL.DestroyRenderer(renderer);
            SDL.DestroyWindow(window);
            SDL.Quit();
        }

        static Color Shader(uint x, uint y, uint frame)
        {
            int red = (int)((x / (float)Width) * 255);
            int green = (int)((y / (float)Height) * 255);
            int blue = (int)((frame / (float)Length) * 255);
            Color colour = Color.FromArgb(red,green,blue);
            
            return colour;
        }
    }
}