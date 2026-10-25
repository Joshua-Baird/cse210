public class PowerPlant : ProductionBuilding
{
    public PowerPlant()
        : base("Power Plant", 6, 2, 0, 1, Specialty.Engineer, 2, 3, 1) { }

    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Engineer;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Energy;
    }
}
