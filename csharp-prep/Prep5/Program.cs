using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();
        string name = PromtUserName();
        int number = PromtUserNumber();
        int birthYear = 0;
        PromtUserBirthYear(out birthYear);
        int numberSquared = SquareNumber(number);
        DisplayResult(name, numberSquared, birthYear);
    }
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }
    static string PromtUserName()
    {
        Console.WriteLine("Please type your name");
        return Console.ReadLine();
    }
    static int PromtUserNumber()
    {
        Console.WriteLine("Please type your favorate number");
        return int.Parse(Console.ReadLine());
    }
    static void PromtUserBirthYear(out int birthYear)
    {
        Console.WriteLine("Please type your birth year");
        birthYear = int.Parse(Console.ReadLine());
    }
    static int SquareNumber(int number)
    {
        return number * number;
    }
    static void DisplayResult(string name, int number, int birthyear)
    {
        Console.WriteLine($"{name}, your number squared is {number}");
        int currentYear = DateTime.Now.Year;
        int age = currentYear - birthyear;
        Console.WriteLine($"{name}, you are turning {age} this year");
    }
}