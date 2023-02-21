using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
    public class DayInstaller : MonoInstaller
    {
        [SerializeField] private Day _daySettings;

        public override void InstallBindings()
        {
            BindDaySettings();
            BindDayTicker();
        }

        private void BindDaySettings()
        {
            Container.Bind<Day>()
                .FromInstance(_daySettings)
                .AsSingle()
                .NonLazy();
        }

        private void BindDayTicker()
        {
            DayTicker dayTicker = CreateDayTicker();

            Container.Bind<DayTicker>()
                .FromInstance(dayTicker)
                .AsSingle()
                .NonLazy();
        }

        private DayTicker CreateDayTicker()
        {
            GameObject dayTickerObject = new GameObject("DayTicker");
            DayTicker dayTicker = dayTickerObject.AddComponent<DayTicker>();

            dayTicker.Construct(_daySettings);
            return dayTicker;
        }
    }
}
