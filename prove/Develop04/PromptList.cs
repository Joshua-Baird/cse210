using System.Diagnostics.Contracts;

public class PromptList
{
    private List<string> _prompts;
    public PromptList(string type)
    {
        if (type == "reflectionQuestions")
        {
            _prompts = ["Why was this experience meaningful to you?", "Have you ever done anything like this before?", "How did you get started?", "How did you feel when it was complete?", "What made this time different than other times when you were not as successful?", "What is your favorite thing about this experience?", "What could you learn from this experience that applies to other situations?", "What did you learn about yourself through this experience?", "How can you keep this experience in mind in the future?"];
        }
        else if (type == "reflectionPrompts")
        {
            _prompts = ["Think of a time when you stood up for someone else.", "Think of a time when you did something really difficult.", "Think of a time when you helped someone in need.", "Think of a time when you did something truly selfless."];
        }
        else if (type == "listing")
        {
            _prompts = ["Who are people that you appreciate?", "What are personal strengths of yours?", "Who are people that you have helped this week?", "When have you felt the Holy Ghost this month?", "Who are some of your personal heroes?"];
        }
    }
    public string Give()
    {
        //returns and removes a random prompt from _prompts
        //if there are no prompts, returns ""
        if (_prompts.Count() == 0)
        {
            return "";
        }
        else
        {

            Random random = new Random();
            int ranI = random.Next(0, _prompts.Count() - 1);
            string prompt = _prompts[ranI];
            _prompts.RemoveAt(ranI);
            return prompt;
        }
    }
}