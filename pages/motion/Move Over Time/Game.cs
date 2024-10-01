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


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Move Over Time");
            Window.SetSize(400, 400);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            // Move at 5px per second
            Draw.FillColor = Color.Blue;
            Draw.Circle(Time.SecondsElapsed * 5, 100, 35);

            // Move at 15px per second
            Draw.FillColor = Color.Green;
            Draw.Circle(Time.SecondsElapsed * 15, 200, 35);

            // Move at 30px per second
            Draw.FillColor = Color.Red;
            Draw.Circle(Time.SecondsElapsed * 30, 300, 35);
        }
    }
}
