using System.ComponentModel.DataAnnotations;

public class ListingActivity : Activity
{
    private PromptList _prompts;
    private int _userNum;
    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _prompts = new PromptList("listing");
    }
    public void Run()
    {
        base.Start();
        Console.WriteLine("List as many responses as you can to the following prompt");
        string currentPrompt = _prompts.Give();
        Console.WriteLine($"-----{currentPrompt}------");
        Console.Write("you may begin in:  ");
        for (int i = 5; i > 0; i--)
        {
            Console.Write($"\b\b{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
        DateTime currentTime = DateTime.Now;
        DateTime futureTime = currentTime.AddSeconds(_timeInSeconds);

        while (currentTime < futureTime)
        {
            Console.Write(">");
            Console.ReadLine();
            _userNum++;
            currentTime = DateTime.Now;
        }
        Console.WriteLine($"congratulation you wrote {_userNum} entries");
        base.End();
    }
}