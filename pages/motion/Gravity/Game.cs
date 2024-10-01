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
        Vector2 velocity;
        Vector2 gravity = new Vector2(0, +25); // positive Y is downward
        float friction = 0.8f;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Gravity");
            Window.SetSize(800, 600);
            ResetPosition();
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            
            DoPlayerInput();
            SimulateGravity();
            PreventSliding();
            DoBoundsCollision();

            DrawGrid(20, 1, Color.LightGray);
            DrawGrid(100, 3, Color.LightGray);
            DrawPlayer();
            DrawTextDisplay();
        }

        void PreventSliding()
        {
            bool hasLittleVelocityY = MathF.Abs(velocity.Y) < 0.5f;
            bool isAtBottomOfScreen = position.Y + size.Y > Window.Height - 1;
            if (hasLittleVelocityY && isAtBottomOfScreen)
            {
                float scaledFriction = 1 - (1 - friction) * Time.DeltaTime;
                scaledFriction = MathF.Pow(scaledFriction, 7);
                velocity.X *= scaledFriction;
            }
        }

        void SimulateGravity()
        {
            // Calculate that much time's worth of gravitational force
            Vector2 gravityForce = gravity * Time.DeltaTime;
            // Apply force to velocity
            velocity += gravityForce;
            // Apply velocity to position
            position += velocity;
        }

        void DoBoundsCollision()
        {

            // Compute each side of the player
            float playerLeftEdge = position.X;
            float playerRightEdge = position.X + size.X;
            float playerTopEdge = position.Y;
            float playerBottomEdge = position.Y + size.Y;

            // Check each side and see if player is out-of-bounds
            bool isLeftOfWindow = playerLeftEdge <= 0;              // left check
            bool isRightOfWindow = playerRightEdge >= Window.Width; // right check
            bool isAboveWindow = playerTopEdge <= 0;                // top check
            bool isBelowWindow = playerBottomEdge >= Window.Height; // bottom check

            if (isLeftOfWindow && velocity.X < 0)
                velocity.X = -velocity.X * friction;
            else if (isRightOfWindow && velocity.X > 0)
                velocity.X = -velocity.X * friction;

            if (isAboveWindow && velocity.Y < 0)
                velocity.Y = -velocity.Y * friction;
            else if (isBelowWindow && velocity.Y > 0)
            {
                velocity.Y = -velocity.Y * friction;
                position.Y = Window.Height - size.Y;
            }
        }

        void DrawTextDisplay()
        {
            Text.Draw("Reset : Spacebar", 10, 10);
            Text.Draw("Random: Enter", 10, 42);
        }

        void ResetPosition()
        {
            position = new Vector2(Window.Width / 2 - size.X / 2, size.Y);
            velocity = Vector2.Zero;
        }

        void AddRandomVelocity()
        {
            velocity += Random.Direction() * Random.Float(10, 150);
        }

        void DoPlayerInput()
        {
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
            {
                ResetPosition();
            }

            if (Input.IsKeyboardKeyPressed(KeyboardInput.Enter))
            {
                AddRandomVelocity();
            }
        }

        void DrawPlayer()
        {
            Draw.LineSize = 3;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.White;
            Draw.Rectangle(position, size);
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
