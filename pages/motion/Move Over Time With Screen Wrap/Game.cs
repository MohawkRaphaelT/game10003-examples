// Include code libraries you need below (use the namespace).
using System;
using System.Numerics;

// The namespace your code is in.
namespace Game10003
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        float x;
        float radius = 50;
        float speed = 200;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Move Over Time With Screen Wrap");
            Window.SetSize(400, 400);

            // Start position off screen
            x = -radius;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            // Move X coordinate over time at a rate of 'speed'
            x += Time.DeltaTime * speed;

            // If X is off right side of screen, move off screen on left side
            if (x > Window.Width + radius)
            {
                // Just offscreen on left side
                x = -radius;
            }
            // If X is off left side of screen, move off screen on right side
            else if (x < -radius)
            {
                // Just offscreen on right side
                x = Window.Width + radius;
            }

            // Draw the circle
            Draw.LineSize = 3;
            Draw.FillColor = Color.Red;
            Draw.Circle(x, Window.Height / 2, radius);
        }
    }
}
