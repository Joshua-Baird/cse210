using System;
using System.Xml.Serialization;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        BreathingActivity breathingActivity = new BreathingActivity();
        ReflectionActivity reflectionActivity = new ReflectionActivity();
        ListingActivity listingActivity = new ListingActivity();
        Menu menu = new Menu();
        int choice;
        do
        {
            choice = menu.Run();
            switch (choice)
            {
                case 1:
                    breathingActivity.Run();
                    break;
                case 2:
                    reflectionActivity.Run();
                    break;
                case 3:
                    listingActivity.Run();
                    break;

            }
        } while (choice != 4);



    }
}