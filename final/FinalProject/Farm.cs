public class Farm : ProductionBuilding
{
    public Farm()
        : base(-1, "Indoor Farm", 6, 2, 1, 1, Specialty.Farmer, 2, 3, 1) { }

    public Farm(int id)
        : base(id, "Indoor Farm", 6, 2, 1, 1, Specialty.Farmer, 2, 3, 1) { }

    public Farm(int id, int constructionDaysRemaining, List<Colonist> workers)
        : base(
            id,
            "Indoor Farm",
            6,
            constructionDaysRemaining,
            1,
            1,
            Specialty.Farmer,
            2,
            3,
            1,
            workers
        ) { }

    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Farmer;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Food;
    }
}
