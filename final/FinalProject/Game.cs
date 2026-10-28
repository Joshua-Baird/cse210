using System.Runtime.CompilerServices;
using System.Xml.Serialization;

public class Game
{
    private Colony _colony;
    private int _targetDays;
    private int _daysUntilArrival;
    private List<Building> _buildingTypes;
    private int _survivorsTurnedAway;

    public Game()
    {
        _colony = new Colony();
        _targetDays = 30;
        _daysUntilArrival = 5;
        _buildingTypes =
        [
            new Farm(),
            new WaterRecycler(),
            new PowerPlant(),
            new Habitat(),
        ];
        _survivorsTurnedAway = 0;
    }

    public void Run()
    {
        Console.Clear();
        // Game Start Message
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
            int choice = this.HandleInput(0, 3);
            switch (choice)
            {
                case 1:
                    this.ManageAssignments();
                    break;
                case 2:
                    this.ManageConstruction();
                    break;
                case 3:
                    this.EndDay();
                    break;
                case 0:
                    return;
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
        int choice = this.HandleInput(0, _colony.GetColonistCount());
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
Current assignment: {_colony.GetColonistAssignment(colonist)}

CHOOSE AN ASSIGNMENT

{_colony.GetBuildingsDisplay()}

{_colony.GetConstructionAssignmentsDisplay()}

[0] Back

Choose: "
            );
            Building building;
            while (true)
            {
                building = _colony.TryAssign(Console.ReadLine());
                if (building != null)
                {
                    break;
                }
                Console.WriteLine("Please give a valid input");
            }
            bool workerAssigned;
            if (building is ProductionBuilding productionBuilding)
            {
                workerAssigned = _colony.TryAssignWorker(
                    colonist,
                    productionBuilding
                );
            }
            else
            {
                workerAssigned = _colony.TryAssignBuilder(colonist, building);
            }
            if (workerAssigned)
            {
                Console.WriteLine(
                    $"{colonist.GetName()} successfully assigned to {building.GetName()}"
                );
            }
            else
            {
                Console.WriteLine(
                    $"{colonist.GetName()} could not be assigned to {building.GetName()}"
                );
            }
        }
    }

    private void ManageConstruction()
    {
        Console.WriteLine(
            $@"==============================================================
                       CONSTRUCTION
==============================================================

Available energy: {_colony.GetEnergy()}
Housing: {_colony.GetColonistCount()}/{_colony.GetHousingCapacity()} occupied

{this.GetConstructionDisplay()}

[5] View construction projects
[0] Back

Choose:"
        );
        int choice = this.HandleInput(0, 4);
        if (choice == 0)
        {
            return;
        }
        //Grab the building they wanted.
        Building building = _buildingTypes[choice - 1];
        Console.WriteLine(
            $@"==============================================================
                        Colonists
=============================================================

{_colony.GetColonistsDisplay()}
        
To Build: {building.GetName()} 
Required Specialty: {building.GetRequiredSpecialty()}
Choose a colonist you want to work on this project"
        );
        int colonistChoice = this.HandleInput(0, _colony.GetColonistCount());
        Colonist colonist = _colony.GetColonists()[colonistChoice - 1];
        if (building is Habitat habitat)
        {
            Console.WriteLine(
                $@"==============================================================
                        {habitat.GetName()}
==============================================================

Provides:             {habitat.GetCapacity()} additional beds
Construction cost:    {habitat.GetConstructionCost()} energy, paid once
Construction time:    {habitat.GetConstructionDaysRemaining()} working days
Operating upkeep:     {habitat.GetEnergyUpkeep()} energy/day after completion

EXPECTED EFFECT
Housing after completion:     {_colony.GetHousingCapacity()} -> {_colony.GetHousingCapacity() + habitat.GetCapacity()} beds
Energy after payment:        {_colony.GetEnergy()} -> {_colony.GetEnergy() - habitat.GetConstructionCost()}

CREW
{colonist.GetName} {colonist.GetSpecialty} {_colony.GetColonistAssignment(colonist)}

[1] Start construction
[0] Back without starting

Choose:"
            );
        }
        else if (building is ProductionBuilding productionBuilding)
        {
            Console.WriteLine(
                $@"==============================================================
                        {productionBuilding.GetName()}
==============================================================

Provides:             {productionBuilding.GetResourceType()}  
Construction cost:    {productionBuilding.GetConstructionCost()} energy, paid once
Construction time:    {productionBuilding.GetConstructionDaysRemaining()} working days
Operating upkeep:     {productionBuilding.GetEnergyUpkeep()} energy/day after completion

EXPECTED EFFECT
Energy after payment:        {_colony.GetEnergy()} -> {_colony.GetEnergy() - productionBuilding.GetConstructionCost()}

CREW
{colonist.GetName()} {colonist.GetSpecialty()} {_colony.GetColonistAssignment(colonist)}

[1] Start construction
[0] Back without starting

Choose:"
            );
        }
        int secondChoice = this.HandleInput(0, 1);
        if (secondChoice == 0)
        {
            return;
        }
        switch (choice)
        {
            case 1:
                _colony.TryAddIndoorFarm(colonist);
                break;
            case 2:
                _colony.TryAddWaterRecycler(colonist);
                break;
            case 3:
                _colony.TryAddPowerPlant(colonist);
                break;
            case 4:
                _colony.TryAddHabitat(colonist);
                break;
        }
    }

    private void EndDay()
    {
        _daysUntilArrival -= 1;
        if (_daysUntilArrival == 0)
        {
            Console.WriteLine("colonist arrives!");
            _daysUntilArrival = 5;
        }
        if (_colony.AdvanceDay())
        {
            Console.WriteLine(
                $@"================================================================
                     DAY {_colony.GetDaysSurvived()} OF {_targetDays} COMPLETE
================================================================

{_colony.GetResourceReportDisplay()}

{_colony.GetStatusReport()}

{_colony.GetConstructionDisplay()}

----------------------------------------------------------------
COLONY
----------------------------------------------------------------

Population: {_colony.GetColonistCount()}
Housing: {_colony.GetColonistCount()}/{_colony.GetHousingCapacity()} occupied
Survivors turned away: {_survivorsTurnedAway}

Next survivor arrives in {_daysUntilArrival} days

Days remaining until the end of the month: {_targetDays}

Press Enter to plan day {_colony.GetDaysSurvived() + 1}.
================================================================ "
            );
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Game Over");
        }
    }

    private string GetConstructionDisplay()
    {
        int i = 0;
        string fullString =
            "# Building     Cost     Build time     Required builder\n";
        foreach (Building building in _buildingTypes)
        {
            i++;
            string line =
                $"{i} {building.GetName()}    {building.GetEnergyUpkeep()}     {building.GetConstructionDaysRemaining()}     {building.GetRequiredSpecialty()}";
            fullString += line + "\n";
        }
        return fullString;
    }

    private void BeginDay()
    {
        Console.WriteLine(
            $@"==============================================================
                         OUTPOST
                    DAY {_colony.GetDaysSurvived() + 1} OF {_targetDays} — PLANNING
==============================================================

Colonists: {_colony.GetColonistCount()}        Housing: {_colony.GetColonistCount()}/{_colony.GetHousingCapacity()}
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

    private int HandleInput(int lowerRange, int higherRange)
    {
        bool valid = false;
        while (!valid)
        {
            string userInput = Console.ReadLine();
            if (
                int.TryParse(userInput, out int choice)
                && choice >= lowerRange
                && choice <= higherRange
            )
            {
                return choice;
            }
            else
            {
                Console.WriteLine("please type a valid number");
            }
        }
        return -9;
    }
}
