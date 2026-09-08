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
            Window.SetTitle("Conditional If Else");
            Window.SetSize(400, 400);
        }

        public void Update()
        {
            // Clear previous image
            Window.ClearBackground(240);

            // Check to see if spacebar key is held down
            if (Input.IsKeyboardKeyDown(KeyboardKey.Space) == true)
            {
                // If it is, then:
                // Draw green circle in the center of the window
                Draw.SetFillColor("00FF00");
                Draw.Circle(Window.Width / 2, Window.Height / 2, 50);
            }
            else
            {
                // If it is not, then:
                // Draw a red square
                Draw.SetFillColor("FF0000");
                Draw.Square(Window.Width / 2 - 50, Window.Height / 2 - 50, 100);
            }
        }
    }
}
