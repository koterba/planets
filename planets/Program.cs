using System.Collections.Generic;
using System.Numerics;
// using static Raylib_cs.MouseButton;

using Raylib_cs;


static class Program
{
    public static void Main()
    {
        Raylib.InitWindow(600, 600, "Hello World");
        Raylib.SetTargetFPS(144);

        var sun = new Planet(0, 0, 30, 1.98892 * Math.Pow(10, 30));
        var earth = new Planet(-1 * Const.AU, 0, 16, 5.9742 * Math.Pow(10, 24));
        earth.yVel = 29.783 * 600;

        List<Planet> planets = new List<Planet> {sun, earth};
        List<Vector2> trails = new List<Vector2>();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            // draw trails
            foreach (var trail in trails) {
                Raylib.DrawCircle((int)trail.X, (int)trail.Y, 1.0f, Color.WHITE);
            }

            var sunPos = sun.ScaledPosition();
            Raylib.DrawCircle((int)sunPos.X, (int)sunPos.Y, sun.Radius, Color.YELLOW);

            var earthPos = earth.ScaledPosition();
            Raylib.DrawCircle((int)earthPos.X, (int)earthPos.Y, earth.Radius, Color.BLUE);

            Planet.UpdatePlanets(planets);

            // cap points to 300
            if (trails.Count > 200) {
                trails.RemoveAt(trails.Count-1);
            }
            trails.Insert(0, new Vector2(earthPos.X, earthPos.Y));


            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
