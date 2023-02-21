using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;

[Serializable]
public class OilFactoryBuilder
{
    [SerializeField] private FactoriesCollection _factoriesCollection;
    [SerializeField] private FactoryPresenter _factoryPresenter;

    [Inject]
    public OilFactoryBuilder(FactoriesCollection factoriesCollection,
        FactoryPresenter factoryPresenter)
    {
        _factoriesCollection = factoriesCollection;
        _factoryPresenter = factoryPresenter;

        BuildFactory(0);
    }

    private void BuildFactory(int levelOfFactory)
    { 
        var newFactorySettings = _factoriesCollection.GetFactory(levelOfFactory);
        
        if (newFactorySettings == null)
            return;

        GameObject newFactoryObject
            = GameObject.Instantiate(newFactorySettings.Prefab, Vector3.zero, Quaternion.identity);

        //TODO: закончил здесь
    }
}

public class OilFactoryInstaller : MonoInstaller
{
    [SerializeField] FactoriesCollection _factoriesCollection;
    public override void InstallBindings()
    {
        BindFactoryCollection();
        BindFactoryPresenter();
        BindFactoryBuilder();
    }

    private void BindFactoryPresenter() 
        => Container.Bind<FactoryPresenter>().AsSingle().NonLazy();

    private void BindFactoryCollection() 
        => Container.Bind<FactoriesCollection>().AsSingle().NonLazy();

    private void BindFactoryBuilder()
        => Container.Bind<OilFactoryBuilder>().AsSingle().NonLazy();
}
