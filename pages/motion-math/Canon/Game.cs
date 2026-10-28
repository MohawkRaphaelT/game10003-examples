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
    Canonball[] canonballs = new Canonball[100];
    int currentCanonball = 0;

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        Window.SetTitle("Canonball Launcher");
        Window.SetSize(400, 400);

        for (int i = 0; i < canonballs.Length; i++)
        {
            canonballs[i] = new Canonball();
        }
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        Window.ClearBackground(Color.OffWhite);

        float canonLength = 50;
        Vector2 canonBase = new Vector2(Window.Width / 2, Window.Height);
        Vector2 target = Input.GetMousePosition();
        Vector2 canonDirection = Vector2.Normalize(target - canonBase);
        Vector2 canonEnd = canonBase + canonDirection * canonLength;

        Draw.LineSize = 5;
        Draw.LineColor = Color.Red;
        Draw.Line(canonBase, canonEnd);

        if (Input.IsMouseButtonPressed(MouseButton.Left))
        {
            // Get canon ball and launch it
            Canonball canonBall = canonballs[currentCanonball];
            canonBall.Launch(canonEnd, canonDirection);
            // Increment and wrap index
            currentCanonball += 1;
            currentCanonball %= canonballs.Length;
        }

        for (int i = 0; i < canonballs.Length; i++)
        {
            canonballs[i].Update();
        }
    }
}
