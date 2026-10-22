using System.Dynamic;
using System.Reflection;
using System.Runtime.CompilerServices;

public abstract class Building
{
    private string _type;
    private int _constructionEnergyCost;
    private int _constructionDaysRemaining;
    private int _energyUpkeep;
    private int _requiredBuilderCount;
    private ConstructionState _state;
    protected Specialty _requiredSpecialty;
    private List<Colonist> _builders;

    public string GetTye()
    {
        return _type;
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
}
