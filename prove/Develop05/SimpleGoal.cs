using System.Drawing;

public class SimpleGoal : Goal
{
    private bool _isCompleted;

    public SimpleGoal(string name, string description, double points)
        : base(name, description, points)
    {
        _isCompleted = false;
    }

    public SimpleGoal(
        string name,
        string description,
        double points,
        bool isCompleted
    )
        : base(name, description, points)
    {
        _isCompleted = isCompleted;
    }

    public override double Complete()
    {
        _isCompleted = true;
        return base.GetPoints();
    }

    public override bool isCompleted()
    {
        return _isCompleted;
    }

    public override string Save()
    {
        return $"simple|{base.Save()}|{_isCompleted}";
    }
}
