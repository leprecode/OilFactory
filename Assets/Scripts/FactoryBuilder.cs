using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;

[Serializable]
public class FactoryBuilder
{
    private const int startProducedOil = 40;
    [SerializeField] private FactoriesCollection _factoriesCollection;
    [SerializeField] private FactoryPresenter _factoryPresenter;
    [SerializeField] private GameObject _factory;

    [Inject]
    public FactoryBuilder(FactoriesCollection factoriesCollection)
    {
        _factoriesCollection = factoriesCollection;

        BuildFactory(0);
    }

    private void BuildFactory(int levelOfFactory)
    {
        var newFactorySettings = _factoriesCollection.GetFactory(levelOfFactory);

        _factory = new GameObject();
        _factory.transform.position = Vector3.zero;
        _factory.name = "Factory";
        _factoryPresenter = _factory.AddComponent<FactoryPresenter>();
        _factoryPresenter.Construct(newFactorySettings, new FactoryModel(startProducedOil));

        GameObject newFactoryObject
            = GameObject.Instantiate
            (newFactorySettings.Prefab,
            Vector3.zero, 
            Quaternion.identity, 
            _factory.transform);


    }
}
