using System;
using Zenject;

namespace Assets.Scripts.Bank
{
    public class BankPresenter
    {
        public delegate void OnMoneyChange(int newMoneyCount);
        public static event OnMoneyChange OnAddMoney;
        public static event OnMoneyChange OnSpendMoney;

        private BankModel _model;

        [Inject]
        public BankPresenter(BankModel model, BankView bankView)
        {
            _model = model;
            bankView.InitialMoneyCounter(_model.money);
        }

        public void AddMoney(int value)
        {
            if (value > 0)
            {
                _model.AddMoney(value);
                OnAddMoney?.Invoke(value);
            }
        }

        public bool TryBuy(int cost)
        {
            if (CheckIsEnoughMoney(cost))
            {
                SpendMoney(cost);
                return true;
            }

            return false;
        }

        private bool CheckIsEnoughMoney(int cost)
        {
            if (cost <= _model.money)
            {
                return true;
            }

            return false;
        }

        private void SpendMoney(int value)
        {
            if (value > 0)
            {
                _model.SpendMoney(value);
                OnSpendMoney?.Invoke(value);
            }
        }

    }
}
