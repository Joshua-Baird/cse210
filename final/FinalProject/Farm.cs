public class Farm : ProductionBuilding
{
    public Farm()
        : base("Farm", 6, 2, 1, 1, Specialty.Farmer, 2, 3, 1) { }

    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Farmer;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Food;
    }
}
