using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class TradingMarketView : MonoBehaviour
    {
        public delegate void OnButtoncClick();
        public static event OnButtoncClick OnSellOilClick;

        [SerializeField] private TextMeshProUGUI _oilPrice;

        public void SellAllOil()
        {
            OnSellOilClick?.Invoke();
        }

        private void OnEnable()
        {
            Subscrbe();
        }

        private void OnDisable()
        {
            Unsubscrbe();
        }

        private void Subscrbe()
        {
            TradingMarketPresenter.OnPriceChange += UpdateCurrentPrice;
        }

        private void Unsubscrbe()
        {
            TradingMarketPresenter.OnPriceChange -= UpdateCurrentPrice;
        }

        private void UpdateCurrentPrice(float price)
        {
            _oilPrice.SetText("$" + price.ToString());
        }
    }
}
