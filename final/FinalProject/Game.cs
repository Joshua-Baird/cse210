public class Game
{
    private Colony _colony;
    private int _targetDays;
    private bool _gameOver;
    private int _daysUntilArrival;

    public Game()
    {
        _colony = new Colony();
        _targetDays = 30;
        _daysUntilArrival = 5;
    }

    public void Run()
    {
        Console.Clear();
        Console.WriteLine(
            @"OUTPOST

At first, the Sun only flickered.

Brief moments of darkness became longer. Scientists argued over the cause.
As the flickering spread, fear turned into worldwide panic. 
Stores emptied. Roads filled. Everyone searched for somewhere safe.

You kept a level head and headed for Iceland.

While there was still time, you established an experimental refuge around a geothermal power plant.
Heated living quarters, an indoor farm, and a water recycler offered a chance to survive without sunlight.

Then the Sun disappeared.
The sky went dark. The world waited for daylight that never came.

But beneath your facility, the Earth was still warm.
The generators hummed. The grow lights stayed on.
Six colonists now depend on this outpost.
Their skills—and your decisions—will determine whether it becomes a lasting refuge.

Survive the first 25 days and grow your community to at least 10 colonists.

Press Enter to begin."
        );
        Console.ReadLine();
        //first day
        this.BeginDay();
        //continue until the target number of days is reached
        while (_colony.GetDaysSurvived() < _targetDays)
        {
            bool valid = false;
            //allow to retry as long as the input is not valid
            while (!valid)
            {
                string userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            valid = true;
                            this.ManageAssignments();
                            break;
                        case 2:
                            valid = true;
                            this.ManageConstruction();
                            break;
                        case 3:
                            valid = true;
                            this.EndDay();
                            break;
                        case 0:
                            valid = true;
                            return;
                    }
                }
                else
                {
                    valid = false;
                    Console.WriteLine("please provide a valid input");
                }
            }
            this.BeginDay();
        }
    }

    private void ManageAssignments()
    {
        Console.WriteLine(
            $@"==============================================================
                   MANAGE ASSIGNMENTS
==============================================================

{_colony.GetColonistsDisplay()}

{_colony.GetBuildingsDisplay()}

{_colony.GetConstructionDisplay()}

Select a colonist number, or 0 to return: "
        );
        bool valid = false;
        while (!valid)
        {
            string userChoice = Console.ReadLine();
            //make sure the user input is a number matching one of the colonists
            if (
                int.TryParse(userChoice, out int choice)
                && choice <= _colony.GetColonistCount()
                && choice >= 0
            )
            {
                if (choice == 0)
                {
                    return;
                }
                Colonist colonist = _colony.GetColonists()[choice - 1];
                Console.WriteLine(
                    $@"==============================================================
                         {colonist.GetName()}
==============================================================

Specialty: {colonist.GetSpecialty()}
Health: {(colonist.IsSick() ? "Sick" : "Healthy")}
Current assignment: {_colony.GetColonistAssignment(colonist)}

CHOOSE AN ASSIGNMENT

{_colony.GetBuildingsDisplay()}

Construction assignments: none available.

[0] Back

Choose: "
                );
            }
            else
            {
                valid = false;
                Console.WriteLine("please provide a valid input");
            }
        }
    }

    private void ManageConstruction() { }

    private void EndDay() { }

    private void BeginDay()
    {
        Console.WriteLine(
            $@"==============================================================
                         OUTPOST
                    DAY {_colony.GetDaysSurvived()} OF {_targetDays} — PLANNING
==============================================================

Colonists: {_colony.GetColonistCount()}        Housing: {_colony.GetColonistCount()}/{_colony.GetHousingCopacity()}        Morale: {_colony.GetMorale()}/100
Goal: Finish day 25 with at least 10 colonists.

{_colony.GetResourceForcastDisplay()}

Energy use: 3 heating + {_colony.GetProjectedEnergyConsumption()} building upkeep.
Forecast excludes random events.

{_colony.GetBuildingsDisplay()}

{_colony.GetColonistsDisplay()}

{_colony.GetConstructionDisplay()}

Next Colonist Arrives in {_daysUntilArrival} day(s)

--------------------------------------------------------------
[1] Manage assignments
[2] Construction
[3] End day
[0] Quit

Choose:"
        );
    }
}
