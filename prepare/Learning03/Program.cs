using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        Fraction randomFrac = new Fraction(random.Next(1, 11), random.Next(1, 11));
        for (int i = 0; i < 30; i++)
        {
            Console.WriteLine($"Fraction: {randomFrac.GetFractionString()} Number: {randomFrac.GetDecimalValue()}");
            randomFrac.SetTop(random.Next(1, 11));
            randomFrac.SetBottom(random.Next());
        }
    }
}