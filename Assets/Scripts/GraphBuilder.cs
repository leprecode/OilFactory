using CodeMonkey.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Scripts
{
    public class GraphBuilder : MonoBehaviour
    {
        private const float _graphWidthOffset = 0.9f;
        [SerializeField] private float _graphOffsetFromTop = 1.2f;
        [SerializeField] private float _widthOfConnection = 3f;
        [SerializeField] private int _pointSize = 5;
        [SerializeField] private Sprite _circleSprite;
        [SerializeField] private RectTransform _graphContainer;
        [SerializeField] Color _dotColor;
        [SerializeField] Color _connectionColor;
        [SerializeField] private FileReader _fileReader;

        private List<float> _points;
        private List<GameObject> _createdGraphObjects;

        private void Start()
        {
            _points = _fileReader.oilTracks;
            _createdGraphObjects = new List<GameObject>();

            CreateGraph(_points.GetRange(0, 10), 10,0);
            //CreateGraph(new List<float> { 90, 110, 80, 65, 33, 101, 89, 99, 10, 63}, 10);
        }

        public void AddPointToGraph(int indexToStart)
        {
            //ShiftGraph();

            //TODO: remove magic numbers
            DestroyGraph();
            var newRange = _points.GetRange(indexToStart - 9, 10);
            var duplicatedList = new List<float>();

            CreateGraph(newRange, 10, indexToStart);

            Debug.Log($"indexToStart before: {indexToStart} after: {indexToStart - 10}");
            Debug.Log($"newRange.Count: {newRange.Count}");

            for (int i = 0; i < newRange.Count; i++)
            {
                Debug.Log($"newRange{i} value: {newRange[i]}");
            }
        }

        private void DestroyGraph()
        {
            for (int i = 0; i < _createdGraphObjects.Count; i++)
            {
                GameObject.Destroy(_createdGraphObjects[i]);
            }
        }

       /* private void DuplicateList(List<float> toDuplicate)
        {
            for (int i = 0; i < toDuplicate.Count; i++)
            {
                _points.Add(toDuplicate[i]);
            }
        }*/


        private void ShiftGraph()
        {
            for (int i = 0; i < _createdGraphObjects.Count; i++)
            {
                GameObject.Destroy(_createdGraphObjects[i]);
            }

            _createdGraphObjects.Clear();
        }

        private void CreateGraph(List<float> valueList, int desiredPointsCount, int indexToStartFrom)
        {
            //int pointsCount = DefineMaxPointCountToCreate(valueList, desiredPointsCount);
            // float yMaximum = CalculateMaxY(valueList.GetRange(0,pointsCount));
            float yMaximum = 250;
            float graphHeight = _graphContainer.sizeDelta.y;
            float xSize = CalculateDistributionOnX(valueList.Count);

            GameObject lastCircleGameObject = null;

            Debug.Log("valueList count !!!" + valueList.Count);

            for (int i = 0; i < valueList.Count; i++)
            {

                Debug.Log("iterations!");
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


        private float CalculateMaxY(List<float> valueList)
        {
            return (float)valueList.Max() * _graphOffsetFromTop;
        }
        private float CalculateDistributionOnX(int pointsCount)
        {
            return (_graphContainer.rect.width * _graphWidthOffset) / pointsCount;
        }

        private int DefineMaxPointCountToCreate(List<float> valueList, int maxCountGraphPoints)
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

            _createdGraphObjects.Add(newCircle);

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

            _createdGraphObjects.Add(newConnection);
        }
    }

}