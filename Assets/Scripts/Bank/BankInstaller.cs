
using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Bank
{
    public class BankInstaller : MonoInstaller
    {
        [SerializeField] private BankView _bankView;
        [SerializeField] private int _initialMoneyCount;
        public override void InstallBindings()
        {
            BindBankView();

            BindBankModel();

            BindBankPresenter();
        }

        private void BindBankPresenter()
        {
            Container.Bind<BankPresenter>().AsSingle().NonLazy();
        }

        private void BindBankModel()
        {
            BankModel model = new BankModel(_initialMoneyCount);
            Container.Bind<BankModel>().FromInstance(model).AsSingle().NonLazy();
        }

        private void BindBankView()
        {
            Container.Bind<BankView>().FromInstance(_bankView).AsSingle().NonLazy();
        }
    }
}
