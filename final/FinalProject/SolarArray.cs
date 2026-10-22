public class SolarArray : ProductionBuilding
{
    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Engineer;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Energy;
    }
}
