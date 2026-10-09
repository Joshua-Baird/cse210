public class Activity
{
    private string _name;
    private string _description;
    protected int _timeInSeconds;
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }
    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Welcome to the {_name} Activity");
        Console.WriteLine(_description);
        Console.Write("how long, in seconds, would you like for you session? ");
        string userInput = Console.ReadLine();
        Console.Clear();
        while (!int.TryParse(userInput, out _timeInSeconds))
        {
            Console.Write("Please enter a number ");
            userInput = Console.ReadLine();
        }
        Console.WriteLine("Get ready . . .");
        string[] spinner = ["/", "-", "\\", "|"];
        for (int i = 0; i < 20; i++)
        {
            Console.Write($"\r{spinner[i % spinner.Length]}");
            Thread.Sleep(100);
        }
        Console.Clear();
    }
    public void End()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Well Done!");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"You have completed another {_timeInSeconds} seconds of the {_name} Activity");
    }
}