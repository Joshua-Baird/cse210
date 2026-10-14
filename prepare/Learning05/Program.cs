using System;

class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = [new Square(5, "blue"), new Rectangle(5, 9, "red"), new Circle(201, "green")];
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Color: {shape.GetColor()} Area: {shape.GetArea()}");
        }
    }
}