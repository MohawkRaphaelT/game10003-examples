using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    // Variables
    Color bg = new Color(0, 161, 224);
    Color frogGreen = new Color(140, 240, 110);
    Color frogRose = new Color(255, 170, 150);
    Color frogPink = new Color(255, 120, 100);
    Frog frog1 = new Frog();
    Frog frog2;

    public void Setup()
    {
        Window.SetTitle("Frog (With Constructor)");
        Window.SetSize(800, 600);

        // Initialize frog2's colors using parameterized constructor
        // We call this here instead of above for a few reasons I 
        // won't get into right now, point is it avoids a compile error.
        frog2 = new Frog(frogRose, frogPink);
    }

    public void Update()
    {
        // Reset background
        Window.ClearBackground(bg);

        // Assign frogs' positions
        frog1.x = Input.GetMouseX();
        frog1.y = Input.GetMouseY();
        frog2.x = Input.GetMouseX() + 180;
        frog2.y = Input.GetMouseY() + 30;

        // Draw frogs
        frog1.Update();
        frog2.Update();
    }
}