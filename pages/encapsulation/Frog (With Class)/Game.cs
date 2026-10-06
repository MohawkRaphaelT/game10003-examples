using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    // Variables
    Color bg = new Color(0, 161, 224);
    Color frogGreen = new Color(140, 240, 110);
    Color frogPink = new Color(255, 170, 150);
    // Initialize class
    Frog frog = new Frog();

    public void Setup()
    {
        Window.SetTitle("Frog (With Class)");
        Window.SetSize(800, 600);

        // Initialize frog's color variables
        frog.skin = frogGreen;
        frog.cheek = frogPink;
    }

    public void Update()
    {
        // Reset background
        Window.ClearBackground(bg);

        // Assign frog's position
        frog.x = Input.GetMouseX();
        frog.y = Input.GetMouseY();

        // Call frog function to draw it with it's local variables
        frog.Update();
    }
}