using System;

class Program
{
    static void Main(string[] args)
    {
        SimpleGoal testSimple = new SimpleGoal(
            "test simple",
            "This is the test simple goal",
            10
        );
        EternalGoal testEteral = new EternalGoal(
            "test eternal",
            "This is the test eternal goal",
            10
        );
        ChecklistGoal testCheck = new ChecklistGoal(
            "test checklist",
            "This is the test checklist goal",
            10,
            5,
            1
        );
        List<Goal> goals = [testSimple, testCheck, testEteral];
        User user = new User("user.txt");
        user.DisplayGoals();
        user.AddPoints(testSimple.Complete());
        user.DisplayPoints();
    }
}
