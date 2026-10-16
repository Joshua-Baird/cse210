using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

public class EternalGoal : RepeatableGoal
{
    public EternalGoal(string name, string description, double points)
        : base(name, description, points) { }

    public EternalGoal(
        string name,
        string description,
        double points,
        int timesCompleted
    )
        : base(name, description, points, timesCompleted) { }

    public override double Complete()
    {
        _numCompleted++;
        return base.GetPoints();
    }

    public override bool isCompleted()
    {
        return false;
    }

    public override string Save()
    {
        return $"eternal|{base.Save()}";
    }
}
