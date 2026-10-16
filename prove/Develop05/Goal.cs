using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

public abstract class Goal
{
    private string _name;
    private string _description;
    private double _points;

    public Goal(string name, string description, double points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }

    public double GetPoints()
    {
        return _points;
    }

    public abstract double Complete();
    public abstract bool isCompleted();

    public virtual string Save()
    {
        return $"{_name}|{_description}|{_points}";
    }

    public virtual void Display()
    {
        Console.Write($"{_name}");
        if (this.isCompleted())
        {
            Console.WriteLine($" [X]");
        }
        else
        {
            Console.WriteLine($" [ ]");
        }
    }

    public virtual void DisplayDetails()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine($"Description: {_description}");
        Console.WriteLine($"Value: {_points}");
    }
}
