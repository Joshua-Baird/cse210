using System;

class Program
{
  static void Main(string[] args)
  {
    List<int> numbers = new List<int>();
    Console.WriteLine("Enter a list of numbers, type 0 when finished.");
    int userNumber;
    do
    {
      Console.WriteLine("Enter a number: ");
      userNumber = int.Parse(Console.ReadLine());
      numbers.Add(userNumber);
    } while (userNumber != 0);
    int sum = 0;
    double max = double.NegativeInfinity;
    foreach (int number in numbers)
    {
      sum = sum + number;
      if (max < number)
      {
        max = number;
      }
    }
    int average = sum / numbers.Count;
    Console.WriteLine($"Total: {sum}");
    Console.WriteLine($"Average: {average}");
    Console.WriteLine($"Max: {max}");
  }
}
