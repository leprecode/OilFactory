using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class DayInstaller : MonoInstaller
    {
        [SerializeField] private TimeSettings _daySettings;

        public override void InstallBindings()
        {
            BindDaySettings();
            BindDayTicker();
        }

        private void BindDaySettings()
        {
            Container.Bind<TimeSettings>()
                .FromInstance(_daySettings)
                .AsSingle()
                .NonLazy();
        }

        private void BindDayTicker()
        {
            TimeTicker dayTicker = CreateDayTicker();

            Container.Bind<TimeTicker>()
                .FromInstance(dayTicker)
                .AsSingle()
                .NonLazy();
        }

        private TimeTicker CreateDayTicker()
        {
            GameObject dayTickerObject = new GameObject("DayTicker");
            TimeTicker dayTicker = dayTickerObject.AddComponent<TimeTicker>();

            dayTicker.Construct(_daySettings);
            return dayTicker;
        }
    }
}
