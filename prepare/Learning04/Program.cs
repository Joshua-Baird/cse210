using System;

class Program
{
    static void Main(string[] args)
    {
        WritingAssignment writingAssignment = new WritingAssignment("Joshua Baird", "World History", "The Causes of WW1");
        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
        MathAssignment assignment = new MathAssignment("Joshua Baird", "Matrices", "7.3", "1, 3, 6, 7, 12, 34");
        Console.WriteLine(assignment.GetSummary());
        Console.WriteLine(assignment.GetHomeworkList());
    }
}