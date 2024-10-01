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
        string ScreenTitle { get; } = "Mouse Cursor (Absolute)";
        int ScreenWidth { get; } = 800; // pixels
        int ScreenHeight { get; } = 600; // pixels
        int GridSize { get; } = 100; // pixels
        int GridLineSize { get; } = 3; // pixels
        int CursorHalfSize { get; } = 40 / 2; // pixels
        int CrosshairThicknessLines { get; } = 3; // pixels
        int CrosshairThicknessCross { get; } = 7; // pixels

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle(ScreenTitle);
            Window.SetSize(ScreenWidth, ScreenHeight);
            Input.HideMouseCursor();
            Text.Size = 48;
            Text.ResetFont();
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            DrawBackgroundGrid();
            DrawMouseCursor();
            DrawMouseCoordinates();
        }

        void DrawBackgroundGrid()
        {
            Color lightGrey = new Color(230, 230, 230, 255);
            DrawGrid(lightGrey, GridSize / 4);

            Color grey = new Color(200, 200, 200, 255);
            DrawGrid(grey, GridSize);
        }

        void DrawGrid(Color color, int cellSize)
        {
            Draw.LineColor = color;
            Draw.LineSize = GridLineSize;

            // Draw vertical lines
            for (int w = 0; w < Window.Width; w += cellSize)
            {
                Vector2 start = new Vector2(w, 0);
                Vector2 end = new Vector2(w, Window.Height);
                Draw.LineSharp(start, end);
            }
            // Draw horizontal lines
            for (int h = 0; h < Window.Height; h += cellSize)
            {
                Vector2 start = new Vector2(0, h);
                Vector2 end = new Vector2(Window.Width, h);
                Draw.LineSharp(start, end);
            }
        }

        void DrawMouseCursor()
        {
            Vector2 position = Input.GetMousePosition();

            // Draw thin crosshair across whole screen
            {
                Vector2 left = new Vector2(0, position.Y);
                Vector2 right = new Vector2(Window.Width, position.Y);
                Vector2 top = new Vector2(position.X, 0);
                Vector2 bottom = new Vector2(position.X, Window.Height);
                Draw.LineColor = Color.Red;
                Draw.LineSize = CrosshairThicknessLines;
                Draw.LineSharp(left, right);
                Draw.LineSharp(top, bottom);
            }

            // Draw thick crosshair on cursor
            {
                Vector2 left = position - Vector2.UnitX * CursorHalfSize;
                Vector2 right = position + Vector2.UnitX * CursorHalfSize;
                Vector2 top = position - Vector2.UnitY * CursorHalfSize;
                Vector2 bottom = position + Vector2.UnitY * CursorHalfSize;
                Draw.LineColor = Color.Red;
                Draw.LineSize = CrosshairThicknessCross;
                Draw.LineSharp(left, right);
                Draw.LineSharp(top, bottom);
            }

            // Draw white dot at centre
            Draw.LineSize = 0;
            Draw.FillColor = Color.White;
            Draw.Rectangle(position - Vector2.One, Vector2.One * 2);
        }

        void DrawMouseCoordinates()
        {
            // Some parameters for drawing text
            Color translucideWhite = new Color(255, 255, 255, 200);
            int padding = 10; // 10px

            // Get mouse X and Y positions
            Vector2 position = Input.GetMousePosition();
            int positionX = (int)position.X;
            int positionY = (int)position.Y;
            // Create string displaying this coordinate
            string coordinates = $"({positionX}, {positionY})";

            // Calculate how wide and tall the text is in pixels
            int textWidth = Text.Size * (coordinates.Length + 1) / 2;
            int textHeight = Text.Size;

            // Right boundary
            // If past boundary, place text on left side of cursor
            // If not, pad lightly to the right
            int rightBoundary = Window.Width - textWidth - padding;
            if (positionX > rightBoundary)
                positionX -= textWidth + padding;
            else
                positionX += padding;

            // Lower boundary
            // If past boundary, place text on above of cursor
            // If not, pad lightly to below it
            int lowerBoundary = Window.Height - textHeight - padding;
            if (positionY > lowerBoundary)
                positionY -= textHeight + padding;
            else
                positionY += padding;

            // Draw a box behind the text
            Draw.LineSize = 0;
            Draw.FillColor = translucideWhite;
            Draw.Rectangle(positionX, positionY, textWidth, textHeight);
            // Draw the text on top
            Text.Draw(coordinates, positionX, positionY);
        }
    }
}
