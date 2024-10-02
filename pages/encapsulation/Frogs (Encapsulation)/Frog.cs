using System;
using System.Numerics;

namespace Game10003;

public class Frog
{
    public Color skin;
    public Color cheek;
    public float x;
    public float y;

    public void Render()
    {
        Draw.LineSize = 0;
        // Body and eyes (green)
        Draw.FillColor = skin;
        Draw.Circle(x - 25, y - 70, 23);
        Draw.Circle(x + 25, y - 70, 23);
        Draw.Capsule(x - 50, y - 40, x + 50, y - 40, 25);
        // Feet
        Draw.Square(x - 20, y - 15, 15); // L
        Draw.Square(x + 05, y - 15, 15); // L
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
        Draw.PolyLine(x - 15, y - 40, x, y - 25, x + 15, y - 40);
    }
}
