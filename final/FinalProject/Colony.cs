using System.Reflection.Metadata.Ecma335;

public class Colony
{
    private int _nextBuildingId;
    private int _daysSurvived;
    private int _food;
    private int _energy;
    private int _water;
    private int _morale;
    private int _housingCopacity;
    private bool _recovering;
    private bool _poweroutage;
    private List<Colonist> _colonists;
    private List<Building> _buildings;

    public Colony()
    {
        _daysSurvived = 0;
        _food = 18;
        _energy = 12;
        _water = 18;
        _morale = 70;
        _housingCopacity = 8;
        _recovering = false;
        _poweroutage = false;
        _colonists =
        [
            new Colonist("Freyja", Specialty.Scientist),
            new Colonist("Aron", Specialty.Engineer),
            new Colonist("Mira", Specialty.Farmer),
            new Colonist("Jonas"),
            new Colonist("Nora"),
            new Colonist("Leah"),
        ];
        _buildings =
        [
            new Farm(1, 0),
            new WaterRecycler(2, 0),
            new PowerPlant(3, 0),
            new Habitat(4, 0),
            new Habitat(5, 0),
        ];
        _nextBuildingId = 6;
    }

    public void MakeHabitat()
    {
        _buildings.Add(new Habitat(_nextBuildingId));
        _nextBuildingId++;
    }

    public void MakePowerPlant()
    {
        _buildings.Add(new PowerPlant(_nextBuildingId));
        _nextBuildingId++;
    }

    public void MakeWaterRecycler()
    {
        _buildings.Add(new WaterRecycler(_nextBuildingId));
        _nextBuildingId++;
    }

    public void MakeIndoorFarm()
    {
        _buildings.Add(new Farm(_nextBuildingId));
        _nextBuildingId++;
    }

    public IReadOnlyList<Colonist> GetColonists()
    {
        IReadOnlyList<Colonist> colonists = _colonists;
        return colonists;
    }

    public int GetColonistCount()
    {
        return _colonists.Count;
    }

    public int GetMorale()
    {
        return _morale;
    }

    public int GetFood()
    {
        return _food;
    }

    public int GetWater()
    {
        return _water;
    }

    public int GetEnergy()
    {
        return _energy;
    }

    public int GetDaysSurvived()
    {
        return _daysSurvived;
    }

    public int GetProjectedFoodConsumption()
    {
        return _colonists.Count;
    }

    public int GetProjectedWaterConsumption()
    {
        return _colonists.Count;
    }

    public int GetProjectedEnergyConsumption()
    {
        int totalEnergyConsumption = 0;
        foreach (Building building in _buildings)
        {
            if (building.GetState() == ConstructionState.Complete)
            {
                totalEnergyConsumption += building.GetEnergyUpkeep();
            }
        }
        return totalEnergyConsumption;
    }

    public int GetProjectedFoodProduction()
    {
        int foodProduction = 0;
        foreach (Building building in _buildings)
        {
            if (
                building is ProductionBuilding productionBuilding
                && productionBuilding.GetResourceType() == ResourceType.Food
            )
            {
                foodProduction += productionBuilding.Produce();
            }
        }
        return foodProduction;
    }

    public int GetProjectedWaterProduction()
    {
        int waterProduction = 0;
        foreach (Building building in _buildings)
        {
            if (
                building is ProductionBuilding productionBuilding
                && productionBuilding.GetResourceType() == ResourceType.Water
            )
            {
                waterProduction += productionBuilding.Produce();
            }
        }
        return waterProduction;
    }

    public int GetProjectedEnergyProduction()
    {
        int energyProduction = 0;
        foreach (Building building in _buildings)
        {
            if (
                building is ProductionBuilding productionBuilding
                && productionBuilding.GetResourceType() == ResourceType.Energy
            )
            {
                energyProduction += productionBuilding.Produce();
            }
        }
        return energyProduction;
    }

    public int GetHousingCopacity()
    {
        return _housingCopacity;
    }

    public IReadOnlyList<Building> GetBuildings()
    {
        IReadOnlyList<Building> buildings = _buildings;
        return buildings;
    }

    public int GetAssignedWorkers(int id)
    {
        foreach (Building building in _buildings)
        {
            if (building is ProductionBuilding productionBuilding)
            {
                if (building.GetId() == id)
                {
                    return productionBuilding.GetWorkers().Count;
                }
            }
        }
        return 0;
    }

    public bool TryAssignWorker(Colonist worker, ProductionBuilding building)
    {
        if (building.IsAvailable())
        {
            this.UnassignColonist(worker);
            building.AddWorker(worker);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryAssignBuilder(Colonist builder, Building building)
    {
        if (building.IsAvailable())
        {
            building.AddBuilder(builder);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UnassignColonist(Colonist colonist)
    {
        foreach (Building building in _buildings)
        {
            building.RemoveAssignedColonist(colonist);
        }
    }

    public bool IsAssigned(Colonist colonist)
    {
        foreach (Building building in _buildings)
        {
            if (building.HasAssignedColonist(colonist))
            {
                return true;
            }
        }
        return false;
    }

    public string GetColonistAssignment(Colonist colonist)
    {
        if (this.IsAssigned(colonist))
        {
            foreach (Building building in _buildings)
            {
                if (building.HasAssignedColonist(colonist))
                {
                    return building.GetName();
                }
            }
        }
        return "Unassigned";
    }

    public bool TryStartConstruction(Building building)
    {
        if (building.CrewReady())
        {
            int cost = building.TryBeginConstruction(_energy);
            if (cost != 0)
            {
                _energy -= cost;
                return true;
            }
        }
        return false;
    }

    public bool AdvanceDay()
    {
        // calculate and production
        _daysSurvived += 1;
        foreach (Building building in _buildings)
        {
            if (building.GetState() == ConstructionState.UnderConstruction)
            {
                building.AdvanceConstruction();
            }
            if (building.GetState() == ConstructionState.Complete)
            {
                if (building is ProductionBuilding productionBuilding)
                {
                    //make sure there is enough energy to run the building
                    if (!_poweroutage)
                    {
                        switch (productionBuilding.GetResourceType())
                        {
                            case ResourceType.Food:
                                _food += productionBuilding.Produce();
                                break;
                            case ResourceType.Water:
                                _water += productionBuilding.Produce();
                                break;
                            case ResourceType.Energy:
                                _energy += productionBuilding.Produce();
                                break;
                        }
                    }
                }
            }
            //handle energy upkeep
            _energy -= building.GetEnergyUpkeep();
            //check for power outage
            if (_energy < 0)
            {
                _poweroutage = true;
            }
            else
            {
                _poweroutage = false;
            }
        }
        //calculate and handle costs
        foreach (Colonist colonist in _colonists)
        {
            _food--;
            _water--;
        }
        if (_food < 0)
        {
            if (_recovering)
            {
                return false;
            }
            _daysSurvived += 1;
            _recovering = true;
            _food = 0;
            return true;
        }
        if (_water < 0)
        {
            if (_recovering)
            {
                return false;
            }
            _daysSurvived += 1;
            _recovering = true;
            _water = 0;
            return true;
        }
        _energy -= 3; //energy cost for heating
        //check for power outage
        if (_energy < 0)
        {
            _poweroutage = true;
        }
        else
        {
            _poweroutage = false;
        }
        return true;
    }

    public void logColonyStatus()
    {
        Console.WriteLine($"Days Survived: {_daysSurvived}");
        Console.WriteLine($"Food: {_food}");
        Console.WriteLine($"Water: {_water}");
        Console.WriteLine($"Energy: {_energy}");
        Console.WriteLine($"Morale: {_morale}");
        Console.WriteLine($"Housing Capacity: {_housingCopacity}");
        Console.WriteLine($"Recovering: {_recovering}");
        Console.WriteLine($"Power Outage: {_poweroutage}");
    }

    public string GetBuildingsDisplay()
    {
        string fullString = "WORKSTATIONS\n";
        foreach (Building building in _buildings)
        {
            if (building is ProductionBuilding productionBuilding)
            {
                string line =
                    $"{building.GetId()} {building.GetName()}          {this.GetAssignedWorkers(building.GetId())}/{productionBuilding.GetWorkerCapacity()} workers";
                fullString += line + "\n";
            }
        }
        return fullString;
    }

    public string GetColonistsDisplay()
    {
        string fullString = "COLONISTS\n";
        int index = 0;
        fullString += "# Name       Specialty    Assignment           Health\n";
        foreach (Colonist colonist in _colonists)
        {
            index++;
            string line =
                $"{index} {colonist.GetName()}     {colonist.GetSpecialty()}     {this.GetColonistAssignment(colonist)}       {(colonist.IsSick() ? "Sick" : "Healthy")}";
            fullString += line + "\n";
        }
        return fullString;
    }

    public string GetResourceForcastDisplay()
    {
        return $@"
RESOURCE FORECAST
Resource    Current    Production    Consumption    Expected
Food           {_food}          +{this.GetProjectedFoodProduction()}             -{this.GetProjectedFoodConsumption()}            {this.GetFood() + this.GetProjectedFoodProduction() - this.GetProjectedFoodConsumption()}
Water          {_water}          +{this.GetProjectedWaterProduction()}             -{this.GetProjectedWaterConsumption()}            {this.GetWater() + this.GetProjectedWaterProduction() - this.GetProjectedWaterConsumption()}
Energy         {_energy}          +{this.GetProjectedEnergyProduction()}             -{this.GetProjectedEnergyConsumption()}            {this.GetEnergy() + this.GetProjectedEnergyProduction() - this.GetProjectedEnergyConsumption()}
        ";
    }

    public string GetConstructionDisplay()
    {
        string fullString = "CONSTRUCTION\n";
        foreach (Building building in _buildings)
        {
            if (building.GetState() == ConstructionState.UnderConstruction)
            {
                string line =
                    $"{building.GetId()} {building.GetName()}          {building.GetConstructionDaysRemaining()} days remaining";
                fullString += line + "\n";
            }
        }
        if (fullString == "CONSTRUCTION\n")
        {
            fullString += "No projects underway.\n";
        }
        return fullString;
    }

    public string GetConstructionAssignmentsDisplay()
    {
        string fullString = "Construction assignments: ";
        bool assignmentsAvailable = false;
        foreach (Building building in _buildings)
        {
            if (
                building.GetState() == ConstructionState.planned
                || building.GetState() == ConstructionState.UnderConstruction
            )
            {
                if (building.BuilderNeeded())
                {
                    assignmentsAvailable = true;
                    fullString +=
                        $"{building.GetId()} {building.GetName()}  Builders {building.GetBuilders().Count()}/{building.GetRequiredBuildersCount()} Days Remaining: {building.GetConstructionDaysRemaining} Requires: {building.GetRequiredSpecialty()}";
                }
            }
        }
        if (!assignmentsAvailable)
        {
            fullString += "none available";
        }
        return fullString;
    }

    public Building TryAssign(string userInput)
    {
        if (int.TryParse(userInput, out int choice))
        {
            foreach (Building building in _buildings)
            {
                if (building.GetId() == choice)
                {
                    return building;
                }
            }
        }
        return null;
    }
}
