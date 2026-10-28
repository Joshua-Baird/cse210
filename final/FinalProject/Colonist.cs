using System.Configuration.Assemblies;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class Colonist
{
    private string _name;
    private Specialty _specialty;

    public Colonist(string name)
    {
        _name = name;
        _specialty = Specialty.None;
    }

    public Colonist(string name, Specialty specialty)
    {
        _name = name;
        _specialty = specialty;
    }

    public Specialty GetSpecialty()
    {
        return _specialty;
    }

    public string GetName()
    {
        return _name;
    }

    public void Specialize(Specialty specialty)
    {
        _specialty = specialty;
    }
}
