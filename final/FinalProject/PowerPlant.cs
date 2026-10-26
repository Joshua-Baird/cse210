public class PowerPlant : ProductionBuilding
{
    public PowerPlant(int id)
        : base(id, "Geothermal Plant", 6, 2, 0, 1, Specialty.Engineer, 2, 3, 1)
    { }

    public PowerPlant(int id, int constructionDaysRemaining)
        : base(
            id,
            "Geothermal Plant",
            6,
            constructionDaysRemaining,
            0,
            1,
            Specialty.Engineer,
            2,
            3,
            1
        ) { }

    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Engineer;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Energy;
    }
}
