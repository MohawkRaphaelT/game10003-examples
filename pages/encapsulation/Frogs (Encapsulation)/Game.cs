using System;
using System.Numerics;

namespace Game10003;

public class Game
{
    // Place your variables here:
    Color bg = new Color(0, 161, 224);
    Color frogGreen = new Color(140, 240, 110);
    Color frogPink = new Color(255, 170, 150);

    Frog frog;

    public void Setup()
    {
        Window.SetTitle("Frog (Encapsulated)");
        Window.SetSize(800, 600);

        // Initialize class
        frog = new Frog();
        frog.skin = frogGreen;
        frog.cheek = frogPink;
    }

    public void Update()
    {
        // Reset background
        Window.ClearBackground(bg);

        // Assign to frog's position
        frog.x = Input.GetMouseX();
        frog.y = Input.GetMouseY();
        // Call frog function to draw it with it's internal variables
        frog.Render();
    }
}
