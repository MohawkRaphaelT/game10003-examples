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


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("UFO Motion With Sine and Cosine");
            Window.SetSize(800, 600);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            DrawGrid(20, 1, Color.LightGray);
            DrawGrid(100, 3, Color.LightGray);
            DrawUFO();
        }

        void DrawUFO()
        {
            // Get the current time as the driver for Sine wave
            float time = Time.SecondsElapsed; // We must convert double to float
            float cycle = time * MathF.Tau;       // 1 cycle == 1 radian == Tau (6.28 or 2*pi)

            // UFO position on screen
            // By using different times per cycle, we create "wiggly" oscillations
            float offsetX = MathF.Sin(cycle / 3) * 250; // 500px wide, 3 seconds per cycle
            float offsetY = MathF.Cos(cycle / 5) * 100; // 200px tall, 5 seconds per cycle
            Vector2 offset = new Vector2(offsetX, offsetY);
            Vector2 screenCentre = new Vector2(Window.Width, Window.Height) / 2f;

            // UFO parameters
            float ufoWidth = 180;
            float ufoHeight = 50;
            float ufoCockpitRadius = 60;
            float ufoBeamThickness = 5;
            float ufoBeamOffsetY = 150;
            float ufoBeamOffsetX = ufoBeamOffsetY / 3f;

            // Position of UFO parts
            Vector2 bodyPosition = screenCentre + offset;
            Vector2 beamBasePosition = bodyPosition + (Vector2.UnitY * ufoHeight / 3);
            Vector2 cockpitPosition = bodyPosition - (Vector2.UnitY * ufoCockpitRadius / 2.5f);
            Vector2 beamPosition = beamBasePosition + (Vector2.UnitY * ufoBeamOffsetY);
            Vector2 beamPositionLeft = beamPosition + (Vector2.UnitX * ufoBeamOffsetX);
            Vector2 beamPositionRight = beamPosition - (Vector2.UnitX * ufoBeamOffsetX);

            // Draw UFO
            Draw.LineSize = 0;
            Draw.FillColor = new Color(102, 191, 255); // sky blue
            Draw.Ellipse(cockpitPosition, Vector2.One * ufoCockpitRadius);
            Draw.FillColor = new Color(200, 122, 255); // purple
            Draw.Ellipse(bodyPosition, new Vector2(ufoWidth, ufoHeight));
            Draw.FillColor = new Color(112, 31, 126); // dark purple
            Draw.Ellipse(beamBasePosition, new Vector2(ufoWidth / 2, ufoHeight / 2));
            // Draw UFO beam
            Draw.LineSize = ufoBeamThickness;
            Draw.LineColor = Color.Green;
            Draw.Line(beamBasePosition, beamPositionLeft);
            Draw.Line(beamBasePosition, beamPositionRight);
            Draw.Line(beamPositionLeft, beamPositionRight);
        }

        void DrawGrid(int cellSize, int thickness, Color color)
        {
            // Prepare line drawing
            Draw.LineSize = thickness;
            Draw.LineColor = color;
            // Vertical lines
            int vLineCount = Window.Width / cellSize;
            for (int x = 0; x <= vLineCount; x++)
            {
                Vector2 top = new Vector2(x * cellSize, 0);
                Vector2 bottom = new Vector2(x * cellSize, Window.Height);
                Draw.Line(top, bottom);
            }
            // Horizontal lines
            int hLineCount = Window.Height / cellSize;
            for (int y = 0; y <= hLineCount; y++)
            {
                Vector2 left = new Vector2(0, y * cellSize);
                Vector2 right = new Vector2(Window.Width, y * cellSize);
                Draw.Line(left, right);
            }
        }
    }
}
