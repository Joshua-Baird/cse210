public class Farm : ProductionBuilding
{
    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Farmer;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Food;
    }
}
