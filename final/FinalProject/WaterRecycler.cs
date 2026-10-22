public class WaterRecycler : ProductionBuilding
{
    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Scientist;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Water;
    }
}
