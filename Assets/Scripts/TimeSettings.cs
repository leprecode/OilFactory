using UnityEngine;

[CreateAssetMenu(fileName = "DaySettings", menuName = "ScriptableObjects/NewDaySettings", order = 1)]
public class TimeSettings : ScriptableObject
{
    [field: SerializeField] public int dayDurationInSeconds { get; private set; }
    [field: SerializeField] public int timeToProduceOil { get; private set; }
}
