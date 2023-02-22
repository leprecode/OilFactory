using UnityEngine;
using Zenject;

public class TimeTicker : MonoBehaviour
{
    [SerializeField] private TimeSettings _daySettings;
    
    public delegate void OnTimeEndHandler();
    public static event OnTimeEndHandler OnDayEnd;
    public static event OnTimeEndHandler OnOilsTimerEnd;


    public void Construct(TimeSettings day)
    {
        _daySettings= day;
    }

    private void Start()
    {
        InvokeRepeating("FinishDay", _daySettings.dayDurationInSeconds, _daySettings.dayDurationInSeconds);
        InvokeRepeating("FinishOilTimer", _daySettings.timeToProduceOil, _daySettings.timeToProduceOil);
    }

    private void FinishDay()
    {
        Debug.Log("DayEnded");
        OnDayEnd?.Invoke();
    }

    private void FinishOilTimer()
    {
        Debug.Log("TimerEnded");
        OnOilsTimerEnd?.Invoke();
    }
}
