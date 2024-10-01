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
        string ScreenTitle { get; } = "Keyboard Input (Relative)";
        Vector2 Position { get; set; }
        Vector2 Size { get; set; } = new Vector2(67, 100);
        float Speed { get; } = 400; // pixels per second
        Vector2 PlayerInput { get; set; }

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle(ScreenTitle);
            Window.SetSize(800, 600);
            Position = Window.Size / 2f;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);
            DrawGrid(20, 1, Color.LightGray);
            DrawGrid(100, 3, Color.LightGray);
            GetPlayerInput();
            MovePlayer();
            DrawPlayer();
        }

        void GetPlayerInput()
        {
            // Create a local variable to house input
            Vector2 input = Vector2.Zero;

            // NOTE:
            // IsKeyDown: always true if key is held down
            // IsKeyPressed: only true once when key is pressed

            // UP
            if (Input.IsKeyboardKeyDown(KeyboardInput.Up) || Input.IsKeyboardKeyDown(KeyboardInput.W))
            {
                input.Y -= 1;
            }

            // DOWN
            if (Input.IsKeyboardKeyDown(KeyboardInput.Down) || Input.IsKeyboardKeyDown(KeyboardInput.S))
            {
                input.Y += 1;
            }

            // LEFT
            if (Input.IsKeyboardKeyDown(KeyboardInput.Left) || Input.IsKeyboardKeyDown(KeyboardInput.A))
            {
                input.X -= 1;
            }

            // RIGHT
            if (Input.IsKeyboardKeyDown(KeyboardInput.Right) || Input.IsKeyboardKeyDown(KeyboardInput.D))
            {
                input.X += 1;
            }

            // Assign local vector variable to class property
            PlayerInput = input;
        }

        void MovePlayer()
        {
            // Smooth diagonal movements
            bool hasInput = PlayerInput.Length() > 0;
            if (hasInput)
            {
                PlayerInput = Vector2.Normalize(PlayerInput);
            }

            // Calculate movement over time
            Vector2 movement = PlayerInput * Speed * Time.DeltaTime;

            // Add movement to position
            Position += movement;
        }

        void DrawPlayer()
        {
            // Construct position at centre of player rectangle
            Vector2 halfSize = Size / 2f;
            float positionX = Position.X - halfSize.X;
            float positionY = Position.Y - halfSize.Y;
            float width = Size.X;
            float height = Size.Y;

            Draw.LineSize = 3;
            Draw.LineColor = Color.Black;
            Draw.FillColor = Color.Red;
            Draw.Rectangle(positionX, positionY, width, height);
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
