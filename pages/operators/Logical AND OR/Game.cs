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
            Window.SetTitle("Logical AND OR");
            Window.SetSize(400, 400);
        }

public void Update()
{
    // Clear previous image
    Window.ClearBackground(240);


    // AND
    // Check to see if EITHER key is pressed
    // One is the number one [1] key
    // Two is the number two [2] key
    if (Input.IsKeyboardKeyDown(KeyboardKey.One) == true && Input.IsKeyboardKeyDown(KeyboardKey.Two) == true)
    {
        // Red
        Draw.SetFillColor(255, 0, 0);
        Draw.Circle(120, 200, 50);
    }

    // OR
    // Check to see if EITHER key is pressed
    if (Input.IsKeyboardKeyDown(KeyboardKey.One) == true || Input.IsKeyboardKeyDown(KeyboardKey.Two) == true)
    {
        // Blue
        Draw.SetFillColor(0, 0, 255);
        Draw.Circle(280, 200, 50);
    }
}
    }
}
