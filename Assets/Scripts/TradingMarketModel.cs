using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class TradingMarketModel
    {
        private readonly List<float> _points;
        private int _trackIndex;
        [field: SerializeField] public float currentPriceForOil { get; private set; }

        public TradingMarketModel([Inject(Id = "Points")] List<float> points)
        {
            this._points = points;
            _trackIndex = 10;
            //RemoveFirstPoints();
        }
        public void InitialValue(float value)
        {
            currentPriceForOil = value;
        }
        public void SetCurrentOilPrice()
        {
                currentPriceForOil = _points[_trackIndex];
        }

        public float GetNextPoint()
        {
            return _points[++_trackIndex];
        }

        public int GetPointIndex()
        {
            return _trackIndex;
        }


        private void RemoveFirstPoints()
        {
            _points.RemoveRange(0,10);
            _trackIndex = 10;
        }
    }
}
