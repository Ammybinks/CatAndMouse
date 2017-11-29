using System;

namespace CatAndMouse
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CatAndMouse game = new CatAndMouse())
            {
                game.Run();
            }
        }
    }
}

