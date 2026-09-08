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
        public void Setup()
        {
            Window.SetTitle("Equality");
            Window.SetSize(400, 400);
        }

        public void Update()
        {
            // Set background color based on key press
            if (Input.IsKeyboardKeyDown(KeyboardKey.Space) == true)
            {
                // Green
                Window.ClearBackground(0, 255, 0);
            }
            else
            {
                // Off-white
                Window.ClearBackground(240);
            }
        }
    }

}
