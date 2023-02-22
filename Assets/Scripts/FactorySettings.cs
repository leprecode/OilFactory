using System.Timers;
using UnityEngine;

[CreateAssetMenu(fileName = "FactoryLevel:", menuName = "ScriptableObjects/NewFactory", order = 1)]
public class FactorySettings : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public int oilProductionPerTImePeriod { get; private set; }
    [SerializeField] private int _level;
    [SerializeField] private float _buildingCost;
}
