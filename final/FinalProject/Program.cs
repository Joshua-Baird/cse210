using System;

class Program
{
    static void Main(string[] args)
    {
        Colonist testColonist = new Colonist("Test Colonist");
        Console.WriteLine(testColonist.GetName());
        Console.WriteLine(testColonist.GetSpecialty());
    }
}
