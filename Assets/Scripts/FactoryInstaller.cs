using UnityEngine;
using Zenject;

public class FactoryInstaller : MonoInstaller
{
    [SerializeField] private FactoriesCollection _factoriesCollection;
    [SerializeField] private FactoryView _factoryView;

    public override void InstallBindings()
    {
        BindFactoryCollection();
        BindFactoryBuilder();
        BindFactoryView();
        BindFactoryPresenter();

        BindInitialFactorySettings();
        BindFactoryModel();
    }
    private void BindInitialFactorySettings()
    {
        Container.Bind<FactorySettings>().WithId("FirstLevel").FromInstance(_factoriesCollection.GetFactory(0)).AsCached().NonLazy();
    }

    private void BindFactoryPresenter()
    {
        Container.Bind<FactoryPresenter>().AsSingle().NonLazy();
    }

    private void BindFactoryModel()
    {
        Container.Bind<FactoryModel>().AsSingle().NonLazy();
    }

    private void BindFactoryCollection()
        => Container
        .Bind<FactoriesCollection>()
        .FromInstance(_factoriesCollection)
        .AsSingle()
        .NonLazy();

    private void BindFactoryBuilder()
        => Container.Bind<FactoryBuilder>().AsSingle().NonLazy();

    private void BindFactoryView()
    => Container.Bind<FactoryView>().FromInstance(_factoryView).AsSingle().NonLazy();
}

