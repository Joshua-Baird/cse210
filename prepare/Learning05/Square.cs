public class Square : Shape
{
    double _side;
    public Square(double side, string color) : base(color)
    {
        _side = side;
    }
    public override double GetArea()
    {
        return _side * _side;
    }
}