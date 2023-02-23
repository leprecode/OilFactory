using Assets.Scripts.Bank;
using UnityEngine;
using Zenject;

public class UpgradeSeller
{
    public delegate void OnCompleteDeal(int newCurrentLevel, int newNextLevel, int newUpgradeCost);
    public static event OnCompleteDeal OnSuccessfullDeal;

    public delegate void OnFinishUpgrade();
    public static event OnFinishUpgrade OnLastUpgrade;

    public delegate void OnUnsuccessful();
    public static event OnUnsuccessful OnNotEnoughMoney;

    private BankPresenter _bankPresenter;
    private FactoryBuilder _factoryBuilder;

    [Inject]
    public UpgradeSeller(BankPresenter bankPresenter, 
        FactoryBuilder factoryBuilder)
    {
        _bankPresenter = bankPresenter;
        _factoryBuilder = factoryBuilder;

        Subscribe();
    }

    ~UpgradeSeller()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        UpgradeMenu.OnUpgradeOilProductionButtonClick += UpgradeOilProduction;
    }

    private void Unsubscribe()
    {
        UpgradeMenu.OnUpgradeOilProductionButtonClick -= UpgradeOilProduction;
    }

    private void UpgradeOilProduction()
    {
        if (!_factoryBuilder.CheckNextFactoryLevel())
        {
            OnLastUpgrade?.Invoke();
            Debug.Log("All upgrades are bought");
            return;
        }

        if (_bankPresenter.TryBuy(_factoryBuilder.GetUpgradeCost()))
        {
            _factoryBuilder.InstallNextFactoryLevel();

            if (!_factoryBuilder.CheckNextFactoryLevel())
            {
                OnLastUpgrade?.Invoke();
                Debug.Log("BoughtLAstUpgrade");
            }
            else
            {
                OnSuccessfullDeal?.Invoke(
                    _factoryBuilder.GetCurrentLevel(),
                    _factoryBuilder.GetNextLevel(),
                    _factoryBuilder.GetUpgradeCost()
                    );
                Debug.Log("Bought another Upgrade");
            }
        }
        else
        {
            OnNotEnoughMoney?.Invoke();
            Debug.Log("Not enough money to buy new Upgrade");
        }

    }
}
