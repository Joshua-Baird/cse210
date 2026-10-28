using System.Reflection.Metadata.Ecma335;

public class Colony
{
    private int _nextBuildingId;
    private int _daysSurvived;
    private int _food;
    private int _energy;
    private int _water;
    private int _morale;
    private bool _recovering;
    private bool _powerOutage;
    private List<Colonist> _colonists;
    private List<Building> _buildings;
    private int _numColonistsFed;
    private int _foodProducedToday;
    private int _energyProducedToday;
    private int _waterProducedToday;
    private int _foodConsumedToday;
    private int _energyConsumedToday;
    private int _waterConsumedToday;

    public Colony()
    {
        _daysSurvived = 0;
        _food = 18;
        _energy = 12;
        _water = 18;
        _morale = 70;
        _recovering = false;
        _powerOutage = false;
        _colonists =
        [
            new Colonist("Freyja", Specialty.Scientist),
            new Colonist("Aron", Specialty.Engineer),
            new Colonist("Mira", Specialty.Farmer),
            new Colonist("Jonas"),
            new Colonist("Nora"),
            new Colonist("Leah"),
        ];
        List<Colonist> farmWorkers = [_colonists[2], _colonists[5]];
        List<Colonist> waterWorkers = [_colonists[0], _colonists[4]];
        List<Colonist> powerWorkers = [_colonists[1], _colonists[3]];

        _buildings =
        [
            new Farm(1, 0, farmWorkers),
            new WaterRecycler(2, 0, waterWorkers),
            new PowerPlant(3, 0, powerWorkers),
            new Habitat(4, 0),
            new Habitat(5, 0),
        ];
        _nextBuildingId = 6;
        _numColonistsFed = 0;
    }

    public bool TryAddHabitat(Colonist colonist)
    {
        Habitat habitat = new Habitat(_nextBuildingId);
        this.TryAssignBuilder(colonist, habitat);
        int cost = habitat.TryBeginConstruction(_energy);
        if (cost != 0)
        {
            _energy -= cost;
            _energyConsumedToday -= cost;
            _buildings.Add(habitat);
            _nextBuildingId++;
            return true;
        }
        return false;
    }

    public bool TryAddPowerPlant(Colonist colonist)
    {
        PowerPlant powerPlant = new PowerPlant(_nextBuildingId);
        this.TryAssignBuilder(colonist, powerPlant);
        int cost = powerPlant.TryBeginConstruction(_energy);
        if (cost != 0)
        {
            _energy -= cost;
            _energyConsumedToday -= cost;
            _buildings.Add(powerPlant);
            _nextBuildingId++;
            return true;
        }
        return false;
    }

    public bool TryAddWaterRecycler(Colonist colonist)
    {
        WaterRecycler waterRecycler = new WaterRecycler(_nextBuildingId);
        this.TryAssignBuilder(colonist, waterRecycler);
        int cost = waterRecycler.TryBeginConstruction(_energy);
        if (cost != 0)
        {
            _energy -= cost;
            _energyConsumedToday -= cost;
            _buildings.Add(waterRecycler);
            _nextBuildingId++;
            return true;
        }
        return false;
    }

    public bool TryAddIndoorFarm(Colonist colonist)
    {
        Farm farm = new Farm(_nextBuildingId);
        this.TryAssignBuilder(colonist, farm);
        int cost = farm.TryBeginConstruction(_energy);
        if (cost != 0)
        {
            _energy -= cost;
            _energyConsumedToday -= cost;
            _buildings.Add(farm);
            _nextBuildingId++;
            return true;
        }
        return false;
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
        //starts at 3 for base heating requirments
        int totalEnergyConsumption = 3;
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

    public int GetHousingCapacity()
    {
        int housingCapacity = 0;
        foreach (Building building in _buildings)
        {
            if (
                building is Habitat habitat
                && habitat.GetState() == ConstructionState.Complete
            )
            {
                housingCapacity += habitat.GetCapacity();
            }
        }
        return housingCapacity;
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
            this.UnassignColonist(builder);
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
                _energyConsumedToday -= cost;
                return true;
            }
        }
        return false;
    }

    public bool AdvanceDay()
    {
        // calculate and production
        _foodProducedToday = 0;
        _waterProducedToday = 0;
        _energyProducedToday = 0;
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
                    //if its a powerplant it can run in power outage
                    if (productionBuilding is PowerPlant)
                    {
                        _energy += productionBuilding.Produce();
                        _energyProducedToday += productionBuilding.Produce();
                    }
                    //make sure there is enough energy to run the building
                    if (!_powerOutage)
                    {
                        switch (productionBuilding.GetResourceType())
                        {
                            case ResourceType.Food:
                                _food += productionBuilding.Produce();
                                _foodProducedToday +=
                                    productionBuilding.Produce();
                                break;
                            case ResourceType.Water:
                                _water += productionBuilding.Produce();
                                _waterProducedToday +=
                                    productionBuilding.Produce();
                                break;
                        }
                    }
                }
                //handle energy upkeep
                _energy -= building.GetEnergyUpkeep();
                _energyConsumedToday -= building.GetEnergyUpkeep();
            }
            //check for power outage
            if (_energy < 0)
            {
                _powerOutage = true;
            }
            else
            {
                _powerOutage = false;
            }
        }
        //calculate and handle costs
        _energy -= 3; //energy cost for heating
        _energyConsumedToday -= 3;
        _numColonistsFed = 0;
        _waterConsumedToday = 0;
        _foodConsumedToday = 0;
        foreach (Colonist colonist in _colonists)
        {
            if (_food != 0 && _water != 0)
            {
                _food--;
                _water--;
                _foodConsumedToday++;
                _waterConsumedToday++;
                _numColonistsFed += 1;
            }
        }
        if (_food <= 0)
        {
            if (_recovering)
            {
                return false;
            }
            _recovering = true;
            _food = 0;
            return true;
        }
        if (_water <= 0)
        {
            if (_recovering)
            {
                return false;
            }
            _recovering = true;
            _water = 0;
            return true;
        }
        //check for power outage
        if (_energy <= 0)
        {
            _powerOutage = true;
        }
        else
        {
            _powerOutage = false;
        }
        return true;
    }

    public string GetBuildingsDisplay()
    {
        string fullString = "WORKSTATIONS\n";
        foreach (Building building in _buildings)
        {
            if (building is ProductionBuilding productionBuilding)
            {
                string line =
                    $"{building.GetId(), -2} {building.GetName(), -10} {this.GetAssignedWorkers(building.GetId()), -1}/{productionBuilding.GetWorkerCapacity(), -1} workers";
                fullString += line + "\n";
            }
        }
        return fullString;
    }

    public string GetColonistsDisplay()
    {
        string fullString = "COLONISTS\n";
        int index = 0;
        fullString += "#  Name       Specialty    Assignment\n";
        foreach (Colonist colonist in _colonists)
        {
            index++;
            string line =
                $"{index, -2} {colonist.GetName(), -10} {colonist.GetSpecialty(), -12} {this.GetColonistAssignment(colonist), -20}";
            fullString += line + "\n";
        }
        return fullString;
    }

    public string GetResourceForecastDisplay()
    {
        return $@"
RESOURCE FORECAST
Resource    Current    Production    Consumption    Expected
Food           {_food}          +{this.GetProjectedFoodProduction()}             -{this.GetProjectedFoodConsumption()}            {this.GetFood() + this.GetProjectedFoodProduction() - this.GetProjectedFoodConsumption()}
Water          {_water}          +{this.GetProjectedWaterProduction()}             -{this.GetProjectedWaterConsumption()}            {this.GetWater() + this.GetProjectedWaterProduction() - this.GetProjectedWaterConsumption()}
Energy         {_energy}          +{this.GetProjectedEnergyProduction()}             -{this.GetProjectedEnergyConsumption()}            {this.GetEnergy() + this.GetProjectedEnergyProduction() - this.GetProjectedEnergyConsumption()}
        ";
    }

    public string GetResourceReportDisplay()
    {
        string s =
            $@"
RESOURCE REPORT
Resource    Amount    Produced    Consumed    
Food           {_food}          +{_foodProducedToday}             -{_foodConsumedToday}       
Water          {_water}          +{_waterProducedToday}             -{_waterConsumedToday}    
Energy         {_energy}          +{_energyProducedToday}             {_energyConsumedToday}  
        ";
        _energyConsumedToday = 0;
        return s;
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
                building.GetState() == ConstructionState.Planned
                || building.GetState() == ConstructionState.UnderConstruction
            )
            {
                if (building.BuilderNeeded())
                {
                    assignmentsAvailable = true;
                    fullString +=
                        $"{building.GetId()} {building.GetName()}  Builders {building.GetBuilders().Count()}/{building.GetRequiredBuildersCount()} Days Remaining: {building.GetConstructionDaysRemaining()} Requires: {building.GetRequiredSpecialty()}";
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

    public string GetStatusReport()
    {
        string fullString =
            $@"
        {_numColonistsFed}/{_colonists.Count()} Received food and water";
        if (!_powerOutage)
        {
            fullString +=
                "\n Heating and production systems remained operational.";
        }
        else
        {
            fullString +=
                "\n There was not enough energy to power all the buildings, All systems except Geothermal Plant are now down";
        }
        return fullString;
    }
}
