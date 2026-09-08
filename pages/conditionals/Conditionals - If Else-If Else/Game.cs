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
            Window.SetTitle("Conditional If Else-If Else");
            Window.SetSize(400, 400);
        }

        public void Update()
        {
            // Set background color based on mouse position
            if (Input.GetMouseY() < 50)
            {
                // Green
                Window.ClearBackground(0, 255, 0);
            }
            else if (Input.GetMouseY() > 350)
            {
                // Blue
                Window.ClearBackground(0, 0, 255);
            }
            else
            {
                // Off-white
                Window.ClearBackground(240);
            }
        }
    }

}
