public class Colony
{
    private int _daysSurvived;
    private int _food;
    private int _energy;
    private int _water;
    private int _morale;
    private int _housingCopacity;
    private int _dailyEnergyCost;
    private List<Colonist> _colonists;
    private List<Building> _buildings;

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
}
