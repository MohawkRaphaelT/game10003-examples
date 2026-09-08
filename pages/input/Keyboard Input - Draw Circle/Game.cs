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
            Window.SetTitle("Keyboard Input - Draw Circles");
            Window.SetSize(400, 400);
        }

        public void Update()
        {
            // Clear previous image
            Window.ClearBackground(240);
            // Disable outline
            Draw.SetLineSize(0);

            // Colours from: https://lospec.com/palette-list/eb-gb-mint-flavour

            // Check to see if the 'spacebar' key is held down
            if (Input.IsKeyboardKeyDown(KeyboardKey.Space) == true)
            {
                Draw.SetFillColor("#64d0b8");
                Draw.Circle(100, 200, 50);
            }

            // Check to see if 'alt' key on the right side of the keyboard is NOT held down
            if (Input.IsKeyboardKeyUp(KeyboardKey.RightAlt) == true)
            {
                Draw.SetFillColor("#291f3e");
                Draw.Circle(300, 200, 50);
            }
        }
    }

}
