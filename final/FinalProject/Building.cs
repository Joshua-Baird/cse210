using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow;

public abstract class Building
{
    private int _id;
    private string _name;
    private int _constructionEnergyCost;
    private int _constructionDaysRemaining;
    private int _energyUpkeep;
    private int _requiredBuilderCount;
    private ConstructionState _state;
    protected Specialty _requiredSpecialty;
    private List<Colonist> _builders;

    public Building(
        int id,
        string name,
        int constructionEnergyCost,
        int constructionDaysRemaining,
        int energyUpkeep,
        int requiredBuilderCount,
        Specialty requiredSpecialty
    )
    {
        _id = id;
        _name = name;
        _constructionEnergyCost = constructionEnergyCost;
        _constructionDaysRemaining = constructionDaysRemaining;
        _energyUpkeep = energyUpkeep;
        _requiredBuilderCount = requiredBuilderCount;
        _requiredSpecialty = requiredSpecialty;
        if (_constructionDaysRemaining <= 0)
        {
            _state = ConstructionState.Complete;
        }
        else
        {
            _state = ConstructionState.planned;
        }
        _builders = new List<Colonist>();
    }

    public string GetName()
    {
        return _name;
    }

    public abstract Specialty GetRequiredSpecialty();

    public IReadOnlyList<Colonist> GetBuilders()
    {
        IReadOnlyList<Colonist> builders = _builders;
        return builders;
    }

    public virtual bool AddBuilder(Colonist colonist)
    {
        _builders.Add(colonist);
        return true;
    }

    public void RemoveBuilder(Colonist colonist)
    {
        _builders.Remove(colonist);
    }

    public virtual bool CrewReady()
    {
        if (_builders.Count() == _requiredBuilderCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AdvanceConstruction()
    {
        if (this.CrewReady() && _constructionDaysRemaining > 0)
        {
            _constructionDaysRemaining--;
            if (_constructionDaysRemaining <= 0)
            {
                _state = ConstructionState.Complete;
            }
        }
    }

    public ConstructionState GetState()
    {
        return _state;
    }

    public bool BuilderNeeded()
    {
        if (_builders.Count() > _requiredBuilderCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual bool HasAssignedColonist(Colonist colonist)
    {
        foreach (Colonist builder in _builders)
        {
            if (builder.GetName() == colonist.GetName())
            {
                return true;
            }
        }
        return false;
    }

    public virtual void RemoveAssignedColonist(Colonist colonist)
    {
        _builders.Remove(colonist);
    }

    public int TryBeginConstruction(int energy)
    {
        //returns the cost of construction or 0 if there is not enough energy to begin construction

        if (energy >= _constructionEnergyCost)
        {
            _state = ConstructionState.UnderConstruction;
            return _constructionEnergyCost;
        }
        else
        {
            return 0;
        }
    }

    public int GetEnergyUpkeep()
    {
        return _energyUpkeep;
    }

    public int GetConstructionDaysRemaining()
    {
        return _constructionDaysRemaining;
    }

    public int GetRequiredBuildersCount()
    {
        return _requiredBuilderCount;
    }

    public int GetId()
    {
        return _id;
    }

    public virtual bool IsAvailable()
    {
        if (_builders.Count() < _requiredBuilderCount)
        {
            return true;
        }
        return false;
    }
}
