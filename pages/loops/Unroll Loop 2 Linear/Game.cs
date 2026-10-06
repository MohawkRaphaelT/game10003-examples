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
        Draw.LineSize = 1;
        Draw.LineColor = Color.Black;
        Draw.FillColor = Color.Red;

        // Declare our variables here:
        int count;
        int radius;

        // Loop counter variable is initialized
        count = 0;

        // Count is 0
        // Is count less than 5? true! So we start running the loop with count == 0.
        radius = 50 - count * 10; // 50
        Draw.SetFillColor(256 - count * 64); // 256
        Draw.Circle(200, 200, radius);
        // Done loop iteration, so we do count++ which is count += 1
        count++;

        // Count is now 1
        // Is count less than 5? true! So we start running the loop with count == 1.
        radius = 50 - count * 10; // 40
        Draw.SetFillColor(256 - count * 64); // 196
        Draw.Circle(200, 200, radius);
        // Done loop iteration, so we do count++ which is count += 1
        count++;

        // Count is now 2
        // Is count less than 5? true! So we start running the loop with count == 2.
        radius = 50 - count * 10; // 30
        Draw.SetFillColor(256 - count * 64); // 128
        Draw.Circle(200, 200, radius);
        // Done loop iteration, so we do count++ which is count += 1
        count++;

        // Count is now 3
        // Is count less than 5? true! So we start running the loop with count == 3.
        radius = 50 - count * 10; // 20
        Draw.SetFillColor(256 - count * 64); // 64
        Draw.Circle(200, 200, radius);
        // Done loop iteration, so we do count++ which is count += 1
        count++;

        // Count is now 4
        // Is count less than 5? true! So we start running the loop with count == 4.
        radius = 50 - count * 10; // 10
        Draw.SetFillColor(256 - count * 64); // 0
        Draw.Circle(200, 200, radius);
        // Done loop iteration, so we do count++ which is count += 1
        count++;

        // Count is now 5
        // Is count less than 5? false! 5 is equal to 5, not less than five, so the loop ends.
    }
}
