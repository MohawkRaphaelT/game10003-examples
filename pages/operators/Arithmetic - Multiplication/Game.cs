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
            Window.SetTitle("Multiplication");
            Window.SetSize(400, 400);
        }

        public void Update()
        {
            // Clear previous image with off-white color
            Window.ClearBackground(240);

            // Black outline
            Draw.SetLineColor(0);
            Draw.SetLineSize(1);

            // Red = fast
            Draw.SetFillColor(255, 0, 0);
            Draw.Circle(Input.GetMouseX() * 1.5f, Input.GetMouseY() * 1.5f, 25);

            // Blue = slow
            Draw.SetFillColor(0, 0, 255);
            Draw.Circle(Input.GetMouseX() * 0.5f, Input.GetMouseY() * 0.5f, 25);

            // Green = normal speed
            Draw.SetFillColor(0, 255, 0);
            Draw.Circle(Input.GetMouseX(), Input.GetMouseY(), 25);
        }
    }

}
