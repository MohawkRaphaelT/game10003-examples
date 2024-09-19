/*///////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
//  ______  ______  __   __  ______  ______ ______  __      __      ______  ______ __  ______  __   __  ______    //
// /\  ___\/\  __ \/\ "-.\ \/\  ___\/\__  _/\  ___\/\ \    /\ \    /\  __ \/\__  _/\ \/\  __ \/\ "-.\ \/\  ___\   //
// \ \ \___\ \ \/\ \ \ \-.  \ \___  \/_/\ \\ \  __\\ \ \___\ \ \___\ \  __ \/_/\ \\ \ \ \ \/\ \ \ \-.  \ \___  \  //
//  \ \_____\ \_____\ \_\\"\_\/\_____\ \ \_\\ \_____\ \_____\ \_____\ \_\ \_\ \ \_\\ \_\ \_____\ \_\\"\_\/\_____\ //
//   \/_____/\/_____/\/_/ \/_/\/_____/  \/_/ \/_____/\/_____/\/_____/\/_/\/_/  \/_/ \/_/\/_____/\/_/ \/_/\/_____/ //
//                                                                                                                //
/*////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/
//                                                                                                                //
//  Constellations, an interactive drawing                                                                        //
//  by Raphael Tetreault                                                                                          //
//  Date: 2014/09/15                                                                                              //
//                                                                                                                //
//  Ported to C# and Raylib_cs                                                                                    //
//  by Raphael Tetreault                                                                                          //
//  Date: 2024/05/10                                                                                              //
//                                                                                                                //
//  The premise of this interactive drawing is that you are looking out into the sky at night, gazing at the      //
//  stars. Moving the cursor around will scroll the screen past it's borders and reveal more of the sky. Looking  //
//  down towards the horizon you see the sunlight after it sets. Curious as you are, you try to find the          //
//  constellations in the sky.                                                                                    //
//                                                                                                                //
//  Pressing the mouse buttons you reveal the constellations briefly with a letter near them. Pressing any key    //
//  will prompt an index which tells you what the constellations are called.                                      //
//                                                                                                                //
/*////////////////////////////////////////////////////////////////////////////////////////////////////////////////*/

using Raylib_cs;
using System.Numerics;

public class Program
{
    // If you need variables in the Program class (outside functions), you must mark them as static
    static readonly string title = "Constellations"; // Window title
    static readonly int width = 400; // Screen width
    static readonly int height = 400; // Screen height
    static readonly int targetFps = 60; // Target frames-per-second
    static readonly double PI = Math.PI; // Pi, 3.1415...
    static Color clearColor; // Background color on clear
    static Color fillColor; // Fill color for shapes
    static Color strokeColor; // Stroke (outline) color for shapes
    static int frameCount; // Number of frames that ghave elapsed
    static bool hasPrintedIndex = false;

    static void Main()
    {
        // Create a window to draw to. The arguments define width and height
        Raylib.InitWindow(width, height, title);
        // Set the target frames-per-second (FPS)
        Raylib.SetTargetFPS(targetFps);
        // Setup your game. This is a function YOU define.
        Setup();
        // Loop so long as window should not close
        while (!Raylib.WindowShouldClose())
        {
            // Enable drawing to the canvas (window)
            Raylib.BeginDrawing();
            // Clear the canvas with one color
            Raylib.ClearBackground(clearColor);
            // Your game code here. This is a function YOU define.
            Update();
            // Stop drawing to the canvas, begin displaying the frame
            Raylib.EndDrawing();
        }
        // Close the window
        Raylib.CloseWindow();
    }

    /*---------------------------------------------------------------------------------------------------------------*/
    // In particular: Initialize on-start prompt messages and a system variable's value.
    /*---------------------------------------------------------------------------------------------------------------*/
    static void Setup()
    {
        //Set background clolour for first frame until refreshed by draw
        clearColor = new (0, 20, 200, 255);

        //Initialize frameCount to higher than 768 (256 * 3) to hide constellations on start-up.
        //Relates to hackish quasi-variable on line 68 (reset) and line 347 (used in fill();)
        frameCount = 1000;

        //Initial prompt messages
        Console.WriteLine();
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Click mouse to view Constellations!");
        Console.WriteLine("Press any key to view Constellation Index!");
    }

    /*---------------------------------------------------------------------------------------------------------------*/
    // Body of Interactive drawing
    /*---------------------------------------------------------------------------------------------------------------*/
    static void Update()
    {
        /*---------------------------------------------------------------------------------------------------------------*/
        // Hit any key to prompt an index for the constellations
        /*---------------------------------------------------------------------------------------------------------------*/

        if (!hasPrintedIndex && Raylib.GetKeyPressed() != 0)
        {
            Console.WriteLine("A - Corona Australis     C - Capricornus ");
            Console.WriteLine("G - Grus                 M - Microscopium");
            Console.WriteLine("P - Pisces Austrinus     S - Sagittarius ");
            hasPrintedIndex = true;
        }

        /*---------------------------------------------------------------------------------------------------------------*/
        // This is used to reset the alpha value on the constellation cross-sections using framCount as a hackish variable.
        /*---------------------------------------------------------------------------------------------------------------*/

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
            frameCount = 0;

        float mouseX = Raylib.GetMouseX();
        float mouseY = Raylib.GetMouseY();

        /*---------------------------------------------------------------------------------------------------------------*/
        // Horizon/light effect
        // When mouseY is 0, all layers are the same colour. When mouseY increases, the colours change
        // and the rects that display them move upwards.
        // The idea is to mimic staring straight up into the sky and looking towards the horizon.
        /*---------------------------------------------------------------------------------------------------------------*/

        Background(0, 20 + mouseY / 4.6, 170 + mouseY / 6);

        //Largest interactive background layer
        Fill(0, 20 + mouseY / 4, 170 + mouseY / 6);
        Rect(0, height - height / 3 - mouseY / 2, width, height);

        Fill(0, 20 + mouseY / 3.4, 170 + mouseY / 6);
        Rect(0, height - height / 9 - mouseY / 4, width, height);

        Fill(0, 20 + mouseY / 2.9, 170 + mouseY / 6);
        Rect(0, height - height / 27 - mouseY / 8, width, height);

        Fill(0, 20 + mouseY / 2.5, 170 + mouseY / 6);
        Rect(0, height - height / 81 - mouseY / 16, width, height);

        //smallest interactive background layer
        Fill(0, 20 + mouseY / 2.2, 170 + mouseY / 6);
        Rect(0, height - height / 243 - mouseY / 64, width, height);

        //Fill style for all of the stars
        //mouseY is used to weaken alpha when cursor is placed lower on the screen. Added to emphasize the above stated
        //horizon effect.
        Fill(255, 240, 220, 255 - mouseY / 3.5);

        /*---------------------------------------------------------------------------------------------------------------*/
        // Brief explanation of the following algorithm used throughout the project:
        //
        // mouseX and mouseY are inverted so that going in any direction will result in the background objects approaching
        // the cursor rather than it "running away" from it.
        //
        // mouseX and mouseY are added onto the x/y coordinates of the object so the theoretical screen size is 800 x 600,
        // rather than the actual 400 x 400. This gives a greater sense of size and space and simultaneously hides parts
        // of the sky. This is achieved by adding an extra mouseX to x (maximum 400 + up to 400, thus 800), and 1/2 of
        // mouseY to y (maximum 400 + up to 200, thus 600).
        //
        // However, since mouseX (and mouseY) are inverted, when x is, say 250, an ellipse at point 240 (240 - 250 = -10),
        // it is not visible, but a point at 260 (260 - 250 = 10), it will appear on the left side of the screen. Likewise
        // it is the same for y, but only half of mouseY is subtracted from the coordinate.
        //
        // Formatting of the algorithm is as follows:
        // (-1 * mouseX + x-coordinate, -0.5 * mouseY + y-coordinate, s-size, y-size)
        // 
        // Use the following line (uncommented) as a debug tool
        // println(mouseX, mouseY);
        /*---------------------------------------------------------------------------------------------------------------*/

        /*---------------------------------------------------------------------------------------------------------------*/
        // Constellation Specific Stars
        /*---------------------------------------------------------------------------------------------------------------*/

        //Note: All stars below are reference by their letter in my process work and later in the project.

        // Capricornus "fixed" star locations
        Ellipse(-1 * mouseX + 140, -0.5 * mouseY + 60, 5, 5); //a
        Ellipse(-1 * mouseX + 190, -0.5 * mouseY + 80, 5, 5); //b
        Ellipse(-1 * mouseX + 250, -0.5 * mouseY + 70, 5, 5); //c
        Ellipse(-1 * mouseX + 300, -0.5 * mouseY + 80, 5, 5); //d
        Ellipse(-1 * mouseX + 240, -0.5 * mouseY + 150, 5, 5); //e
        Ellipse(-1 * mouseX + 250, -0.5 * mouseY + 160, 5, 5); //f
        Ellipse(-1 * mouseX + 320, -0.5 * mouseY + 130, 5, 5); //g
        Ellipse(-1 * mouseX + 300, -0.5 * mouseY + 270, 5, 5); //h
        Ellipse(-1 * mouseX + 400, -0.5 * mouseY + 260, 5, 5); //i
        Ellipse(-1 * mouseX + 420, -0.5 * mouseY + 230, 5, 5); //j
        Ellipse(-1 * mouseX + 460, -0.5 * mouseY + 110, 5, 5); //k
        Ellipse(-1 * mouseX + 490, -0.5 * mouseY + 50, 5, 5); //l
        Ellipse(-1 * mouseX + 500, -0.5 * mouseY + 20, 5, 5); //m

        //Corona Australis "fixed" star locations
        Ellipse(-1 * mouseX + 650, -0.5 * mouseY + 460, 5, 5); //a
        Ellipse(-1 * mouseX + 650, -0.5 * mouseY + 450, 5, 5); //b
        Ellipse(-1 * mouseX + 640, -0.5 * mouseY + 440, 5, 5); //c
        Ellipse(-1 * mouseX + 630, -0.5 * mouseY + 430, 5, 5); //d
        Ellipse(-1 * mouseX + 610, -0.5 * mouseY + 430, 5, 5); //e
        Ellipse(-1 * mouseX + 590, -0.5 * mouseY + 450, 5, 5); //f
        Ellipse(-1 * mouseX + 580, -0.5 * mouseY + 480, 5, 5); //g
        Ellipse(-1 * mouseX + 580, -0.5 * mouseY + 520, 5, 5); //h
        Ellipse(-1 * mouseX + 590, -0.5 * mouseY + 560, 5, 5); //i
        Ellipse(-1 * mouseX + 610, -0.5 * mouseY + 570, 5, 5); //j

        //Grus "fixed" star locations
        Ellipse(-1 * mouseX + 160, -0.5 * mouseY + 360, 5, 5); //a
        Ellipse(-1 * mouseX + 140, -0.5 * mouseY + 390, 5, 5); //b
        Ellipse(-1 * mouseX + 140, -0.5 * mouseY + 480, 5, 5); //c
        Ellipse(-1 * mouseX + 100, -0.5 * mouseY + 450, 5, 5); //d
        Ellipse(-1 * mouseX + 30, -0.5 * mouseY + 470, 5, 5); //e
        Ellipse(-1 * mouseX + 30, -0.5 * mouseY + 500, 5, 5); //f
        Ellipse(-1 * mouseX + 90, -0.5 * mouseY + 510, 5, 5); //g
        Ellipse(-1 * mouseX + 90, -0.5 * mouseY + 540, 5, 5); //h
        Ellipse(-1 * mouseX + 50, -0.5 * mouseY + 570, 5, 5); //i

        //Microscopium "fixed" star locations
        Ellipse(-1 * mouseX + 230, -0.5 * mouseY + 470, 5, 5); //a
        Ellipse(-1 * mouseX + 240, -0.5 * mouseY + 340, 5, 5); //b
        Ellipse(-1 * mouseX + 340, -0.5 * mouseY + 360, 5, 5); //c
        Ellipse(-1 * mouseX + 370, -0.5 * mouseY + 390, 5, 5); //d

        //Pisces Austrinus "fixed" star locations
        Ellipse(-1 * mouseX + 200, -0.5 * mouseY + 230, 5, 5); //a
        Ellipse(-1 * mouseX + 200, -0.5 * mouseY + 270, 5, 5); //b
        Ellipse(-1 * mouseX + 150, -0.5 * mouseY + 270, 5, 5); //c
        Ellipse(-1 * mouseX + 70, -0.5 * mouseY + 220, 5, 5); //d
        Ellipse(-1 * mouseX + 30, -0.5 * mouseY + 250, 5, 5); //e
        Ellipse(-1 * mouseX + 50, -0.5 * mouseY + 290, 5, 5); //f
        Ellipse(-1 * mouseX + 90, -0.5 * mouseY + 283, 5, 5); //g

        //Sagittarius "fixed" star locations
        Ellipse(-1 * mouseX + 620, -0.5 * mouseY + 70, 5, 5); //a
        Ellipse(-1 * mouseX + 610, -0.5 * mouseY + 100, 5, 5); //b
        Ellipse(-1 * mouseX + 630, -0.5 * mouseY + 170, 5, 5); //c
        Ellipse(-1 * mouseX + 660, -0.5 * mouseY + 180, 5, 5); //d
        Ellipse(-1 * mouseX + 620, -0.5 * mouseY + 230, 5, 5); //e
        Ellipse(-1 * mouseX + 660, -0.5 * mouseY + 250, 5, 5); //f
        Ellipse(-1 * mouseX + 570, -0.5 * mouseY + 280, 5, 5); //g
        Ellipse(-1 * mouseX + 580, -0.5 * mouseY + 240, 5, 5); //h
        Ellipse(-1 * mouseX + 450, -0.5 * mouseY + 290, 5, 5); //i
        Ellipse(-1 * mouseX + 500, -0.5 * mouseY + 370, 5, 5); //j
        Ellipse(-1 * mouseX + 480, -0.5 * mouseY + 420, 5, 5); //k
        Ellipse(-1 * mouseX + 440, -0.5 * mouseY + 360, 5, 5); //l
        Ellipse(-1 * mouseX + 720, -0.5 * mouseY + 290, 5, 5); //m
        Ellipse(-1 * mouseX + 720, -0.5 * mouseY + 220, 5, 5); //n
        Ellipse(-1 * mouseX + 760, -0.5 * mouseY + 190, 5, 5); //o
        Ellipse(-1 * mouseX + 760, -0.5 * mouseY + 290, 5, 5); //p
        Ellipse(-1 * mouseX + 710, -0.5 * mouseY + 360, 5, 5); //q
        Ellipse(-1 * mouseX + 690, -0.5 * mouseY + 350, 5, 5); //r

        /*---------------------------------------------------------------------------------------------------------------*/
        // Filler Stars for Background 
        /*---------------------------------------------------------------------------------------------------------------*/

        //Sorry for the list of 110 stars, but I assume you won't need to look at EACH individual one. They all use the
        //algorithm used and describbed above.

        //Stars of size 3x3
        Ellipse(-1 * mouseX + 690, -0.5 * mouseY + 340, 3, 3);
        Ellipse(-1 * mouseX + 290, -0.5 * mouseY + 310, 3, 3);
        Ellipse(-1 * mouseX + 680, -0.5 * mouseY + 130, 3, 3);
        Ellipse(-1 * mouseX + 370, -0.5 * mouseY + 510, 3, 3);
        Ellipse(-1 * mouseX + 360, -0.5 * mouseY + 380, 3, 3); //5
        Ellipse(-1 * mouseX + 150, -0.5 * mouseY + 350, 3, 3);
        Ellipse(-1 * mouseX + 240, -0.5 * mouseY + 290, 3, 3);
        Ellipse(-1 * mouseX + 630, -0.5 * mouseY + 150, 3, 3);
        Ellipse(-1 * mouseX + 120, -0.5 * mouseY + 530, 3, 3);
        Ellipse(-1 * mouseX + 410, -0.5 * mouseY + 310, 3, 3); //10
        Ellipse(-1 * mouseX + 400, -0.5 * mouseY + 200, 3, 3);
        Ellipse(-1 * mouseX + 490, -0.5 * mouseY + 510, 3, 3);
        Ellipse(-1 * mouseX + 480, -0.5 * mouseY + 380, 3, 3);
        Ellipse(-1 * mouseX + 470, -0.5 * mouseY + 300, 3, 3);
        Ellipse(-1 * mouseX + 460, -0.5 * mouseY + 350, 3, 3); //15
        Ellipse(-1 * mouseX + 450, -0.5 * mouseY + 530, 3, 3);
        Ellipse(-1 * mouseX + 750, -0.5 * mouseY + 500, 3, 3);
        Ellipse(-1 * mouseX + 775, -0.5 * mouseY + 575, 3, 3);
        Ellipse(-1 * mouseX + 730, -0.5 * mouseY + 444, 3, 3);
        Ellipse(-1 * mouseX + 770, -0.5 * mouseY + 530, 3, 3); //20
        Ellipse(-1 * mouseX + 720, -0.5 * mouseY + 480, 3, 3);
        Ellipse(-1 * mouseX + 650, -0.5 * mouseY + 540, 3, 3);
        Ellipse(-1 * mouseX + 550, -0.5 * mouseY + 585, 3, 3);
        Ellipse(-1 * mouseX + 600, -0.5 * mouseY + 484, 3, 3);
        Ellipse(-1 * mouseX + 520, -0.5 * mouseY + 570, 3, 3); //25
        Ellipse(-1 * mouseX + 50, -0.5 * mouseY + 50, 3, 3);
        Ellipse(-1 * mouseX + 50, -0.5 * mouseY + 30, 3, 3);
        Ellipse(-1 * mouseX + 175, -0.5 * mouseY + 70, 3, 3);
        Ellipse(-1 * mouseX + 30, -0.5 * mouseY + 40, 3, 3);
        Ellipse(-1 * mouseX + 170, -0.5 * mouseY + 30, 3, 3); //30
        Ellipse(-1 * mouseX + 20, -0.5 * mouseY + 80, 3, 3);
        Ellipse(-1 * mouseX + 250, -0.5 * mouseY + 40, 3, 3);
        Ellipse(-1 * mouseX + 50, -0.5 * mouseY + 80, 3, 3);
        Ellipse(-1 * mouseX + 100, -0.5 * mouseY + 80, 3, 3);
        Ellipse(-1 * mouseX + 20, -0.5 * mouseY + 170, 3, 3); //35
        Ellipse(-1 * mouseX + 50, -0.5 * mouseY + 250, 3, 3);
        Ellipse(-1 * mouseX + 50, -0.5 * mouseY + 330, 3, 3);
        Ellipse(-1 * mouseX + 375, -0.5 * mouseY + 370, 3, 3);
        Ellipse(-1 * mouseX + 230, -0.5 * mouseY + 240, 3, 3);
        Ellipse(-1 * mouseX + 170, -0.5 * mouseY + 130, 3, 3); //40
        Ellipse(-1 * mouseX + 420, -0.5 * mouseY + 180, 3, 3);
        Ellipse(-1 * mouseX + 550, -0.5 * mouseY + 240, 3, 3);
        Ellipse(-1 * mouseX + 550, -0.5 * mouseY + 180, 3, 3);
        Ellipse(-1 * mouseX + 200, -0.5 * mouseY + 80, 3, 3);
        Ellipse(-1 * mouseX + 320, -0.5 * mouseY + 70, 3, 3); //45
        Ellipse(-1 * mouseX + 420, -0.5 * mouseY + 80, 3, 3);
        Ellipse(-1 * mouseX + 520, -0.5 * mouseY + 40, 3, 3);
        Ellipse(-1 * mouseX + 650, -0.5 * mouseY + 80, 3, 3);
        Ellipse(-1 * mouseX + 700, -0.5 * mouseY + 80, 3, 3);
        Ellipse(-1 * mouseX + 720, -0.5 * mouseY + 70, 3, 3); //50

        //Stars of size 4x4
        Ellipse(-1 * mouseX + 210, -0.5 * mouseY + 210, 4, 4);
        Ellipse(-1 * mouseX + 210, -0.5 * mouseY + 210, 4, 4);
        Ellipse(-1 * mouseX + 650, -0.5 * mouseY + 30, 4, 4);
        Ellipse(-1 * mouseX + 320, -0.5 * mouseY + 570, 4, 4);
        Ellipse(-1 * mouseX + 180, -0.5 * mouseY + 450, 4, 4);
        Ellipse(-1 * mouseX + 210, -0.5 * mouseY + 480, 4, 4); //5
        Ellipse(-1 * mouseX + 690, -0.5 * mouseY + 350, 4, 4);
        Ellipse(-1 * mouseX + 110, -0.5 * mouseY + 330, 4, 4);
        Ellipse(-1 * mouseX + 410, -0.5 * mouseY + 510, 4, 4);
        Ellipse(-1 * mouseX + 450, -0.5 * mouseY + 430, 4, 4);
        Ellipse(-1 * mouseX + 420, -0.5 * mouseY + 310, 4, 4); //10
        Ellipse(-1 * mouseX + 420, -0.5 * mouseY + 270, 4, 4);
        Ellipse(-1 * mouseX + 480, -0.5 * mouseY + 150, 4, 4);
        Ellipse(-1 * mouseX + 410, -0.5 * mouseY + 80, 4, 4);
        Ellipse(-1 * mouseX + 490, -0.5 * mouseY + 550, 4, 4);
        Ellipse(-1 * mouseX + 410, -0.5 * mouseY + 430, 4, 4); //15
        Ellipse(-1 * mouseX + 400, -0.5 * mouseY + 30, 4, 4);
        Ellipse(-1 * mouseX + 410, -0.5 * mouseY + 110, 4, 4);
        Ellipse(-1 * mouseX + 420, -0.5 * mouseY + 560, 4, 4);
        Ellipse(-1 * mouseX + 440, -0.5 * mouseY + 470, 4, 4);
        Ellipse(-1 * mouseX + 380, -0.5 * mouseY + 410, 4, 4); //20
        Ellipse(-1 * mouseX + 480, -0.5 * mouseY + 320, 4, 4);
        Ellipse(-1 * mouseX + 360, -0.5 * mouseY + 330, 4, 4);
        Ellipse(-1 * mouseX + 290, -0.5 * mouseY + 560, 4, 4);
        Ellipse(-1 * mouseX + 320, -0.5 * mouseY + 430, 4, 4);
        Ellipse(-1 * mouseX + 470, -0.5 * mouseY + 340, 4, 4); //25
        Ellipse(-1 * mouseX + 500, -0.5 * mouseY + 260, 4, 4);
        Ellipse(-1 * mouseX + 525, -0.5 * mouseY + 155, 4, 4);
        Ellipse(-1 * mouseX + 200, -0.5 * mouseY + 500, 4, 4);
        Ellipse(-1 * mouseX + 190, -0.5 * mouseY + 550, 4, 4);
        Ellipse(-1 * mouseX + 100, -0.5 * mouseY + 575, 4, 4); //30
        Ellipse(-1 * mouseX + 80, -0.5 * mouseY + 120, 4, 4);
        Ellipse(-1 * mouseX + 60, -0.5 * mouseY + 130, 4, 4);
        Ellipse(-1 * mouseX + 90, -0.5 * mouseY + 220, 4, 4);
        Ellipse(-1 * mouseX + 120, -0.5 * mouseY + 400, 4, 4);
        Ellipse(-1 * mouseX + 270, -0.5 * mouseY + 440, 4, 4); //35
        Ellipse(-1 * mouseX + 100, -0.5 * mouseY + 460, 4, 4);
        Ellipse(-1 * mouseX + 225, -0.5 * mouseY + 555, 4, 4);
        Ellipse(-1 * mouseX + 200, -0.5 * mouseY + 300, 4, 4);
        Ellipse(-1 * mouseX + 170, -0.5 * mouseY + 250, 4, 4);
        Ellipse(-1 * mouseX + 130, -0.5 * mouseY + 490, 4, 4); //40
        Ellipse(-1 * mouseX + 80, -0.5 * mouseY + 120, 4, 4);
        Ellipse(-1 * mouseX + 600, -0.5 * mouseY + 130, 4, 4);
        Ellipse(-1 * mouseX + 590, -0.5 * mouseY + 420, 4, 4);
        Ellipse(-1 * mouseX + 420, -0.5 * mouseY + 490, 4, 4);
        Ellipse(-1 * mouseX + 700, -0.5 * mouseY + 540, 4, 4); //45
        Ellipse(-1 * mouseX + 660, -0.5 * mouseY + 460, 4, 4);
        Ellipse(-1 * mouseX + 525, -0.5 * mouseY + 550, 4, 4);
        Ellipse(-1 * mouseX + 570, -0.5 * mouseY + 300, 4, 4);
        Ellipse(-1 * mouseX + 670, -0.5 * mouseY + 250, 4, 4);
        Ellipse(-1 * mouseX + 680, -0.5 * mouseY + 200, 4, 4); //50

        //Stars of Size 5x5
        Ellipse(-1 * mouseX + 780, -0.5 * mouseY + 30, 5, 5);
        Ellipse(-1 * mouseX + 450, -0.5 * mouseY + 210, 5, 5);
        Ellipse(-1 * mouseX + 720, -0.5 * mouseY + 190, 5, 5);
        Ellipse(-1 * mouseX + 130, -0.5 * mouseY + 570, 5, 5);
        Ellipse(-1 * mouseX + 10, -0.5 * mouseY + 480, 5, 5); //5
        Ellipse(-1 * mouseX + 600, -0.5 * mouseY + 360, 5, 5);
        Ellipse(-1 * mouseX + 550, -0.5 * mouseY + 370, 5, 5);
        Ellipse(-1 * mouseX + 450, -0.5 * mouseY + 270, 5, 5);
        Ellipse(-1 * mouseX + 700, -0.5 * mouseY + 290, 5, 5);
        Ellipse(-1 * mouseX + 650, -0.5 * mouseY + 310, 5, 5); //10

        /*---------------------------------------------------------------------------------------------------------------*/
        // Constellation Cross-Sections
        /*---------------------------------------------------------------------------------------------------------------*/

        //Note: I'm using frameCount to fade out lines. Alpha is reset when mousePressed is activated.
        //Info: (See 'void mousePressed')

        //Colour: White. Alpha is calculated by:
        // 255 (max opacity) - frameCount * (larger number = faster fade out |or| lower number = slower fade out)
        Stroke(255, 255 - frameCount * 2.75);

        //Prevent triangles and quads from being filled in.
        NoFill();

        //Note: The letters used in comments refer to those assigned in the listings for each constellation above.

        //Lines that form the Capricornus constellation
        Line(-1 * mouseX + 140, -0.5 * mouseY + 60, -1 * mouseX + 190, -0.5 * mouseY + 80); //a - b
        Line(-1 * mouseX + 190, -0.5 * mouseY + 80, -1 * mouseX + 250, -0.5 * mouseY + 70); //b - c
        Line(-1 * mouseX + 250, -0.5 * mouseY + 70, -1 * mouseX + 300, -0.5 * mouseY + 80); //c - d
        Line(-1 * mouseX + 250, -0.5 * mouseY + 70, -1 * mouseX + 240, -0.5 * mouseY + 150); //c - e
        Line(-1 * mouseX + 300, -0.5 * mouseY + 80, -1 * mouseX + 250, -0.5 * mouseY + 160); //d - f
        Line(-1 * mouseX + 320, -0.5 * mouseY + 130, -1 * mouseX + 300, -0.5 * mouseY + 270); //g - h
        Line(-1 * mouseX + 460, -0.5 * mouseY + 110, -1 * mouseX + 420, -0.5 * mouseY + 230); //k - j
        Line(-1 * mouseX + 420, -0.5 * mouseY + 230, -1 * mouseX + 400, -0.5 * mouseY + 260); //j - i
        Line(-1 * mouseX + 490, -0.5 * mouseY + 50, -1 * mouseX + 500, -0.5 * mouseY + 20); //l - m

        Quad(-1 * mouseX + 300, -0.5 * mouseY + 80, -1 * mouseX + 320, -0.5 * mouseY + 130,
             -1 * mouseX + 460, -0.5 * mouseY + 110, -1 * mouseX + 490, -0.5 * mouseY + 50); // d - g - k - l


        //Lines that form the Corona Australis constellation
        Line(-1 * mouseX + 650, -0.5 * mouseY + 460, -1 * mouseX + 650, -0.5 * mouseY + 450); //a - b
        Line(-1 * mouseX + 650, -0.5 * mouseY + 450, -1 * mouseX + 630, -0.5 * mouseY + 430); //b - c - d
        Line(-1 * mouseX + 630, -0.5 * mouseY + 430, -1 * mouseX + 610, -0.5 * mouseY + 430); //d - e
        Line(-1 * mouseX + 610, -0.5 * mouseY + 430, -1 * mouseX + 590, -0.5 * mouseY + 450); //e - f
        Line(-1 * mouseX + 590, -0.5 * mouseY + 450, -1 * mouseX + 580, -0.5 * mouseY + 480); //f - g
        Line(-1 * mouseX + 580, -0.5 * mouseY + 480, -1 * mouseX + 580, -0.5 * mouseY + 520); //g - h
        Line(-1 * mouseX + 580, -0.5 * mouseY + 520, -1 * mouseX + 590, -0.5 * mouseY + 560); //h - i
        Line(-1 * mouseX + 590, -0.5 * mouseY + 560, -1 * mouseX + 610, -0.5 * mouseY + 570); //i - j


        //Lines that form the Grus constellation
        Line(-1 * mouseX + 160, -0.5 * mouseY + 360, -1 * mouseX + 140, -0.5 * mouseY + 390); //a - b
        Line(-1 * mouseX + 140, -0.5 * mouseY + 390, -1 * mouseX + 140, -0.5 * mouseY + 480); //b - c
        Line(-1 * mouseX + 140, -0.5 * mouseY + 480, -1 * mouseX + 100, -0.5 * mouseY + 450); //c - d
        Line(-1 * mouseX + 100, -0.5 * mouseY + 450, -1 * mouseX + 30, -0.5 * mouseY + 470); //d - e
        Line(-1 * mouseX + 30, -0.5 * mouseY + 470, -1 * mouseX + 30, -0.5 * mouseY + 500); //e - f
        Line(-1 * mouseX + 30, -0.5 * mouseY + 500, -1 * mouseX + 90, -0.5 * mouseY + 510); //f - g
        Line(-1 * mouseX + 90, -0.5 * mouseY + 510, -1 * mouseX + 140, -0.5 * mouseY + 480); //g - c
        Line(-1 * mouseX + 90, -0.5 * mouseY + 510, -1 * mouseX + 90, -0.5 * mouseY + 540); //g - h
        Line(-1 * mouseX + 90, -0.5 * mouseY + 540, -1 * mouseX + 50, -0.5 * mouseY + 570); //h - i


        //Lines that form the Microsopium constellation
        Line(-1 * mouseX + 230, -0.5 * mouseY + 470, -1 * mouseX + 240, -0.5 * mouseY + 340); //a - b
        Line(-1 * mouseX + 240, -0.5 * mouseY + 340, -1 * mouseX + 340, -0.5 * mouseY + 360); //b - c
        Line(-1 * mouseX + 340, -0.5 * mouseY + 360, -1 * mouseX + 370, -0.5 * mouseY + 390); //c - d


        //Lines that form the Pisces Austrinus constellation
        Triangle(-1 * mouseX + 200, -0.5 * mouseY + 230,
                 -1 * mouseX + 200, -0.5 * mouseY + 270,
                 -1 * mouseX + 150, -0.5 * mouseY + 270); ///a - b - c

        Quad(-1 * mouseX + 150, -0.5 * mouseY + 270, -1 * mouseX + 70, -0.5 * mouseY + 220,
             -1 * mouseX + 30, -0.5 * mouseY + 250, -1 * mouseX + 50, -0.5 * mouseY + 290); //c - d - e - f - g


        //Lines that form the Sagittarius constellation
        Line(-1 * mouseX + 620, -0.5 * mouseY + 70, -1 * mouseX + 610, -0.5 * mouseY + 100); //a - b
        Line(-1 * mouseX + 610, -0.5 * mouseY + 100, -1 * mouseX + 630, -0.5 * mouseY + 170); //b - c
        Line(-1 * mouseX + 660, -0.5 * mouseY + 250, -1 * mouseX + 720, -0.5 * mouseY + 290); //f - m
        Line(-1 * mouseX + 450, -0.5 * mouseY + 290, -1 * mouseX + 440, -0.5 * mouseY + 360); //i - l
        Line(-1 * mouseX + 500, -0.5 * mouseY + 370, -1 * mouseX + 480, -0.5 * mouseY + 420); //j - k

        Triangle(-1 * mouseX + 630, -0.5 * mouseY + 170,
                 -1 * mouseX + 660, -0.5 * mouseY + 180,
                 -1 * mouseX + 620, -0.5 * mouseY + 230); //c - d - e

        Quad(-1 * mouseX + 620, -0.5 * mouseY + 230, -1 * mouseX + 660, -0.5 * mouseY + 250,
             -1 * mouseX + 570, -0.5 * mouseY + 280, -1 * mouseX + 580, -0.5 * mouseY + 240); //e - f - g - h

        Quad(-1 * mouseX + 570, -0.5 * mouseY + 280, -1 * mouseX + 580, -0.5 * mouseY + 240,
             -1 * mouseX + 450, -0.5 * mouseY + 290, -1 * mouseX + 500, -0.5 * mouseY + 370); //g - h - i - j

        Quad(-1 * mouseX + 720, -0.5 * mouseY + 290, -1 * mouseX + 720, -0.5 * mouseY + 220,
             -1 * mouseX + 760, -0.5 * mouseY + 190, -1 * mouseX + 760, -0.5 * mouseY + 290); //m - n - o - p

        Quad(-1 * mouseX + 720, -0.5 * mouseY + 290, -1 * mouseX + 760, -0.5 * mouseY + 290,
             -1 * mouseX + 710, -0.5 * mouseY + 360, -1 * mouseX + 690, -0.5 * mouseY + 350); //m - p - q - r

        /*---------------------------------------------------------------------------------------------------------------*/
        // Letters for Each Constellation - Index
        /*---------------------------------------------------------------------------------------------------------------*/

        //Note: Decided not to use text() and instead get creative with lines and arcs.

        //Corona Australis A made from lines     
        Line(-1 * mouseX + 610, -0.5 * mouseY + 510, -1 * mouseX + 620, -0.5 * mouseY + 490); //Left / of A
        Line(-1 * mouseX + 620, -0.5 * mouseY + 490, -1 * mouseX + 630, -0.5 * mouseY + 510); //Right \ of A
        Line(-1 * mouseX + 613, -0.5 * mouseY + 505, -1 * mouseX + 627, -0.5 * mouseY + 505); //Central - of A

        //Caprincornus C made from arc
        Arc(-1 * mouseX + 380, -0.5 * mouseY + 150, 20, 20, PI / 4, PI + PI * 3 / 4); //Forms C shape  

        //Grus G made from arc and line
        Arc(-1 * mouseX + 70, -0.5 * mouseY + 400, 20, 20, PI / 16, PI + PI * 3 / 4);       //C part of G
        Line(-1 * mouseX + 79, -0.5 * mouseY + 401, -1 * mouseX + 73, -0.5 * mouseY + 401); //- part of G

        //Microscopium M made from lines
        Line(-1 * mouseX + 280, -0.5 * mouseY + 400, -1 * mouseX + 280, -0.5 * mouseY + 380); //Left | of M
        Line(-1 * mouseX + 280, -0.5 * mouseY + 380, -1 * mouseX + 290, -0.5 * mouseY + 390); //Left \ in middle of M
        Line(-1 * mouseX + 290, -0.5 * mouseY + 390, -1 * mouseX + 300, -0.5 * mouseY + 380); //Right / in middle of M
        Line(-1 * mouseX + 300, -0.5 * mouseY + 400, -1 * mouseX + 300, -0.5 * mouseY + 380); //Right | of M

        //Pisces P made from arc and lines
        Arc(-1 * mouseX + 120, -0.5 * mouseY + 180.25, 10, 10.5, 1.5 * PI, 2.5 * PI);         //P curve )
        Line(-1 * mouseX + 120, -0.5 * mouseY + 175, -1 * mouseX + 110, -0.5 * mouseY + 175); //P top of loop -
        Line(-1 * mouseX + 120, -0.5 * mouseY + 185, -1 * mouseX + 110, -0.5 * mouseY + 185); //P bottom of loop -
        Line(-1 * mouseX + 110, -0.5 * mouseY + 175, -1 * mouseX + 110, -0.5 * mouseY + 195); //P left side |

        //Sagittarius S made from arcs ans lines
        Arc(-1 * mouseX + 630, -0.5 * mouseY + 300, 12, 12, PI / 2, 7 / 3 * PI); //Upper S curve
        Arc(-1 * mouseX + 630, -0.5 * mouseY + 313, 14, 14, PI / 2 * -1, PI); //Lower S curve

        /*---------------------------------------------------------------------------------------------------------------*/
        // Increment frame counter, used for alpha effect
        /*---------------------------------------------------------------------------------------------------------------*/
        frameCount++;
    }


    /*---------------------------------------------------------------------------------------------------------------*/
    // "Ported" functions from Processing
    //  These are only approximations of the functions to get the sketch working
    /*---------------------------------------------------------------------------------------------------------------*/

    static void Background(double r, double g, double b, double a = 255)
    {
        clearColor = new Color((int)r, (int)g, (int)b, (int)a);
    }

    static void Fill(double r, double g, double b, double a = 255)
    {
        int br = (int)Math.Clamp(r, 0, 255);
        int bg = (int)Math.Clamp(g, 0, 255);
        int bb = (int)Math.Clamp(b, 0, 255);
        int ba = (int)Math.Clamp(a, 0, 255);
        fillColor = new Color(br, bg, bb, ba);
    }
    static void NoFill()
    {
        fillColor = new Color(0, 0, 0, 0);
    }

    static void Stroke(double i, double a = 255)
    {
        int intensity = (int)Math.Clamp(i, 0, 255);
        int alpha = (int)Math.Clamp(a, 0, 255);
        strokeColor = new Color(intensity, intensity, intensity, alpha);
    }

    static void Rect(float x, float y, float w, float h)
    {
        Rectangle rect = new Rectangle(x, y, w, h);
        Raylib.DrawRectangleRec(rect, fillColor);
    }

    static void Line(double x1, double y1, double x2, double y2)
    {
        Raylib.DrawLine((int)x1, (int)y1, (int)x2, (int)y2, strokeColor);
    }
    static void Ellipse(double x, double y, double xr, double yr)
    {
        xr /= 2;
        yr /= 2;
        Raylib.DrawEllipse((int)x, (int)y, (int)xr, (int)yr, fillColor);
        //Raylib.DrawEllipseLines((int)x, (int)y, (int)xr, (int)yr, strokeColor);
    }
    static void Quad(double x1, double y1, double x2, double y2, double x3, double y3, double x4, double y4)
    {
        Vector2[] points =
        [
            new Vector2((float)x1, (float)y1),
            new Vector2((float)x2, (float)y2),
            new Vector2((float)x3, (float)y3),
            new Vector2((float)x4, (float)y4),
            new Vector2((float)x1, (float)y1),
        ];
        Raylib.DrawLineStrip(points, points.Length, strokeColor);
    }
    static void Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
    {
        Vector2[] points =
        [
            new Vector2((float)x1, (float)y1),
            new Vector2((float)x2, (float)y2),
            new Vector2((float)x3, (float)y3),
            new Vector2((float)x1, (float)y1),
        ];
        Raylib.DrawLineStrip(points, points.Length, strokeColor);
    }
    static void Arc(double x, double y, double xr, double yr, double angleStart, double angleEnd)
    {
        Vector2 centre = new Vector2((float)x, (float)y);
        float xRadius = (float)(xr / 2);
        float yRadius = (float)(yr / 2);
        float angleRange = (float)(angleEnd - angleStart);

        Vector2[] points = new Vector2[90];
        for (int i = 0; i < points.Length; i++)
        {
            float percentage = (float)i / (points.Length - 1);
            float angle = (angleRange * percentage) + (float)angleStart;

            Vector2 position = centre;
            position.X += xRadius * MathF.Cos(angle);
            position.Y += yRadius * MathF.Sin(angle);

            points[i] = position;
        }

        Raylib.DrawLineStrip(points, points.Length, strokeColor);
    }
}