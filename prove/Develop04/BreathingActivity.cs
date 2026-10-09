public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "In this activity you will be prompted to breath in and out in time for a given amount of seconds")
    {
    }
    public void Run()
    {
        base.Start();
        while (_timeInSeconds > 0)
        {
            Console.WriteLine();
            Console.Write("Breath in. . . ");
            for (int i = 4; i != 0; i--)
            {
                Console.Write($"{i}\b");
                Thread.Sleep(1000);
            }
            _timeInSeconds = _timeInSeconds - 4;
            //rewrite the line to remove the numeber
            Console.WriteLine("\rBreath in. . .   ");
            Console.WriteLine();
            Console.Write("Breath out. . . ");
            for (int i = 6; i != 0; i--)
            {
                Console.Write($"{i}\b");
                Thread.Sleep(1000);
            }
            _timeInSeconds = _timeInSeconds - 6;
            //rewrite the line to remove the numeber
            Console.WriteLine("\rBreath out. . .   ");
        }
        base.End();

    }
}