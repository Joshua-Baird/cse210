public class WaterRecycler : ProductionBuilding
{
    public WaterRecycler(int id)
        : base(id, "Water Recycler", 6, 2, 1, 1, Specialty.Scientist, 2, 3, 1)
    { }

    public WaterRecycler(int id, int constructionDaysRemaining)
        : base(
            id,
            "Water Recycler",
            6,
            constructionDaysRemaining,
            1,
            1,
            Specialty.Scientist,
            2,
            3,
            1
        ) { }

    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Scientist;
    }

    public override ResourceType GetResourceType()
    {
        return ResourceType.Water;
    }
}
