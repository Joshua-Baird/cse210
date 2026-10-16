public class ChecklistGoal : RepeatableGoal
{
    private int _repeatability;
    private bool _isCompleted;
    private double _bonusPoints;

    public ChecklistGoal(
        string name,
        string description,
        double points,
        int repeatability
    )
        : base(name, description, points)
    {
        _repeatability = repeatability;
        _isCompleted = false;
        _bonusPoints = 10;
    }

    public ChecklistGoal(
        string name,
        string description,
        double points,
        int repeatability,
        int timesCompleted
    )
        : base(name, description, points, timesCompleted)
    {
        if (repeatability <= timesCompleted)
        {
            _isCompleted = true;
        }
        else
        {
            _isCompleted = false;
        }
        _repeatability = repeatability;
        _bonusPoints = 10;
    }

    public ChecklistGoal(
        string name,
        string description,
        double points,
        int repeatability,
        int timesCompleted,
        double bonusPoints
    )
        : base(name, description, points, timesCompleted)
    {
        if (repeatability <= timesCompleted)
        {
            _isCompleted = true;
        }
        else
        {
            _isCompleted = false;
        }
        _repeatability = repeatability;
        _bonusPoints = bonusPoints;
    }

    public override double Complete()
    {
        _numCompleted++;
        if (_numCompleted >= _repeatability)
        {
            _isCompleted = true;
            double totalPoints = base.GetPoints() + _bonusPoints;
            return totalPoints;
        }
        else
        {
            return base.GetPoints();
        }
    }

    public override bool isCompleted()
    {
        return _isCompleted;
    }

    public override string Save()
    {
        return $"checklist|{base.Save()}|{_repeatability}|{_bonusPoints}|{_isCompleted}";
    }
}
