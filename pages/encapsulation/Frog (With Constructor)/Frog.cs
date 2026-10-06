using System;
using System.Numerics;

namespace MohawkGame2D;

public class Frog
{
    // Private variables
    Color skinColor;
    Color cheekColor;
    // Public variables
    public float x;
    public float y;

    // Constructor assign default values to frog
    public Frog()
    {
        x = Window.Width / 2;
        y = Window.Height / 2;
        // Green
        skinColor = new Color(140, 240, 110);
        // Rose
        cheekColor = new Color(255, 170, 150);
    }

    // Secondary constructor that lets you assign unique colors
    public Frog(Color skinColor, Color cheekColor)
    {
        // Assign unique colors pass in through the constructor (function)
        // Since the parameter names of the function are the same as the ones
        // inside this class, we distinguish them using the 'this' keyword.
        this.skinColor = skinColor;
        this.cheekColor = cheekColor;

        // Note here we don't assign x and y coordinates.
    }


    public void Update()
    {
        Draw.LineSize = 0;
        // Body and eyes (green)
        Draw.FillColor = skinColor;
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
        Draw.FillColor = cheekColor;
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
        Draw.Line(x, y - 25, x + 15, y - 40);
        Draw.Line(x - 15, y - 40, x, y - 25);
    }
}