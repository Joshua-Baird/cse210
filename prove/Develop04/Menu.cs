public class Menu
{
    public int Run()
    //Displays the menu, and then returns the user's selection (1, 2, 3, 4, or 5) 0 for try again
    {
        bool running = true;
        while (running)
        {

            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("--------------------------");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("--------------------------");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("--------------------------");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("--------------------------");
            Console.WriteLine("4. Quit Program");
            Console.WriteLine("--------------------------");
            Console.Write(">");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                case "1.":
                    running = false;
                    return 1;
                case "2":
                case "2.":
                    running = false;
                    return 2;
                case "3":
                case "3.":
                    running = false;
                    return 3;
                case "4":
                case "4.":
                    running = false;
                    return 4;
                default:
                    Console.Clear();
                    Console.WriteLine("Please give a valid responce (1, 2, 3, 4, or 5)");
                    break;
            }
        }
        return -1;
    }
}