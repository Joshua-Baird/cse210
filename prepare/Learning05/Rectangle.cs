
public class Rectangle : Shape
{
    double _length;
    double _width;
    public Rectangle(double lenght, double width, string color) : base(color)
    {
        _length = lenght;
        _width = width;
    }
    public override double GetArea()
    {
        return _length * _width;
    }
}