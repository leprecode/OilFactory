using UnityEngine;
using Zenject;

public class OilFactoryInstaller : MonoInstaller
{
    [SerializeField] FactoriesCollection _factoriesCollection;
    public override void InstallBindings()
    {
        BindFactoryCollection();
      BindFactoryBuilder();
    }

    private void BindFactoryCollection() 
        => Container
        .Bind<FactoriesCollection>()
        .FromInstance(_factoriesCollection)
        .AsSingle()
        .NonLazy();

    private void BindFactoryBuilder()
        => Container.Bind<FactoryBuilder>().AsSingle().NonLazy();
}
