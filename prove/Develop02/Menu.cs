public class Menu
{
    public string _fileName;
    bool _running = true;

    public int Run()
    //Displays the menu, and then returns the user's selection (1, 2, 3, 4, or 5) 0 for try again
    {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine();
        Console.WriteLine("--------------------------");
        Console.WriteLine("1. Write a New Entry");
        Console.WriteLine("--------------------------");
        Console.WriteLine("2. Display Journal Entries");
        Console.WriteLine("--------------------------");
        Console.WriteLine("3. Save Journal To File");
        Console.WriteLine("--------------------------");
        Console.WriteLine("4. Load Journal From File");
        Console.WriteLine("--------------------------");
        Console.WriteLine("5. Quit Program");
        Console.WriteLine("--------------------------");
        Console.Write(">");
        string userInput = Console.ReadLine();
        if (userInput == "1" || userInput == "1.")
        {
            return 1;
        }
        else if (userInput == "2" || userInput == "2.")
        {
            return 2;
        }
        else if (userInput == "3" || userInput == "3.")
        {
            return 3;
        }
        else if (userInput == "4" || userInput == "4.")
        {
            return 4;
        }
        else if (userInput == "5" || userInput == "5.")
        {
            _running = false;
            return 5;
        }
        else
        {
            Console.WriteLine("Please give a valid responce (1, 2, 3, 4, or 5)");
            return 0;
        }
    }
}