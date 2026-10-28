using System;
using System.Numerics;

namespace MohawkGame2D;

public class Canonball
{
    Vector2 position = new Vector2(-1000); // hide offscreen until launched
    Vector2 velocity;
    Vector2 gravity = new Vector2(0, +10);
    float speed = Random.Float(450, 600) / Window.TargetFPS; // convert into pixels per second
    float radius = 10;

    public void Launch(Vector2 position, Vector2 direction)
    {
        this.position = position;
        this.velocity = direction * speed;
    }

    public void Update()
    {
        // Update gravity
        Vector2 gravityForce = gravity * Time.DeltaTime;
        velocity += gravityForce;
        position += velocity;

        // Draw canon ball
        Draw.LineSize = 1;
        Draw.LineColor = Color.Black;
        Draw.FillColor = Color.DarkGray;
        Draw.Circle(position, radius);
    }
}
