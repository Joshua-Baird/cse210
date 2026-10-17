using System.Collections;
using System.Configuration.Assemblies;
using System.IO.Enumeration;
using System.Reflection;
using System.Xml.Serialization;

public class User
{
    private List<Goal> _goals;
    private double _totalPoints;
    private string _name;

    public User(double totalPoints, string name)
    {
        _goals = [];
        _totalPoints = totalPoints;
        _name = name;
    }

    public User(string fileName)
    //loads info from a .txt file into its atributes
    {
        _goals = [];
        string[] lines = File.ReadAllLines(fileName);
        string[] currentGoalStrings = lines[0].Split("||");
        _totalPoints = double.Parse(lines[1].Trim());
        _name = lines[2].Trim();
        foreach (string goal in currentGoalStrings)
        {
            string[] parts = goal.Split("|");

            string type = parts[0].Trim();
            string name = parts[1].Trim();
            string description = parts[2].Trim();
            double points = double.Parse(parts[3].Trim());
            if (type == "simple")
            {
                bool isComplete = bool.Parse(parts[4].Trim());
                SimpleGoal simpleGoal = new SimpleGoal(
                    name,
                    description,
                    points,
                    isComplete
                );
                _goals.Add(simpleGoal);
            }
            if (type == "eternal")
            {
                int timesCompleted = int.Parse(parts[4].Trim());
                _goals.Add(
                    new EternalGoal(name, description, points, timesCompleted)
                );
            }
            if (type == "checklist")
            {
                int timesCompleted = int.Parse(parts[4].Trim());
                int repeatability = int.Parse(parts[5].Trim());
                int bonus = int.Parse(parts[6].Trim());
                bool isComplete = bool.Parse(parts[7].Trim());
                _goals.Add(
                    new ChecklistGoal(
                        name,
                        description,
                        points,
                        repeatability,
                        timesCompleted,
                        bonus
                    )
                );
            }
        }
    }

    public void Save(string fileName)
    {
        List<string> goalStrings = [];
        foreach (Goal goal in _goals)
        {
            goalStrings.Add(goal.Save());
        }
        using (StreamWriter outputFile = new StreamWriter(fileName))
        {
            outputFile.WriteLine(string.Join("||", goalStrings));
            outputFile.WriteLine(_totalPoints);
            outputFile.WriteLine(_name);
        }
    }

    public void AddGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void AddPoints(double points)
    {
        _totalPoints += points;
    }

    public void DisplayPoints()
    {
        Console.WriteLine($"{_name} has {_totalPoints} points");
    }

    public void DisplayGoals()
    {
        foreach (Goal goal in _goals)
        {
            goal.Display();
        }
    }

    public void DisplayGoalDetails()
    {
        int i = 0;
        foreach (Goal goal in _goals)
        {
            i++;
            Console.WriteLine($"{i}.");
            goal.DisplayDetails();
        }
    }

    public void CompleteGoal(int index)
    {
        _totalPoints += _goals[index].Complete();
    }

    public string GetName()
    {
        return _name;
    }
}
