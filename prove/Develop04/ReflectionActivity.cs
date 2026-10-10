
public class ReflectionActivity : Activity
{
    private PromptList _reflectionPrompts;
    private PromptList _reflectionQuestions;

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strenght and resiliance. This will help you realize the power you have and how you can use it in other aspects of your life.")
    {
        _reflectionPrompts = new PromptList("reflectionPrompts");
        _reflectionQuestions = new PromptList("reflectionQuestions");
    }
    public void Run()
    {
        base.Start();
        Console.WriteLine("Consider the following prompt");

        string currentPrompt = _reflectionPrompts.Give();
        Console.WriteLine($"-----{currentPrompt}------");
        Console.Write("Press enter to continue");
        Console.ReadLine();
        Console.Clear();

        int runTime = 0;
        while (runTime <= _timeInSeconds)
        {
            currentPrompt = _reflectionQuestions.Give();
            Console.WriteLine(currentPrompt);
            Thread.Sleep(5000);
            runTime += 5;
        }
        base.End();
    }


}