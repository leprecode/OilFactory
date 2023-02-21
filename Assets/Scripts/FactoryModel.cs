using System.Timers;
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryLevel:", menuName = "ScriptableObjects/NewFactory", order = 1)]
public class FactoryModel : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; }
    [SerializeField] private int _level;
    [SerializeField] private float _oilProductionPerDay;
    [SerializeField] private float _buildingCost;
}
