using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;

public class Game
{
    private List<User> _users;
    private int _choice;
    private bool _running;

    public Game()
    {
        _users = [];
        _choice = 0;
        _running = true;
    }

    public void RunMain()
    {
        do
        {
            do
            {
                Console.WriteLine("What would you like to do");
                Console.WriteLine();
                Console.WriteLine("1. Create a User");
                Console.WriteLine("2. Load a User");
                Console.WriteLine("3. Save a User");
                Console.WriteLine("4. Show all loaded users");
                Console.WriteLine("5. Add a Goal to a user");
                Console.WriteLine("6. Show a users Goals");
                Console.WriteLine("7. Complete a Goal");
                Console.WriteLine("8. Display users points");
                Console.WriteLine("9. Quit");
                Console.Write("> ");
                string input = Console.ReadLine();
                string cleanedInput = input?.Trim().TrimEnd('.');
                if (
                    int.TryParse(cleanedInput, out int number)
                    && number >= 1
                    && number <= 9
                )
                {
                    _choice = number;
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Please type a valid number");
                }
            } while (true);
            switch (_choice)
            {
                case 1:
                    Console.WriteLine("What is the username for this user?");
                    Console.Write(">");
                    string username = Console.ReadLine().Trim();
                    _users.Add(new User(0, username));
                    Console.WriteLine($"{username} was successfully created");
                    break;
                case 2:
                    Console.WriteLine(
                        "What is the name of the user you want to load?"
                    );
                    username = Console.ReadLine().Trim();
                    string filename = username + ".txt";
                    if (!File.Exists(filename))
                    {
                        Console.WriteLine(
                            $"there is no user with the name of {username} saved."
                        );
                        break;
                    }
                    _users.Add(new User(filename));
                    Console.WriteLine(
                        $"The user {username} was successfully loaded"
                    );
                    break;
                case 3:
                    Console.WriteLine(
                        "What is the name of the user you want to save"
                    );
                    Console.Write(">");
                    string input = Console.ReadLine().Trim();
                    bool found = false;
                    foreach (User user in _users)
                    {
                        if (input == user.GetName())
                        {
                            user.Save($"{input}.txt");
                            Console.WriteLine(
                                $"{input} was saved successfully"
                            );
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine(
                            $"{input} either does not exist, or is not loaded"
                        );
                    }
                    break;
                case 4:
                    Console.WriteLine("The loaded users are");
                    foreach (User user in _users)
                    {
                        Console.WriteLine(user.GetName());
                    }
                    break;
                case 5:
                    Console.Clear();
                    Console.WriteLine(
                        "What is the name of the user you want to add a goal for"
                    );
                    input = Console.ReadLine().Trim();
                    User tempUser = null;
                    found = false;
                    foreach (User user in _users)
                    {
                        if (input == user.GetName())
                        {
                            tempUser = user;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine(
                            $"{input} either does not exist, or is not loaded"
                        );
                        break;
                    }
                    int userChoice = 0;
                    do
                    {
                        Console.WriteLine("what type of goal is this");
                        Console.WriteLine("1. Simple Goal");
                        Console.WriteLine("2. Eternal Goal");
                        Console.WriteLine("3. Checklist Goal");
                        input = Console.ReadLine();
                        string cleanedInput = input?.Trim().TrimEnd('.');
                        if (
                            int.TryParse(cleanedInput, out userChoice)
                            && userChoice >= 1
                            && userChoice <= 3
                        )
                        {
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Please type a valid number");
                        }
                    } while (true);
                    Console.WriteLine("what is the name of the goal");
                    Console.Write(">");
                    string name = Console.ReadLine().Trim();
                    Console.WriteLine("what is the description of the goal");
                    Console.Write(">");
                    string description = Console.ReadLine().Trim();
                    Console.WriteLine("how many points is it worth?");
                    Console.Write(">");
                    double points = double.Parse(Console.ReadLine().Trim());
                    switch (userChoice)
                    {
                        case 1:
                            Console.WriteLine(
                                "have you already completed it? if you have type (y)"
                            );
                            Console.Write(">");
                            string userInput = Console.ReadLine().Trim();
                            bool isComplete = false;
                            if (userInput == "y")
                            {
                                isComplete = true;
                                tempUser.AddPoints(points);
                            }
                            tempUser.AddGoal(
                                new SimpleGoal(
                                    name,
                                    description,
                                    points,
                                    isComplete
                                )
                            );
                            break;
                        case 2:
                            tempUser.AddGoal(
                                new EternalGoal(name, description, points)
                            );
                            break;
                        case 3:
                            Console.WriteLine(
                                "How many times would you like to do this?"
                            );
                            Console.Write(">");
                            int repeatability = int.Parse(
                                Console.ReadLine().Trim()
                            );
                            Console.WriteLine(
                                "would you like to record previos completions? if so please type the number completed. In not please leave blank"
                            );
                            Console.Write(">");

                            string timesCompletedString = Console
                                .ReadLine()
                                .Trim();
                            int timesCompleted;
                            if (timesCompletedString == "")
                            {
                                timesCompleted = 0;
                            }
                            else if (
                                int.TryParse(
                                    timesCompletedString,
                                    out timesCompleted
                                )
                                && timesCompleted <= repeatability
                            ) { }
                            else
                            {
                                Console.WriteLine(
                                    "please type a number less than or equal to the number of times you can complete this goal"
                                );
                            }
                            Console.WriteLine(
                                "how many bonus points do you want to get for fully completing ths goal"
                            );
                            Console.Write(">");
                            double bonuspoints = double.Parse(
                                Console.ReadLine().Trim()
                            );
                            tempUser.AddGoal(
                                new ChecklistGoal(
                                    name,
                                    description,
                                    points,
                                    repeatability,
                                    timesCompleted,
                                    bonuspoints
                                )
                            );
                            break;
                    }
                    Console.WriteLine("Your goal has been added");
                    break;
                case 6:

                    Console.Clear();
                    Console.WriteLine(
                        "What is the name of the user you want to see the goals for"
                    );
                    input = Console.ReadLine().Trim();
                    found = false;
                    foreach (User user in _users)
                    {
                        if (input == user.GetName())
                        {
                            user.DisplayGoals();
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine(
                            $"{input} either does not exist, or is not loaded"
                        );
                    }
                    break;
                case 7:
                    Console.Clear();
                    Console.WriteLine(
                        "What is the name of the user you want to complete a goal for"
                    );
                    input = Console.ReadLine().Trim();
                    found = false;
                    User user1 = null;
                    foreach (User user in _users)
                    {
                        if (input == user.GetName())
                        {
                            user1 = user;
                            user.DisplayGoalDetails();
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine(
                            $"{input} either does not exist, or is not loaded"
                        );
                        break;
                    }

                    Console.WriteLine("Which goal would you like to complete?");
                    Console.Write(">");
                    input = Console.ReadLine().Trim();
                    user1.CompleteGoal(int.Parse(input) - 1);
                    break;
                case 8:
                    Console.WriteLine(
                        "Which user would you like to see the points for (for all users press enter)"
                    );
                    Console.Write(">");
                    string choice = Console.ReadLine().Trim();
                    if (choice == "")
                    {
                        foreach (User user in _users)
                        {
                            user.DisplayPoints();
                        }
                    }
                    else
                    {
                        found = false;
                        foreach (User user in _users)
                        {
                            if (user.GetName().ToLower() == choice.ToLower())
                            {
                                user.DisplayPoints();
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine(
                                $"There is no user with the name {choice} loaded."
                            );
                        }
                    }
                    break;
                case 9:
                    _running = false;
                    break;
            }
        } while (_running);
    }
}
