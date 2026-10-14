
public class Circle : Shape
{
    double _radius; public Circle(double radius, string color) : base(color)
    {
        _radius = radius;
    }
    public override double GetArea()
    {
        return _radius / 2 * Math.PI;
    }
}