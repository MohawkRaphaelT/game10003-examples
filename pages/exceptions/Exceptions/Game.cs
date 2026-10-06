using System;
using System.Numerics;

namespace MohawkGame2D;

public class Game
{
    Frog frog;

    public void Setup()
    {
        Window.SetTitle("Frog is Null");
        Window.SetSize(800, 600);
    }

    public void Update()
    {
        // Reset background
        Window.ClearBackground(240);

        // Draw frogs
        frog.Update();
    }
}