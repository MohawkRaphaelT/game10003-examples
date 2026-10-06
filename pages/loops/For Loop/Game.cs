// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("For Loop");
            Window.SetSize(400, 400);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            Draw.SetFillColor(255, 0, 0);

            // Loop runs 5 times
            // index = 0. index < 5? true. loop runs
            // index = 1. index < 5? true. loop runs
            // index = 2. index < 5? true. loop runs
            // index = 3. index < 5? true. loop runs
            // index = 4. index < 5? true. loop runs
            // index = 5. index < 5? false. loop stops, inner code does not run
            for (int index = 0; index < 5; index++)
            {
                // Each iteration, we draw a circle at X of 80
                // Each loop, we multiply 'index' by 80px to offset X coordinate
                // index 0. 80+0*60 = 80
                // index 1. 80+1*60 = 140
                // index 2. 80+2*60 = 200
                // index 3. 80+3*60 = 260
                // index 4. 80+4*60 = 320
                int x = 80 + index * 60;
                Draw.Circle(x, 200, 20);
            }
        }
    }

}
