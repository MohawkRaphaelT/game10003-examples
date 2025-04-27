// Include the namespaces (code libraries) you need below.
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D;

/// <summary>
///     Your game code goes inside this class!
/// </summary>
public class Game
{
    // Place your variables here:
    private readonly Color inactiveColor = Color.White;
    private readonly Color activeColor = Color.Red;
    private readonly float size = 32;
    private readonly float padS = 3;
    private readonly float padL = 16;

    public List<ControllerInputs> Controllers = [];


    /// <summary>
    ///     Setup runs once before the game loop begins.
    /// </summary>
    public void Setup()
    {
        Window.SetTitle("Controller Input Viewer");
        Window.SetSize(800, 800);
    }

    /// <summary>
    ///     Update runs every frame.
    /// </summary>
    public void Update()
    {
        Window.ClearBackground(Color.OffWhite);
        CheckControllers();

        for (int i = 0; i < Controllers.Count; i++)
        {
            float x = padL * (i + 1);
            DrawController(Controllers[i], x, padL);
        }

        UpdateWindowSize();
    }

    private void DrawDigital(Vector2 pos, int id, ControllerButton button)
        => DrawDigital(pos.X, pos.Y, id, button);
    private void DrawDigital(float x, float y, int id, ControllerButton button)
    {
        float halfSize = size / 2;
        Vector2 position = new(x, y);
        position += Vector2.One * halfSize;
        bool state = Input.IsControllerButtonDown(id, button);
        Draw.LineSize = 1;
        Draw.LineColor = Color.Black;
        Draw.FillColor = state ? activeColor : inactiveColor;
        Draw.Circle(position, size / 2);
        position.X += size;
        position.Y += padS;
        position -= Vector2.One * halfSize;
        Text.Size = (int)size;
        Text.Draw(button.ToString(), position);
    }
    private void DrawAxis(Vector2 pos, int id, ControllerAxis axis)
        => DrawAxis(pos.X, pos.Y, id, axis);
    private void DrawAxis(float x, float y, int id, ControllerAxis axis)
    {
        Vector2 position = new(x, y);
        float value = Input.GetControllerAxis(id, axis); // -1 to +1
        float fWidth = size * 4;
        float hWidth = fWidth / 2;
        Vector2 rAnchor = value >= 0 ?
            position + new Vector2(hWidth, 0):
            position + new Vector2(hWidth * (1 + value), 0);
        Vector2 rSize = new(MathF.Abs(value * hWidth), size);
        Vector2 rBox = new(fWidth, size);
        Draw.LineSize = 1;
        Draw.LineColor = Color.Black;
        Draw.FillColor = activeColor;
        Draw.Rectangle(rAnchor, rSize);
        // bnorder box
        Draw.FillColor = Color.Clear;
        Draw.Rectangle(position, rBox);
        // centre line
        Draw.Line(position.X +hWidth, position.Y, position.X + hWidth, position.Y + size);
        position.X += size * 4;
        position.Y += padS;
        Text.Size = (int)size;
        Text.Draw($"{value:+0.00;-0.00; 0.00} {axis}", position);
    }
    public void DrawController(ControllerInputs controller, float x, float y)
    {
        Vector2 anchor = new(x, y);
        string name = Raylib.GetGamepadName_(controller.ID);
        Text.Draw($"{controller.ID}: {name}", anchor); anchor.Y += size;
        foreach (var button in controller.Buttons)
        {
            DrawDigital(anchor, controller.ID, button);
            anchor.Y += size;
            anchor.Y += padS;
        }
        foreach (var axis in controller.Axes)
        {
            DrawAxis(anchor, controller.ID, axis);
            anchor.Y += size;
            anchor.Y += padS;
        }
    }

    public void CheckControllers()
    {
        int count = Input.GetConnectedControllerCount();
        while (Controllers.Count < count)
        {
            Controllers.Add(new ControllerInputs()
            {
                // Add next controller index
                ID = Controllers.Count,
                Buttons = [],
                Axes = [],
            });
        }

        foreach (var controller in Controllers)
        {
            CheckControllerButtons(controller);
            CheckControllerAxis(controller);
        }
    }
    public void CheckControllerButtons(ControllerInputs controller)
    {
        var items = Enum.GetValues<ControllerButton>()
            .OrderBy(str => str)
            .ToArray();
        foreach (var button in items)
        {
            bool isActive = Input.IsControllerButtonPressed(controller.ID, button);
            if (isActive && !controller.Buttons.Contains(button))
            {
                controller.Buttons.Add(button);
            }
        }
    }
    public void CheckControllerAxis(ControllerInputs controller)
    {
        var items = Enum.GetValues<ControllerAxis>()
            .OrderBy(str => str)
            .ToArray();
        foreach (var axis in items)
        {
            float value = Input.GetControllerAxis(controller.ID, axis);
            bool isActive = MathF.Abs(value) > 0.1f;
            if (isActive && !controller.Axes.Contains(axis))
            {
                controller.Axes.Add(axis);
            }
        }
    }
    public void UpdateWindowSize()
    {
        int maxElements = GetMaxControllerElements();
        float conW = 500 * Controllers.Count; 
        float conH = size * maxElements; // lines only
        float padH = padS * maxElements +
                     padL * (Controllers.Count + 1) ;
        float padW = padL * (Controllers.Count + 1);
        float w = conW + padW;
        float h = conH + padH;

        int x = Math.Max((int)w, 200);
        int y = Math.Max((int)h, 200);
        Window.SetSize(x, y);
    }

    private int GetMaxControllerElements()
    {
        int max = int.MinValue;
        foreach (ControllerInputs controller in Controllers)
        {
            int count = controller.ElementCount();
            max = Math.Max(max, count);
        }
        return max;
    }
}

public readonly record struct ControllerInputs
{
    public int ID { get; init; }
    public List<ControllerButton> Buttons { get; init; }
    public List<ControllerAxis> Axes { get; init; }

    public int ElementCount()
    {
        // name + buttons + axes
        return 1 + Buttons.Count + Axes.Count;
    }
}
