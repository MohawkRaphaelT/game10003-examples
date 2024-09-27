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
        Vector2[] positions;
        Vector2[] dirs;
        Color[] colors;
        float[] speeds;
        float[] sizes;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Randomness");
            Window.SetSize(400, 400);
            Draw.LineSize = 0;

            int count = Random.Integer(5, 20);
            positions = new Vector2[count];
            dirs = new Vector2[count];
            colors = new Color[count];
            speeds = new float[count];
            sizes = new float[count];

            for (int i = 0; i < count; i++)
            {
                positions[i] = Random.Vector2(50, Window.Width - 50, 50, Window.Height - 50);
                dirs[i] = Random.Direction();
                colors[i] = Random.Color();
                speeds[i] = Random.Float(3, 50);
                sizes[i] = Random.Integer(10, 25);
            }
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.OffWhite);

            for (int i = 0; i < positions.Length; i++)
            {
                Draw.FillColor = colors[i];
                Draw.Circle(positions[i], sizes[i]);

                positions[i] += dirs[i] * speeds[i] * Time.DeltaTime;
            }
        }
    }
}
