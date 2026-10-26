using System.Net.Sockets;

public class Habitat : Building
{
    private int _capacity;

    public Habitat(int id)
        : base(id, "Habitat", 8, 2, 1, 1, Specialty.Engineer)
    {
        _capacity = 4;
    }

    public Habitat(int id, int constructionDaysRemaining)
        : base(
            id,
            "Habitat",
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
        throw new NotImplementedException();
    }
}
