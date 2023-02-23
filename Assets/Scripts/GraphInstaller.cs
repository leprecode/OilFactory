using Assets.Scripts;
using UnityEngine;
using Zenject;

public class GraphInstaller : MonoInstaller
{
    [SerializeField] private GraphBuilder _builder;
    

    public override void InstallBindings()
    {
        BindGraphBuilder();
    }


    private void BindGraphBuilder()
        => Container
        .Bind<GraphBuilder>()
        .FromInstance(_builder)
        .AsSingle()
        .NonLazy();
}

