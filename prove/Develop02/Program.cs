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