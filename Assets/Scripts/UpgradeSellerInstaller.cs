using Zenject;

public class UpgradeSellerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindUpgradeSeller();
    }

    private void BindUpgradeSeller()
    {
        Container.Bind<UpgradeSeller>().AsSingle().NonLazy();
    }
}