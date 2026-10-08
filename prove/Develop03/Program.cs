using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        //Instead of always displaying 1 scripture this program reads from a file that contains 50 different scriptures
        //I used AI to generate the file with the scruptures, but that is the only thing I used AI for.
        //The program reads the file into a list of scriptures, then displays one at random every time you run it.
        List<Scripture> scriptures = [];
        string currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine(currentDirectory);
        string filePath = Path.Combine(currentDirectory, "scriptures.csv");
        Console.WriteLine(filePath);
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] scriptureLine = line.Split("||");
                scriptures.Add(new Scripture(new Reference(scriptureLine[0]), scriptureLine[1]));
            }
        }
        Random random = new Random();
        int ranScriptureIndex = random.Next(1, scriptures.Count());
        Scripture currentScripture = scriptures[ranScriptureIndex];
        string userInput;
        bool done;
        do
        {
            done = currentScripture.Display();
            currentScripture.Hide();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Please hit return to continue or type 'quit' to quit the program");
            Console.Write(">");
            userInput = Console.ReadLine();
        } while (userInput != "quit" && !done);
    }
}