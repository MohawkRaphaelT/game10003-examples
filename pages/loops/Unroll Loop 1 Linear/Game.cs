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
        // Before the loop runs
        Window.ClearBackground(0);
        Draw.FillColor = new Color(255, 100);
        // Code that runs in the above loop.
        Draw.Circle(200, 200, 50);
        Draw.Circle(200, 200, 50);
        Draw.Circle(200, 200, 50);
        Draw.Circle(200, 200, 50);
        Draw.Circle(200, 200, 50);
    }
}
