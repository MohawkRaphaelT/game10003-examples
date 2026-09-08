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
            Window.SetTitle("Addition and Subtraction");
            Window.SetSize(400, 400);
        }

        public void Update()
        {
            // Clear previous image with off-white color
            Window.ClearBackground(240);

            // Green fill with black outline
            Draw.SetFillColor(0, 255, 0);
            Draw.SetLineColor(0);
            Draw.SetLineSize(1);

            // Draw a circle at the mouse position
            Draw.Circle(Input.GetMouseX(), Input.GetMouseY(), 25);

            // But also at and offset position!
            // +100 to X moves it right
            // -50 to Y moves it up
            Draw.Circle(Input.GetMouseX() + 100, Input.GetMouseY() - 50, 25);
        }
    }

}
