using System.ComponentModel.DataAnnotations;

public class Reference
{
    private string _content;
    public Reference()
    {
        _content = "Moroni 10:4-5";
    }
    public Reference(string book, int chapter, int verse)
    {
        _content = $"{book.ToUpper()} {chapter}:{verse}";
    }
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _content = $"{book.ToUpper()} {chapter}:{startVerse}-{endVerse}";
    }
    public Reference(string entireReference)
    {
        _content = entireReference;
    }
    public void Display()
    {
        Console.Write(_content + " ");
    }
}