using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    // Place your variables here:
    Color bgBlue = new Color(0, 161, 224);
    Color frogGreen = new Color(140, 240, 110);
    Color frogRose = new Color(255, 170, 150);
    Color frogPink = new Color(255, 120, 100);

    Frog frog1;
    Frog frog2;

    public void Setup()
    {
        Window.SetTitle("Frogs with Constructors");
        Window.SetSize(800, 600);

        // Frog #1 is a default frog
        frog1 = new Frog();
        // Frog #2 we provide with "unique" colours
        frog2 = new Frog();
        frog2.skinColor = frogRose;
        frog2.cheekColor = frogPink;
    }

    public void Update()
    {
        // Reset background
        Window.ClearBackground(bgBlue);

        // Frog #1 stays in its default position.

        // Frog 2 is placed to the right of Frog #1 but with own height
        frog2.x = frog1.x + 180;
        frog2.y = frog1.y + (Input.GetMouseY() - 300) / 2;
        // Call frog function to draw it with it's internal variables
        frog1.Update();
        frog2.Update();
    }
}
