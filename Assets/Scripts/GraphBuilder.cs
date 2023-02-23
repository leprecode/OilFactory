using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Bank;
using Zenject;

namespace Assets.Scripts
{
    public class GraphBuilder : MonoBehaviour
    {
        private const float _graphWidthOffset = 0.9f;
        [SerializeField] private float _graphOffsetFromTop = 1.2f;
        [SerializeField] private float _widthOfConnection = 3f;
        [SerializeField] private int _pointSize = 11;
        [SerializeField] private Sprite _circleSprite;
        [SerializeField] private RectTransform _graphContainer;
        [SerializeField] Color _dotColor;
        [SerializeField] Color _connectionColor;
        [SerializeField] private FileReader _fileReader;

        private void Start()
        {
              CreateGraph(_fileReader.oilTracks,10);
            //CreateGraph(new List<double> { 90, 110, 80, 65, 33, 101, 89, 99, 10, 63}, 10);
        }

        public void AddPointToGraph(double pointValue)
        {
            
        
        }

        private void CreateGraph(List<double> valueList, int desiredPointsCount)
        {
            int pointsCount = DefineMaxPointCountToCreate(valueList, desiredPointsCount);
            float yMaximum = CalculateMaxY(valueList.GetRange(0,pointsCount));
            float graphHeight = _graphContainer.sizeDelta.y;
            float xSize = CalculateDistributionOnX(pointsCount);

            GameObject lastCircleGameObject = null;

            for (int i = 0; i < pointsCount; i++)
            {
                float xPosition = i * xSize;
                float yPosition = (float)((valueList[i] / yMaximum) * graphHeight);

                GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));

                if (lastCircleGameObject != null)
                {
                    CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                }

                lastCircleGameObject = circleGameObject;
            }
        }

        private float CalculateMaxY(List<double> valueList)
        {
            return (float)valueList.Max() * _graphOffsetFromTop;
        }
        private float CalculateDistributionOnX(int pointsCount)
        {
            return (_graphContainer.rect.width* _graphWidthOffset) / pointsCount;
        }

        private int DefineMaxPointCountToCreate(List<double> valueList, int maxCountGraphPoints)
        {
            int pointsCount;
            if (maxCountGraphPoints > valueList.Count)
                pointsCount = valueList.Count;
            else
                pointsCount = maxCountGraphPoints;
            return pointsCount;
        }

        private GameObject CreateCircle(Vector2 anchoredPosition)
        {
            GameObject newCircle = new GameObject("circle", typeof(Image));
            newCircle.transform.SetParent(_graphContainer, false);
            newCircle.GetComponent<Image>().sprite = _circleSprite;
            newCircle.GetComponent<Image>().color = _dotColor;
            RectTransform rectTransform = newCircle.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(_pointSize, _pointSize);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            return newCircle;
        }

        private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
        {
            GameObject newConnection = new GameObject("dotConnection", typeof(Image));
            newConnection.transform.SetParent(_graphContainer, false);
            newConnection.GetComponent<Image>().color = _connectionColor;

            RectTransform rectTransform = newConnection.GetComponent<RectTransform>();
            Vector2 dir = (dotPositionB - dotPositionA).normalized;
            float distance = Vector2.Distance(dotPositionA, dotPositionB);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, _widthOfConnection);
            rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
            rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        }
    }


    public class TradingMarketModel
    {
        [field: SerializeField] private double _currentPriceForOil;


    }

    public class TradingMarketPresenter 
    {
        private readonly FactoryPresenter factoryPresenter;
        private readonly BankPresenter bankPresenter;
        private readonly TradingMarketModel tradingMarket;

        [Inject]
        public TradingMarketPresenter(FactoryPresenter factoryPresenter, 
            BankPresenter bankPresenter, TradingMarketModel tradingMarket)
        {
            this.factoryPresenter = factoryPresenter;
            this.bankPresenter = bankPresenter;
            this.tradingMarket = tradingMarket;

            Subscribe();
        }

        ~TradingMarketPresenter()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            TradingMarketView.OnSellOilClick += HandleOnSellOilClick;
        }

        private void Unsubscribe()
        {
            TradingMarketView.OnSellOilClick -= HandleOnSellOilClick;
        }

        private void HandleOnSellOilClick()
        {
            //Выяснить есть ли нефть
            
            //1. Нет. Бросить попап что все продано

            //2. Есть
                //Берем всю нефть и расходуем ее
                //Перемножаем количество нефти на нынешний курс и отправляем в банк
        }

    }

    public class TradingMarketView
    {
        public delegate void OnButtoncClick();
        public static event OnButtoncClick OnSellOilClick;

        public void SellAllOil()
        {
            OnSellOilClick?.Invoke();
        }
    }
}
