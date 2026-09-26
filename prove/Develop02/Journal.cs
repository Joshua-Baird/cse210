using System.IO.Enumeration;

public class Journal
{
    public List<Entry> _entries = [];
    public bool _saved = true;
    public void Load()
    {
        if (!_saved)
        {
            Console.WriteLine("There are unsaved changes in your journal. Press 1 if you want to continue (you will loose all your changes)");
            Console.Write(">");
            if (Console.ReadLine() != "1")
            {

                Console.WriteLine("Exiting without Loading");
                return;
            }
        }
        Console.WriteLine("Please type the file name for the Journal you would like to load");
        Console.Write(">");
        string fileName = Console.ReadLine();
        if (File.Exists(fileName))
        {
            string fullFile = File.ReadAllText(fileName);
            string[] stringEntries = fullFile.Split("~");
            foreach (string stringEntry in stringEntries)
            {
                Entry entry = new Entry();
                string[] splitEntry = stringEntry.Split("||");
                entry._date = splitEntry[0];
                entry._promptUsed = splitEntry[1];
                entry._content = splitEntry[2];
                _entries.Add(entry);
            }

        }
        else
        {
            Console.WriteLine("That File does not exist. Please choose a file that does");
        }
        _saved = true;
    }
    public void Save()
    {
        Console.WriteLine("Please type the name for the file");
        Console.Write(">");
        string fileName = Console.ReadLine();
        if (File.Exists(fileName))
        {
            Console.WriteLine("This file already exists. press 1 to override it");
            Console.Write(">");
            if (Console.ReadLine() != "1")
            {
                Console.WriteLine("Exiting without Saving");
                return;
            }
        }
        List<string> entryStrings = [];
        foreach (Entry entry in _entries)
        {
            string entryString = entry._date + "||" + entry._promptUsed + "||" + entry._content;
            entryStrings.Add(entryString);
        }
        string entriesCombined = String.Join("~", entryStrings);
        File.WriteAllText(fileName, entriesCombined);
        _saved = true;
    }
    public void Display()
    {

        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }
}