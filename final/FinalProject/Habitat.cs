public class Habitat : Building
{
    private int _capacity;

    public Habitat()
        : base("Habitat", 8, 2, 1, 1, Specialty.Engineer)
    {
        _capacity = 4;
    }

    public int GetCapacity()
    {
        return _capacity;
    }

    public override Specialty GetRequiredSpecialty()
    {
        throw new NotImplementedException();
    }
}
