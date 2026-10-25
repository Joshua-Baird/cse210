using System.Reflection;
using System.Runtime.CompilerServices;

public abstract class ProductionBuilding : Building
{
    private int _workerCapacity;
    private int _baseOutputPerWorker;
    private int _specialistBonus;
    private List<Colonist> _normalWorkers;
    private List<Colonist> _specializedWorkers;
    public abstract ResourceType GetResourceType();

    public IReadOnlyList<Colonist> GetWorkers()
    {
        IReadOnlyList<Colonist> allWorkers =
        [
            .. _specializedWorkers,
            .. _normalWorkers,
        ];
        return allWorkers;
    }

    public void AddWorker(Colonist worker)
    {
        if (worker.GetSpecialty() == _requiredSpecialty)
        {
            _specializedWorkers.Add(worker);
        }
        else
        {
            _normalWorkers.Add(worker);
        }
    }

    public void RemoveWorker(Colonist worker)
    {
        if (worker.GetSpecialty() == _requiredSpecialty)
        {
            _specializedWorkers.Remove(worker);
        }
        else
        {
            _normalWorkers.Remove(worker);
        }
    }

    public int Produce()
    {
        int total = 0;
        total += _baseOutputPerWorker * _normalWorkers.Count();
        total += _specialistBonus * _specializedWorkers.Count();
        return total;
    }

    public bool IsAvailable()
    {
        if (_workerCapacity <= this.GetWorkers().Count())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool HasAssignedColonist(Colonist colonist)
    {
        if (!base.HasAssignedColonist(colonist))
        {
            foreach (Colonist worker in this.GetWorkers())
            {
                if (worker.GetName() == colonist.GetName())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override void RemoveAssignedColonist(Colonist colonist)
    {
        base.RemoveAssignedColonist(colonist);
        _normalWorkers.Remove(colonist);
        _specializedWorkers.Remove(colonist);
    }
}
