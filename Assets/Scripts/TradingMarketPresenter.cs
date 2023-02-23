using UnityEngine;
using Assets.Scripts.Bank;
using Zenject;
using System;

namespace Assets.Scripts
{
    public class TradingMarketPresenter 
    {
        public delegate void OnNewOilPrice(float price);
        public static event OnNewOilPrice OnPriceChange;

        private readonly FactoryPresenter _factoryPresenter;
        private readonly BankPresenter _bankPresenter;
        private readonly TradingMarketModel _tradingMarketModel;
        private readonly GraphBuilder _graphBuilder;

        [Inject]
        public TradingMarketPresenter(FactoryPresenter factoryPresenter, 
            BankPresenter bankPresenter, 
            TradingMarketModel tradingMarket, 
            GraphBuilder graphBuilder)
        {
            this._factoryPresenter = factoryPresenter;
            this._bankPresenter = bankPresenter;
            this._tradingMarketModel = tradingMarket;
            this._graphBuilder = graphBuilder;

            //TODO: remove hardcode
            _tradingMarketModel.InitialValue(108f);
            Subscribe();
        }

        ~TradingMarketPresenter()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            TradingMarketView.OnSellOilClick += HandleOnSellOilClick;
            TimeTicker.OnDayEnd += HandleDayEnd;
        }

        private void Unsubscribe()
        {
            TradingMarketView.OnSellOilClick -= HandleOnSellOilClick;
            TimeTicker.OnDayEnd -= HandleDayEnd;
        }

        private void HandleOnSellOilClick()
        {
            if (_factoryPresenter.CheckOil())
            {
                var allOil = _factoryPresenter.TakeAllOil();
                Debug.Log($"Enough oil for selling: "+ allOil);
                var earnedMoney = Mathf.RoundToInt(allOil * (float)_tradingMarketModel.currentPriceForOil);
                Debug.Log($"Earned from selling oil: " + earnedMoney);
                _bankPresenter.AddMoney(earnedMoney);
            }
            else 
            {
                Debug.Log($"Not Enough oil for selling");

                //1. Нет. Бросить попап что все продано
            }
        }


        private void HandleDayEnd()
        {
            AddNewPoint();

            _tradingMarketModel.SetCurrentOilPrice();
        }

        private void AddNewPoint()
        {
            var newPoint = _tradingMarketModel.GetNextPoint();
            Debug.Log("new value : " + newPoint);
            _graphBuilder.AddPointToGraph(_tradingMarketModel.GetPointIndex());

            OnPriceChange?.Invoke(newPoint);
        }
    }
}
