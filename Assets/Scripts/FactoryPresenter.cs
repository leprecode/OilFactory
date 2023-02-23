using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class FactoryPresenter
{
    public delegate void OnOilExtractHandler(int newTotalOilValue);
    public static event OnOilExtractHandler OnOilExtract;
    public static event OnOilExtractHandler OnOilSpend;

    [SerializeField] private FactorySettings _settings;
    [SerializeField] private FactoryModel _factoryModel;

    //TODO:REMOVE HARDCODE
    private int _initialOilValue = 30;

    [Inject]
    private FactoryPresenter([Inject(Id = "FirstLevel")] FactorySettings settings, FactoryModel factoryModel, FactoryView factoryView)
    {
        _settings = settings;
        _factoryModel = factoryModel;

        _factoryModel.AddOil(_initialOilValue);
        factoryView.InitialValueint(_factoryModel.GetOil());
        Subscribe();
    }

    ~ FactoryPresenter()
    {
        Unsubscribe();
    }

    public void InstallNewSettings(FactorySettings model)
    {
        _settings = model;
    }

    public bool CheckOil()
    {
        if (_factoryModel.GetOil() > 0)
            return true;

        return false;
    }

    private void HandleOilsTimerFinish()
    {
        ExtractOil();
    }

    private void ExtractOil()
    {
        _factoryModel.AddOil(_settings.oilProductionPerTImePeriod);

        OnOilExtract?.Invoke(_factoryModel.GetOil());
    }

    private void Subscribe()
    {
        TimeTicker.OnOilsTimerEnd += HandleOilsTimerFinish;
    }

    private void Unsubscribe()
    {
        TimeTicker.OnOilsTimerEnd -= HandleOilsTimerFinish;
    }

    public int TakeAllOil()
    {
        var oil = _factoryModel.TakeAllOil();
        OnOilSpend?.Invoke(0);
        return oil;
    }
}