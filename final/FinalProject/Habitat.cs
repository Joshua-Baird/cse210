public class Habitat : Building
{
    private int _energyConsumption;
    private int _capacity;

    public Habitat(int energyConsumption, int capacity)
        : base()
    {
        _energyConsumption = energyConsumption;
        _capacity = capacity;
    }

    public int GetEnergyConsumption()
    {
        return _energyConsumption;
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
