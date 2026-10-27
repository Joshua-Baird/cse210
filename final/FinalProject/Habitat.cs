using System.Net.Sockets;

public class Habitat : Building
{
    private int _capacity;

    public Habitat()
        : base(-1, "Heated Shelter", 8, 2, 1, 1, Specialty.Engineer)
    {
        _capacity = 4;
    }

    public Habitat(int id)
        : base(id, "Heated Shelter", 8, 2, 1, 1, Specialty.Engineer)
    {
        _capacity = 4;
    }

    public Habitat(int id, int constructionDaysRemaining)
        : base(
            id,
            "Heated Shelter",
            8,
            constructionDaysRemaining,
            1,
            1,
            Specialty.Engineer
        )
    {
        _capacity = 4;
    }

    public int GetCapacity()
    {
        return _capacity;
    }

    public override Specialty GetRequiredSpecialty()
    {
        return Specialty.Engineer;
    }
}
