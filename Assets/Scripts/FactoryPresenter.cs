using UnityEngine;
using Zenject;

public class FactoryPresenter
{
    [SerializeField] private FactoryModel _model;

    public FactoryPresenter()
    {
        Subscribe();
    }

    ~FactoryPresenter() 
    {
        Unsubscribe();
    }

    public void Construct(FactoryModel model)
    {
        ChangeModel(model);
    }

    private void ChangeModel(FactoryModel model)
    {
        _model = model;
    }

    private void HandleDayEnd()
    {
        Debug.Log("Oil!");
    }

    private void Subscribe()
    {
        DayTicker.OnDayEnd += HandleDayEnd;
    }

    private void Unsubscribe()
    {
        DayTicker.OnDayEnd += HandleDayEnd;
    }
}