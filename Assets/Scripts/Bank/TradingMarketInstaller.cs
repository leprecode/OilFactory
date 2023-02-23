using Zenject;

namespace Assets.Scripts.Bank
{
    public class TradingMarketInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTradingMarketModel();
            BindTradingMarketPresenter();
        }

        private void BindTradingMarketPresenter()
        {
            Container.Bind<TradingMarketPresenter>().AsSingle().NonLazy();
        }

        private void BindTradingMarketModel()
        {
            Container.Bind<TradingMarketModel>().AsSingle().NonLazy();
        }
    }
}
