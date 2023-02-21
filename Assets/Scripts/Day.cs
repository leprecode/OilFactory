using UnityEngine;

[CreateAssetMenu(fileName = "DaySettings", menuName = "ScriptableObjects/NewDaySettings", order = 1)]
public class Day : ScriptableObject
{
    [field: SerializeField] public int dayDurationInSeconds { get; private set; }
}
