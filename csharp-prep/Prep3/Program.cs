using System;

class Program
{
    static void Main(string[] args)
    {
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);
        int userNumber;
        Console.WriteLine("what is the magic number");
        do
        {
            userNumber = int.Parse(Console.ReadLine());
            if (userNumber > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else if (userNumber < magicNumber)
            {
                Console.WriteLine("Higher");
            }
        } while (userNumber != magicNumber);
    }
}