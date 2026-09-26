using System.Threading.Tasks.Dataflow;
using Microsoft.VisualBasic;

public class Entry
{
    public string _date;
    public string _content;
    public string _promptUsed;
    List<string> _prompts = [
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
        ];

    public void Write()
    {
        // get a random number between 0 and the length of the _prompts list. We add 1 since it is exclusive
        int randomPromptIndex = Random.Shared.Next(_prompts.Count + 1);
        Console.WriteLine("What is the date that your would like to write for");
        _date = Console.ReadLine();
        _promptUsed = _prompts[randomPromptIndex];
        Console.WriteLine(_promptUsed);
        _content = Console.ReadLine();
    }
    public void Display()
    {
        Console.WriteLine($"Date: {_date}");
        Console.WriteLine($"Prompt: {_promptUsed}");
        Console.WriteLine(_content);
    }
}