// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            // set title
            Window.SetTitle("Picture Frame");
            // set canvas size
            Window.SetSize(400, 400);
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            // turn off outline
            Draw.SetLineSize(0);
            Draw.SetLineColor(255, 0, 0);
            //Draw.SetLineSize(1);
            // ^^ Turn this on to see the meat n' bones!

            ///////////////////
            // STATIC IMAGES //
            ///////////////////

            // set background to red
            Window.ClearBackground(125, 6, 6); // dark red

            // darker red rectangles (wall stripes)
            Draw.SetFillColor(115, 6, 6); // darker red
            Draw.Rectangle(40, 0, 80, 400);
            Draw.Rectangle(200, 0, 80, 400);
            Draw.Rectangle(360, 0, 80, 400);

            // transparent black ellipse (shadow)
            Draw.SetFillColor(0, 0, 0, 50); // alpha'd black
            Draw.Ellipse(200, 210, 280, 320);

            // brown ellipse (picture frame)
            Draw.SetFillColor(158, 107, 76); // brown
            Draw.Ellipse(200, 200, 280, 320);

            // dark brown ellipse (inner picture frame)
            Draw.SetFillColor(138, 87, 56); // dark brown
            Draw.Ellipse(200, 200, 270, 310);

            // darker brown ellipse (inner-inner picture frame)
            Draw.SetFillColor(128, 77, 46); // darker brown
            Draw.Ellipse(200, 200, 235, 275);

            // white ellipse (picture background)
            Draw.SetFillColor(255, 255, 255); //white
            Draw.Ellipse(200, 200, 220, 260);

            //////////////////
            // MOVING STUFF //
            //////////////////

            /* Because each shape moves a fixed amount, we can use this formula to move each shape:
            starting position + (mouseX / screen width / distance travelled)

            "distance travelled" is usually:
            screen width - 2 * (starting position)

            I decided to manually calculate (screen width / distance travelled) because otherwise it rounded weird.
            There's probably a better way of fixing this but I don't know what that is (I am dumb)
            */

            // set color to black
            Draw.SetFillColor(0);

            int mouseX = Input.GetMouseX();

            // quad (base)
            // Transforms along x and y axes
            Draw.Quad(
            180 + mouseX / -11.42857f, 290 + mouseX / -13.33333f,
            // -11.42857 = 400/-35
            // -13.33333 = 400/-30
            175 + mouseX / -11.42857f, 300,
            // -11.42857 = 400/-35
            260 + mouseX / -11.42857f, 300,
            // -11.42857 = 400/-35
            255 + mouseX / -11.42857f, 260 + mouseX / 13.33333f
            // -11.42857 = 400/-35
            // 13.33333 = 400/30
            );

            // quad (neck)
            // Transforms along x and y axes
            Draw.Quad(
            180 + mouseX / -40, 210 + mouseX / -20, // top left
            // -40 = 400/-10
            // -20 = 400/-20
            200 + mouseX / -7.69231f, 290 + mouseX / -14.28571f, // bottom left
            // -7.69231 = 400/-52
            // -14.28571 = 400/-28
            252 + mouseX / -7.69231f, 262 + mouseX / 14.28571f, // bottom right
            // -7.69231 = 400/-52
            // 14.28571 = 400/28
            230 + mouseX / -40, 190 + mouseX / 20 // top right
            // -40 = 400/-10
            // 20 = 400/20
            );

            // ellipse (noggin)
            // Moves along the x axis, 185 to 215
            Draw.Ellipse(185 + mouseX / (400 / 30), 180, 120, 120);

            // quad (jaw)
            // Transforms along x and y axes
            Draw.Quad(
            125 + mouseX / 13.33333f, 180, // top left
            // 13.33333 = 400/30
            160 + mouseX / 20, 260 - mouseX / 40, // bottom left
            // 20 = 400/20
            // 40 = 400/10
            220 + mouseX / 20, 250 + mouseX / 40, // bottom right
            // 20 = 400/20
            // 40 = 400/10
            245 + mouseX / 13.33333f, 180 // top right
            // 13.33333 = 400/30
            );
            // ellipse (chin)
            // Moves along the x axis, 160 to 240
            Draw.Ellipse(160 + mouseX / (400 / 80), 250, 20, 20);

            // triangle + ellipse (nose)
            // Moves along the x axis, 130 to 270
            Draw.Triangle(
            130 + mouseX / 2.85714f, 200, // top corner
            // 2.85714 = 400/140
            120 + mouseX / 2.85714f, 220, // left corner
            // 2.85714 = 400/140
            140 + mouseX / 2.85714f, 220  // right corner
            // 2.85714 = 400/140
            );
            Draw.Ellipse(130 + mouseX / 2.85714f, 220, 20, 10);

            // triangle + ellipse (upper lip)
            // Moves along the x axis, 142 to 258
            Draw.Triangle(
            142 + mouseX / 3.44828f, 215, // top corner
            // 3.44828 = 400/116
            137 + mouseX / 3.44828f, 230, // left corner
            // 3.44828 = 400/116
            147 + mouseX / 3.44828f, 230  // right corner
            // 3.44828 = 400/116
            );
            Draw.Ellipse(142 + mouseX / 3.44828f, 230, 10, 5);

            // triangle (lower lip)
            // Moves along the x axis, 145 to 255
            Draw.Triangle(
            144 + mouseX / 3.57143f, 240, // left corner
            // 3.57143 = 400/112
            145 + mouseX / 3.63636f, 220, // top corner
            // 3.63636 = 400/110
            155 + mouseX / 4.44444f, 245  // bottom corner
            // 4.44444 = 400/90
            );

            // ellipses (collar)
            Draw.Ellipse(235 + mouseX / -13.33333f, 260 + mouseX / 10, 39, 25);
            // -13.33333 = 400/-30
            // 10 = 400/40
            Draw.Ellipse(195 + mouseX / -13.33333f, 300 + mouseX / -10, 39, 25);
            // -13.33333 = 400/-30
            // 10 = 400/40

            // ellipses (hair)
            Draw.Ellipse(210 + mouseX / -20, 130, 40, 30);
            // -20 = 400/-20
            Draw.Ellipse(230 + mouseX / -6.66667f, 160, 60, 60);
            // -6.66667 = 400/-60
            Draw.Ellipse(170 + mouseX / 6.66667f, 150, 90, 80);
            // 6.66667 = 400/-60

            // white rectangle (negative space)
            Draw.SetFillColor(255, 255, 255);
            Draw.Rectangle(160, 300, 80, 20);

            // ellipse (shoulder)
            Draw.Ellipse(240 + mouseX / 40, 300,
            55 - mouseX / 10, 20);
            Draw.Ellipse(150 + mouseX / 40, 300,
            15 + mouseX / 10, 20);
        }
    }

}
