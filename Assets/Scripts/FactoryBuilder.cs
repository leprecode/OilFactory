using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class FactoryBuilder
{
    private const int startProducedOil = 40;
    [SerializeField] private FactoriesCollection _factoriesCollection;
    [SerializeField] private FactorySettings _currentFactorySettings;
    [SerializeField] private FactoryPresenter _factoryPresenter;
    [SerializeField] private GameObject _factory;

    [Inject]
    public FactoryBuilder(FactoriesCollection factoriesCollection)
    {
        _factoriesCollection = factoriesCollection;

        BuildFactory(0);
    }

    public void InstallNextFactoryLevel()
    {
        _currentFactorySettings = _factoriesCollection.GetNextFactory(_currentFactorySettings);
        _factoryPresenter.InstallNewSettings(_currentFactorySettings);
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

    private void BuildFactory(int levelOfFactory)
    {
        _currentFactorySettings = _factoriesCollection.GetFactory(levelOfFactory);

        _factory = new GameObject();
        _factory.transform.position = Vector3.zero;
        _factory.name = "Factory";
        _factoryPresenter = _factory.AddComponent<FactoryPresenter>();
        _factoryPresenter.Construct(_currentFactorySettings, new FactoryModel(startProducedOil));

        GameObject newFactoryObject
            = GameObject.Instantiate
            (_currentFactorySettings.Prefab,
            Vector3.zero,
            Quaternion.identity,
            _factory.transform);


    }
}
