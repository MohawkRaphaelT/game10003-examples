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
            Window.SetTitle("Remainder");
            Window.SetSize(400, 400);
        }

        public void Update()
        {
            // Clear previous image with off-white color
            Window.ClearBackground(240);

            // Black outline
            Draw.SetLineColor(0);
            Draw.SetLineSize(1);

            // Line grid to help visualize
            // Vertical lines
            Draw.Line(100, 0, 100, 400);
            Draw.Line(200, 0, 200, 400);
            Draw.Line(300, 0, 300, 400);
            // Horizontal lines
            Draw.Line(0, 100, 400, 100);
            Draw.Line(0, 200, 400, 200);
            Draw.Line(0, 300, 400, 300);

            // Red loops inside the range 0-100
            Draw.SetFillColor(255, 0, 0);
            Draw.Circle(Input.GetMouseX() % 100, Input.GetMouseY() % 100, 25);

            // Blue loops inside the range 0-200
            Draw.SetFillColor(0, 0, 255);
            Draw.Circle(Input.GetMouseX() % 200, Input.GetMouseY() % 200, 20);

            // Green loops inside the range 0-300
            Draw.SetFillColor(0, 255, 0);
            Draw.Circle(Input.GetMouseX() % 300, Input.GetMouseY() % 300, 15);
        }
    }

}
