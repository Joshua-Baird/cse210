using System.Reflection.Metadata.Ecma335;

public class Colony
{
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
        _housingCopacity = 10;
        _recovering = false;
        _poweroutage = false;
        _colonists = new List<Colonist>();
        _buildings = new List<Building>();
    }

    public IReadOnlyList<Colonist> GetColonists()
    {
        IReadOnlyList<Colonist> colonists = _colonists;
        return colonists;
    }

    public IReadOnlyList<Building> GetBuildings()
    {
        IReadOnlyList<Building> buildings = _buildings;
        return buildings;
    }

    public bool TryAssignWorker(Colonist worker, ProductionBuilding building)
    {
        if (building.IsAvailable())
        {
            building.AddWorker(worker);
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TryAssignBuilder(Colonist builder, ProductionBuilding building)
    {
        if (building.IsAvailable())
        {
            building.AddWorker(builder);
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

    public int GetHousingCopacity()
    {
        return _housingCopacity;
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
                    _energy -= building.GetEnergyUpkeep();
                    if (_energy < 0)
                    {
                        _poweroutage = true;
                    }
                }
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

    public int GetDaysSurvived()
    {
        return _daysSurvived;
    }
}
