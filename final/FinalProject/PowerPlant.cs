public class PowerPlant : ProductionBuilding
{
    public PowerPlant()
        : base(-1, "Geothermal Plant", 6, 2, 1, 1, Specialty.Engineer, 3, 3, 1)
    { }

    public PowerPlant(int id)
        : base(id, "Geothermal Plant", 6, 2, 0, 1, Specialty.Engineer, 3, 3, 1)
    { }

    public PowerPlant(
        int id,
        int constructionDaysRemaining,
        List<Colonist> workers
    )
        : base(
            id,
            "Geothermal Plant",
            6,
            constructionDaysRemaining,
            1,
            1,
            Specialty.Engineer,
            3,
            3,
            1,
            workers
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
