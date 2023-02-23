using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class FactoryBuilder
{
    [SerializeField] private FactoriesCollection _factoriesCollection;
    [SerializeField] private FactorySettings _currentFactorySettings;
    [SerializeField] private FactoryPresenter _factoryPresenter;
    [SerializeField] private GameObject _factory;

    [Inject]
    public FactoryBuilder(FactoriesCollection factoriesCollection,
        [Inject(Id = "FirstLevel")] FactorySettings settings, 
        FactoryPresenter factoryPresenter)
    {
        _factoriesCollection = factoriesCollection;
        _currentFactorySettings = settings;

        BuildCurrentFactory();
        _factoryPresenter = factoryPresenter;
    }

    public void UpgradeFactory()
    {
        var nextFactory = _factoriesCollection.GetNextFactory(_currentFactorySettings);
        Debug.Log($"UpgradeFactory new level is: {nextFactory.level}");


        if (nextFactory == null) 
        {
            Debug.Log("NextFactory is not exist");
            return;
        }

        _currentFactorySettings = nextFactory;
        _factoryPresenter.InstallNewSettings(nextFactory);

        GameObject.Destroy(_factory);
        BuildCurrentFactory();
    }

    public bool CheckNextFactoryLevel()
    {
        if (_factoriesCollection.CheckNextFactoryLevel(_currentFactorySettings))
            return true;

        return false;
    }

    public int GetUpgradeCost()
    {
        if (CheckNextFactoryLevel())
        {
            var nextFactory = _factoriesCollection.GetNextFactory(_currentFactorySettings);

            if (nextFactory != null)
            {
                var cost = nextFactory.buildingCost;

                Debug.Log($"COST OF UPGRADE level {nextFactory.level} is : ${cost}");
                return cost;
            }
        }

        return 0;
    }

    public int GetCurrentLevel()
    {
        return _currentFactorySettings.level;
    }

    public int GetNextLevel()
    {
        if (CheckNextFactoryLevel())
            return _factoriesCollection.GetNextFactory(_currentFactorySettings).level;

        return 0;
    }

    private void BuildCurrentFactory()
    {
        _factory = new GameObject();
        _factory.transform.position = Vector3.zero;
        _factory.name = "Factory";

        GameObject newFactoryObject
            = GameObject.Instantiate
            (_currentFactorySettings.Prefab,
            Vector3.zero,
            Quaternion.identity,
            _factory.transform);
    }
}
