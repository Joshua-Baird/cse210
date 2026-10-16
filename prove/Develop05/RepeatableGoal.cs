public abstract class RepeatableGoal : Goal
{
    protected int _numCompleted;

    public RepeatableGoal(string name, string description, double points)
        : base(name, description, points)
    {
        _numCompleted = 0;
    }

    public RepeatableGoal(
        string name,
        string description,
        double points,
        int timesCompleted
    )
        : base(name, description, points)
    {
        _numCompleted = timesCompleted;
    }

    public string Count()
    {
        return $"you have completed {base.GetName()}, {_numCompleted} time(s)";
    }

    public override string Save()
    {
        return $"{base.Save()}|{_numCompleted}";
    }
}
