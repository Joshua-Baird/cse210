using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your percentage");
        string percentString = Console.ReadLine();
        int percent = int.Parse(percentString);
        string letterGrade;
        if (percent >= 90)
        {
            letterGrade = "A";
        }
        else if (percent >= 80)
        {
            letterGrade = "B";
        }
        else if (percent >= 70)
        {
            letterGrade = "C";
        }
        else if (percent >= 60)
        {
            letterGrade = "D";
        }
        else
        {
            letterGrade = "F";
        }
        Console.WriteLine($"Your grade is {letterGrade}");
        if (letterGrade != "D" && letterGrade != "F")
        {
            Console.WriteLine("Your passing! Good job");
        }
        else
        {
            Console.WriteLine("Try a bit harder, you got this!");
        }

    }
}