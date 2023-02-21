using UnityEngine;
using Zenject;

public class DayTicker : MonoBehaviour
{
    [SerializeField] private Day _daySettings;
    
    public delegate void OnDayEndHandler();
    public static event OnDayEndHandler OnDayEnd;


    public void Construct(Day day)
    {
        _daySettings= day;
    }

    private void Start()
    {
        InvokeRepeating("FinishDay", _daySettings.dayDurationInSeconds, _daySettings.dayDurationInSeconds);
    }

    private void FinishDay()
    {
        Debug.Log("DayEnded");
        OnDayEnd?.Invoke();
    }
}
