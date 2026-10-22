using System.Configuration.Assemblies;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Colonist
{
    private string _name;
    private int _daysRemaingSick;
    private Specialty _specialty;

    public Colonist(string name)
    {
        _name = name;
        _daysRemaingSick = 0;
        _specialty = Specialty.None;
    }

    public Specialty GetSpecialty()
    {
        return _specialty;
    }

    public bool IsSick()
    {
        if (_daysRemaingSick > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string GetName()
    {
        return _name;
    }

    public void BecomeSick(int daysSick)
    {
        _daysRemaingSick = daysSick;
    }

    public void DayPassed()
    {
        if (_daysRemaingSick > 0)
        {
            _daysRemaingSick--;
        }
    }

    public void Specialize(Specialty specialty)
    {
        _specialty = specialty;
    }
}
