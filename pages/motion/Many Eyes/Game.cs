// Include code libraries you need below (use the namespace).
using System;
using System.Numerics;

// The namespace your code is in.
namespace Game10003
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Vector2[] eyeCoorindates =
            [new(100),
            new(300, 100),
            new(200, 300),
            new(200),
            new(75, 325),
            new(325, 275),
            new(50, 50),
            new(340, 30),
            new(250, 360),
            new(40, 180),
            new(370, 210),
            new(220, 25),
        ];
        float[] corneaRadii = [35, 35, 20, 50, 30, 28, 10, 20, 10, 12, 10, 9];
        float[] irisRadii   = [23, 20, 13, 30, 20, 20,  6, 12,  7,  8,  7, 6];
        float[] pupilRadii  = [13, 10,  7, 17, 10, 12,  3,  7,  4,  4,  4, 4];
        Color[] irisColors = [];
        Color[] eyeColors = [
            new Color(80, 48, 24),
            new Color(122, 79, 20),
            new Color(104, 119, 82),
            new Color(73, 80, 50),
            new Color(87, 107, 148),
            new Color(157, 168, 144),
            new Color(49, 96, 142),
            new Color(235, 95, 14),
            ];

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Many Eyes");
            Window.SetSize(400, 400);

            irisColors = new Color[eyeCoorindates.Length];
            for (int i = 0; i < irisColors.Length; i++)
            {
                int randomIndex = Random.Integer(eyeColors.Length);
                irisColors[i] = eyeColors[randomIndex];
            }
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);


            for (int i = 0; i < eyeCoorindates.Length; i++)
            {
                DrawEye(eyeCoorindates[i], corneaRadii[i], irisRadii[i], pupilRadii[i], irisColors[i]);
            }
        }

        void DrawEye(Vector2 position, float corneaR, float irisR, float pupilR, Color irisColor)
        {
            // Draw cornea
            Draw.LineSize = 1;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.White;
            Draw.Circle(position, corneaR);
            // Choose to move in the direction of mouse or be at mouse position depending on distance
            Vector2 mousePosition = Input.GetMousePosition();
            Vector2 positionOffset;
            // If mouse is outside of eye radius
            if (Vector2.Distance(position, mousePosition) > corneaR - irisR)
                positionOffset = position + Vector2.Normalize(mousePosition - position) * (corneaR - irisR);
            else // mouse is inside eye radius
                positionOffset = mousePosition;
            // Draw Iris
            //Draw.LineSize = 0;
            Draw.FillColor = irisColor;
            Draw.Circle(positionOffset, irisR);
            // Draw pupil
            Draw.FillColor = Color.Black;
            Draw.Circle(positionOffset, pupilR);
        }
    }
}
