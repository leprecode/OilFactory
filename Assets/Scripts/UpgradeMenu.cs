using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public delegate void OnButtonClick();
    public static event OnButtonClick OnUpgradeOilProductionButtonClick;

    [SerializeField] private TextMeshProUGUI _currentFactoryLevel;
    [SerializeField] private TextMeshProUGUI _nextFactoryLevel;
    [SerializeField] private TextMeshProUGUI _upgradeCost;

    [SerializeField] private GameObject _notEnoughMoneyPopUp;
    [SerializeField] private GameObject _boughtLastUpgradePopUp;

    public void Upgrade()
    {
        OnUpgradeOilProductionButtonClick?.Invoke();
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        UpgradeSeller.OnSuccessfullDeal+= HandleCompletedUpgrade;
        UpgradeSeller.OnLastUpgrade += HandleLastUpgrade;
        UpgradeSeller.OnNotEnoughMoney+= HandleNotEnoughMoney;
    }

    private void Unsubscribe()
    {
        UpgradeSeller.OnSuccessfullDeal -= HandleCompletedUpgrade;
        UpgradeSeller.OnLastUpgrade -= HandleLastUpgrade;
        UpgradeSeller.OnNotEnoughMoney -= HandleNotEnoughMoney;
    }

    private void HandleCompletedUpgrade(int newCurrentLevel, int newNextLevel, int newUpgradeCost)
    {
        _currentFactoryLevel.SetText(newCurrentLevel.ToString());
        _nextFactoryLevel.SetText(newNextLevel.ToString());
        _upgradeCost.SetText(newUpgradeCost.ToString());

    }
    private void HandleLastUpgrade()
    {
        if (!_boughtLastUpgradePopUp.activeSelf)
        {
            PreparePopUp(_boughtLastUpgradePopUp);
            EnablePopUp(_boughtLastUpgradePopUp);

            StartCoroutine(DisablePopUp(_boughtLastUpgradePopUp,_boughtLastUpgradePopUp.GetComponent<Animation>().clip.length));
        }
    }
    private void HandleNotEnoughMoney()
    {
        if (!_notEnoughMoneyPopUp.activeSelf)
        {
            PreparePopUp(_notEnoughMoneyPopUp);
            EnablePopUp(_notEnoughMoneyPopUp);

            StartCoroutine(DisablePopUp(_notEnoughMoneyPopUp, _notEnoughMoneyPopUp.GetComponent<Animation>().clip.length));
        }
    }
    private void EnablePopUp(GameObject popUp)
    {
        popUp.SetActive(true);
    }
    private void PreparePopUp(GameObject popUp)
    {
        popUp.GetComponent<CanvasGroup>().alpha = 1;
    }

    public IEnumerator DisablePopUp(GameObject popUp, float delay)
    {
        yield return new WaitForSeconds(delay); 
    }
}
