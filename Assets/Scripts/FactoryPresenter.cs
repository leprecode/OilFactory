using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class FactoryPresenter : MonoBehaviour
{
    public delegate void OnOilExtractHandler(int newTotalOilValue);
    public static event OnOilExtractHandler OnOilExtract;

    [SerializeField] private FactorySettings _settings;
    [SerializeField] private FactoryModel _factoryModel;

    public void Construct(FactorySettings settings, FactoryModel factoryModel)
    {
        _settings = settings;
        _factoryModel = factoryModel;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Start()
    {
        OnOilExtract?.Invoke(_factoryModel.GetOil());
    }

    public void ChangeModel(FactorySettings model)
    {
        _settings = model;
    }

    private void HandleDayEnd()
    {
        //TODO: исправить на каждые десять секунд
        ExtractOil();
    }

    private void ExtractOil()
    {
        _factoryModel.AddOil(_settings.oilProductionPerTImePeriod);

        OnOilExtract?.Invoke(_factoryModel.GetOil());
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