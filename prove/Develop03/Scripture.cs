using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words = [];
    private int _numWordsUnHidden = 0;
    public Scripture()
    {
        _reference = new Reference();
        string scriptureString = "And when ye shall receive these things, I would exhort you that ye would ask God, the Eternal Father, in the name of Christ, if these things are not true; and if ye shall ask with a sincere heart, with real intent, having faith in Christ, he will manifest the truth of it unto you, by the power of the Holy Ghost. And by the power of the Holy Ghost ye may know the truth of all things.";
        string[] scriptureWordStrings = scriptureString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in scriptureWordStrings)
        {
            _words.Add(new Word(word));
        }

    }
    public Scripture(Reference reference, string scripture)
    {
        _reference = reference;
        string[] scriptureWordStrings = scripture.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in scriptureWordStrings)
        {
            _words.Add(new Word(word));
        }

    }
    public bool Display()
    {
        int numWordsHidden = 0;
        _reference.Display();
        foreach (Word word in _words)
        {
            word.Display();
            if (word.getHidden())
            {
                numWordsHidden++;
            }
        }
        Console.WriteLine();
        _numWordsUnHidden = _words.Count() - numWordsHidden;
        if (_numWordsUnHidden != 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void Hide()
    {
        int wordsToHide = 3;
        if (_numWordsUnHidden >= 3)
        {
            wordsToHide = 3;
        }
        else
        {
            wordsToHide = _numWordsUnHidden;

        }
        Random random = new Random();
        int wordsHidden = 0;
        while (wordsHidden != wordsToHide)
        {
            int ranWordI = random.Next(0, _words.Count());
            if (!_words[ranWordI].getHidden())
            {
                _words[ranWordI].Hide();
                wordsHidden++;
            }
        }

    }

}
