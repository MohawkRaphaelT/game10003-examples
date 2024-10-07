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
        Vector2 position;
        Vector2 size = new Vector2(40, 60);
        Vector2 rectanglePosition;
        Vector2 rectangleSize;
        Color red = Color.Red;
        Color green = new(0, 180, 0);


        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Rect-Rect Intersection");
            Window.SetSize(800, 600);
            // Assign middle rect position and size
            rectanglePosition = new Vector2(240, 260);
            rectangleSize = new Vector2(320, 240);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            UpdatePlayerPosition();
            DrawIntersection();
        }

        void DrawIntersection()
        {
            // Get sides of rectangle
            float rectangleLeftEdge   = (int)(rectanglePosition.X                  );
            float rectangleRightEdge  = (int)(rectanglePosition.X + rectangleSize.X);
            float rectangleTopEdge    = (int)(rectanglePosition.Y                  );
            float rectangleBottomEdge = (int)(rectanglePosition.Y + rectangleSize.Y);

            // Compute each side of the player (also a rectangle)
            float playerLeftEdge   = (int)(position.X         );
            float playerRightEdge  = (int)(position.X + size.X);
            float playerTopEdge    = (int)(position.Y         );
            float playerBottomEdge = (int)(position.Y + size.Y);

            // Check each side and see if player is inside any edge
            bool isInsideLeftEdge = playerRightEdge >= rectangleLeftEdge;   // left check
            bool isInsideRightEdge = playerLeftEdge <= rectangleRightEdge;  // right check
            bool isInsideTopEdge = playerBottomEdge >= rectangleTopEdge;    // top check
            bool isInsideBottomEdge = playerTopEdge <= rectangleBottomEdge; // bottom check

            // Draw "player"
            Draw.LineSize = 0;
            Draw.FillColor = Color.White;
            Draw.Rectangle(position, size);
            // Draw sides of the player
            DrawCollisionEdges(isInsideRightEdge, playerLeftEdge, playerTopEdge, playerLeftEdge, playerBottomEdge);    // left
            DrawCollisionEdges(isInsideLeftEdge, playerRightEdge, playerTopEdge, playerRightEdge, playerBottomEdge); // right
            DrawCollisionEdges(isInsideBottomEdge, playerLeftEdge, playerTopEdge, playerRightEdge, playerTopEdge);       // top
            DrawCollisionEdges(isInsideTopEdge, playerLeftEdge, playerBottomEdge, playerRightEdge, playerBottomEdge); // bottom
            // Draw sides of the rectangle
            DrawCollisionEdges(isInsideLeftEdge, rectangleLeftEdge, rectangleTopEdge, rectangleLeftEdge, rectangleBottomEdge);    // left
            DrawCollisionEdges(isInsideRightEdge, rectangleRightEdge, rectangleTopEdge, rectangleRightEdge, rectangleBottomEdge); // right
            DrawCollisionEdges(isInsideTopEdge, rectangleLeftEdge, rectangleTopEdge, rectangleRightEdge, rectangleTopEdge);       // top
            DrawCollisionEdges(isInsideBottomEdge, rectangleLeftEdge, rectangleBottomEdge, rectangleRightEdge, rectangleBottomEdge); // bottom

            // Build up some text to display what is going on
            string displayLeft   = $"Player Right  >= Rect Left   = {playerRightEdge,3} >= {rectangleLeftEdge,3}, {GetCharTF(isInsideLeftEdge)}";
            string displayRight  = $"Player Left   <= Rect Right  = {playerLeftEdge,3} <= {rectangleRightEdge,3}, {GetCharTF(isInsideRightEdge)}";
            string displayTop    = $"Player Bottom >= Rect Top    = {playerBottomEdge,3} >= {rectangleTopEdge,3}, {GetCharTF(isInsideTopEdge)}";
            string displayBottom = $"Player Top    <= Rect Bottom = {playerTopEdge,3} <= {rectangleBottomEdge,3}, {GetCharTF(isInsideBottomEdge)}";
            // Prepare variables to display text on screen
            int gap = 10;
            int posX = gap;
            int posY = gap;
            int fontSize = 32;
            int increment = fontSize + gap;
            // Write text to screen
            DisplayEdgeCheckResult(isInsideLeftEdge, displayLeft, posX, posY, fontSize);
            DisplayEdgeCheckResult(isInsideRightEdge, displayRight, posX, posY += increment, fontSize);
            DisplayEdgeCheckResult(isInsideTopEdge, displayTop, posX, posY += increment, fontSize);
            DisplayEdgeCheckResult(isInsideBottomEdge, displayBottom, posX, posY += increment, fontSize);
        }

        void DrawCollisionEdges(bool isInRectangle, float startX, float startY, float endX, float endY)
        {
            Color color = GetCollisionColor(isInRectangle);
            Vector2 start = new Vector2(startX, startY);
            Vector2 end = new Vector2(endX, endY);
            Draw.LineSize = 3;
            Draw.LineColor = color;
            Draw.Line(start, end);
        }

        Color GetCollisionColor(bool isInRectangle)
        {
            Color color = isInRectangle ? green : red;
            return color;
        }

        void DisplayEdgeCheckResult(bool isInRectangle, string display, float posX, float posY, int fontSize)
        {
            Color color = GetCollisionColor(isInRectangle);
            Text.Color = color;
            Text.Size = fontSize;
            Text.Draw(display, posX, posY);
        }

        void UpdatePlayerPosition()
        {
            position = Input.GetMousePosition() - size / 2;
        }

        char GetCharTF(bool trueOrFalse)
        {
            return trueOrFalse ? 'T' : 'F';
        }
    }
}
