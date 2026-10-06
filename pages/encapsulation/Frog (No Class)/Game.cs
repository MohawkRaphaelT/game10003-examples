using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    // Variables
    Color bg = new Color(0, 161, 224);
    Color frogGreen = new Color(140, 240, 110);
    Color frogPink = new Color(255, 170, 150);

    public void Setup()
    {
        Window.SetTitle("Frog (No Class)");
        Window.SetSize(800, 600);
    }

    public void Update()
    {
        // Reset background
        Window.ClearBackground(bg);

        // Prepare some drawing variables
        float x = Input.GetMouseX();
        float y = Input.GetMouseY();

        // Draw at mouse position
        DrawFrog(x, y, frogGreen, frogPink);
    }

    void DrawFrog(float x, float y, Color skin, Color cheek)
    {
        Draw.LineSize = 0;
        // Body and eyes (green)
        Draw.FillColor = skin;
        Draw.Circle(x - 25, y - 70, 23);
        Draw.Circle(x + 25, y - 70, 23);
        Draw.Capsule(x - 50, y - 40, x + 50, y - 40, 25);
        // Feet
        Draw.Square(x - 20, y - 15, 15); // L
        Draw.Square(x + 05, y - 15, 15); // R
        Draw.Rectangle(x - 40, y - 12, 32, 12); // L
        Draw.Rectangle(x + 05, y - 12, 32, 12); // R
        Draw.Circle(x - 40, y - 6, 6); // L
        Draw.Circle(x + 39, y - 6, 6); // R
        // Cheeks
        Draw.FillColor = cheek;
        Draw.Circle(x - 50, y - 40, 20);
        Draw.Circle(x + 50, y - 40, 20);
        // Eyes
        Draw.FillColor = Color.White;
        Draw.Circle(x - 25, y - 70, 18);
        Draw.Circle(x + 25, y - 70, 18);
        Draw.FillColor = Color.Black;
        Draw.Circle(x - 25, y - 70, 10);
        Draw.Circle(x + 25, y - 70, 10);
        // Mouth
        Draw.LineSize = 3;
        Draw.LineColor = Color.Black;
        Draw.Line(x - 15, y - 40, x, y - 25);
        Draw.Line(x, y - 25, x + 15, y - 40);
    }
}