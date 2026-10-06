// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D;

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
        Window.SetTitle("Unrolled For Loop");
        Window.SetSize(400, 400);
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        // Set up drawing
        Draw.LineSize = 0;
        Draw.FillColor = Color.Red;
        // Loop 5 times
        for (int count = 0; count < 5; count++)
        {
            // Each time, calculate the radius
            int radius = 100 - count * 20;
            // And calculate the color, too!
            Draw.SetFillColor(255 - count * 64);
            // Then draw a new circle overtop with the new radius.
            Draw.Circle(200, 200, radius);
        }
    }
}
