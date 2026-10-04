using System.Diagnostics.Contracts;

public class Word
{
    private bool _hidden = false;
    private string _content;
    public Word(string word)
    {
        _content = word;
    }
    public void Display()
    {
        if (!_hidden)
        {
            Console.Write(_content + " ");
        }
        else
        {
            for (int i = 0; i < _content.Count(); i++)
            {
                Console.Write('_');
            }
            Console.Write(' ');
        }
    }
    public void Hide()
    {
        _hidden = true;
    }
    public bool getHidden()
    {
        return _hidden;
    }
}