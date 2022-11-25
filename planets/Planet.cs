using System.Numerics;

public class Planet {
    public double X { get; set; }
    public double Y { get; set; }

    public int Radius { get; set; }
    public double Mass { get; set; }

    public double xVel { get; set; }
    public double yVel { get; set; }

    public Planet(double _x, double _y, int _radius, double _mass) {
        X = _x;
        Y = _y;
        Radius = _radius;
        Mass = _mass;

        xVel = 0;
        yVel = 0;
    }

    public Vector2 Attraction(Planet other) {
		var DistanceX = other.X - X;
		var DistanceY = other.Y - Y;
		double Distance = Math.Sqrt(Math.Pow(DistanceX, 2) + Math.Pow(DistanceY, 2));

		double Force = Const.G * Mass * other.Mass / Math.Pow(Distance, 2);
		double Theta = Math.Atan2(DistanceY, DistanceX);
		double ForceX = Math.Cos(Theta) * Force;
		double ForceY = Math.Sin(Theta) * Force;

		return new Vector2((float)ForceX, (float)ForceY);
    }

    public void Update(Planet other) {
        var Force = Attraction(other);

        xVel += Force.X / Mass * Const.TIMESTEP;
        yVel += Force.Y / Mass * Const.TIMESTEP;

        X += xVel * Const.TIMESTEP;
        Y += yVel * Const.TIMESTEP;
    }

    public Vector2 ScaledPosition() {
        var x = X * Const.SCALE + Const.WIDTH / 2;
        var y = Y * Const.SCALE + Const.HEIGHT / 2;

        return new Vector2((int)x, (int)y);
    }

    public static void UpdatePlanets(List<Planet> planets) {
        foreach (var planet1 in planets) {
            foreach (var planet2 in planets) {
                if (planet1 == planet2) {
                    continue;
                }
                planet1.Update(planet2);
            }
        }
    }
}