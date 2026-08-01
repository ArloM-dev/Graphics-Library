using SDL3;

namespace Graphics_Library.Rendering
{
    public class Canvas
    {
        // Public Variables
        public int Width, Height;
        public string name;
        public IntPtr window, renderer, texture;
        public uint[] frameBuffer;



        public Canvas(int Width, int Height, string name)
        {

            this.Width = Width;
            this.Height = Height;
            this.name = name;
            frameBuffer = new uint[Width * Height];
        }

        public void ClearCanvas()
        {
            frameBuffer = new uint[Width * Height];
        }

        public void InitCanvas()
        {
            // Safely initializes SDL
            if (!SDL.Init(SDL.InitFlags.Video))
            {
                Console.WriteLine("Failed to initialize SDL");
                return;
            }

            // Safely creates window and renderer
            if (!SDL.CreateWindowAndRenderer("Blank window", Width, Height, 0, out IntPtr window, out IntPtr renderer))
            {
                Console.WriteLine("Failed to create window or renderer");
                SDL.Quit();
                return;
            }
            this.window = window;
            this.renderer = renderer;

            // Creates texture
            texture = SDL.CreateTexture(renderer,SDL.PixelFormat.XRGB8888,SDL.TextureAccess.Streaming,Width,Height);
        }

        public bool CheckQuit()
        {
            while (SDL.PollEvent(out var e))
            {
                // Cast raw event type to the explicit SDL EventType enum
                if ((SDL.EventType)e.Type == SDL.EventType.Quit)
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateCanvas()
        {
            unsafe
                {
                    fixed (uint* pPixels = frameBuffer)
                    {
                        SDL.UpdateTexture(
                            texture,
                            IntPtr.Zero,
                            (IntPtr)pPixels,
                            Width * sizeof(uint));
                    }
                }
            SDL.RenderClear(renderer);
            SDL.RenderTexture(renderer,texture,IntPtr.Zero,IntPtr.Zero);
            SDL.RenderPresent(renderer);
        }

        public void DestroyCanvas()
        {
            SDL.DestroyTexture(texture);
            SDL.DestroyRenderer(renderer);
            SDL.DestroyWindow(window);
            SDL.Quit();
        }
    }
}