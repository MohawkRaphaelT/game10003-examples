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
            Window.SetTitle("Wave Motion With Sine");
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
            DrawWave();
        }

        void DrawWave()
        {
            // Get the current time as the driver for Sine wave
            float secondsScaled = Time.SecondsElapsed / 5; // 5 seconds per cycle
            float cycle = secondsScaled * MathF.Tau;       // 1 cycle == 1 radian == Tau (6.28 or 2*pi)

            // Get the position of the wave.
            // As a basis, we get the screen centre and add the wave offset to it.
            float screenCentreY = Window.Height / 2;
            float waveOffset = MathF.Sin(cycle) * 100; // 100px above and below centre
            float positionY = screenCentreY + waveOffset;
            Vector2 leftPoint = new Vector2(0, positionY);
            Vector2 rightPoint = new Vector2(Window.Width, positionY);
            Vector2 size = new Vector2(Window.Width, Window.Height - positionY);

            // Draw colored rectangle
            Draw.LineSize = 0;
            Draw.FillColor = new Color(102, 191, 255, 100); // translucid sky blue
            Draw.Rectangle(leftPoint, size);
            // Draw line across the screen horizontally
            Draw.LineSize = 5;
            Draw.LineColor = new Color(0, 121, 241); // blue
            Draw.Line(leftPoint, rightPoint);
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
