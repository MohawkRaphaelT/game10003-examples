// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    Color red = new Color(220, 0, 0);
    Color green = new Color(0, 200, 0);
    float angleRadians = 0;

    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        Window.SetTitle("Directions and Angles");
        Window.SetSize(400, 400);
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        Window.ClearBackground(Color.OffWhite);

        angleRadians += (Time.DeltaTime * MathF.Tau) / 6;
        angleRadians %= MathF.Tau;

        //angleRadians = MathF.Atan2(direction.Y, direction.X);
        float radius = 100;
        Vector2 direction = new Vector2(MathF.Cos(angleRadians), MathF.Sin(angleRadians));
        float angleRadiansAtan2 = MathF.Atan2(direction.Y, direction.X);
        float angleDegrees = angleRadiansAtan2 / MathF.Tau * 360;

        // "clock hand"
        Vector2 centre = Window.Size / 2;
        Vector2 end = centre + direction * radius;

        // Outer perimieter
        Draw.LineSize = 2;
        Draw.LineColor = Color.Black;
        Draw.FillColor = Color.OffWhite;
        Draw.Circle(Window.Size / 2, radius);
        // Y
        Draw.LineColor = green;
        Draw.Line(Window.Width / 2, 0, Window.Width / 2, Window.Height);
        Draw.Line(end.X, end.Y, end.X, centre.Y);

        // X
        Draw.LineColor = red;
        Draw.Line(0, Window.Height / 2, Window.Width, Window.Height/ 2);
        Draw.Line(end.X, end.Y, centre.X, end.Y);

        // Line / angle
        Draw.LineSize = 6;
        Draw.LineColor = Color.Black;
        Draw.Line(centre, end);

        //
        Text.Color = Color.Black;
        Text.Draw($"Angle Rad: {angleRadiansAtan2,7:0.00}\nAngle Deg: {angleDegrees,7:0.00}", 10, 10);
        Text.Color = red;
        Text.Draw($"+X", Window.Width - Text.Size - 10, Window.Height / 2 + 10);
        Text.Color = green;
        Text.Draw($"+Y", Window.Width / 2 + 10, Window.Height - Text.Size);
    }
}
