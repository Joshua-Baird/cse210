using System;
using System.Configuration.Assemblies;
using System.Runtime.CompilerServices;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        Menu menu = new Menu();
        int userChoice = menu.Run();
        while (userChoice != 5)
        {
            switch (userChoice)
            {
                case 1:
                    Entry newEntry = new Entry();
                    newEntry.Write();
                    journal._entries.Add(newEntry);
                    //to exceed the requirements I have added a fail save that will make sure that you don't loose portions of your journal accidentally. 
                    //When you save it will check to see if there is a file named the same thing and warn you about replacing it.
                    //If your attempt to load and you have unsaved entries in your journal it will warn you about loosing your data when you load. 
                    journal._saved = false;
                    break;
                case 2:
                    journal.Display();
                    break;
                case 3:
                    journal.Save();
                    break;
                case 4:

                    journal.Load();
                    break;
                case 5:
                    Console.WriteLine("Quit");
                    break;
            }
            userChoice = menu.Run();
        }
    }
    public void LoadJournalFromFile(string filePath)
    {

    }
}