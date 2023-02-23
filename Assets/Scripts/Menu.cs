using UnityEngine;

namespace Assets.Scripts
{
    public class Menu: MonoBehaviour
    {
        [SerializeField] private GameObject _upgradeMenu;
        [SerializeField] private GameObject _tradeMenu;

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
            TapHandler.OnFactoryTapped += DisplayUpgradeMenu;
        }

        private void Unsubscribe()
        { 
            TapHandler.OnFactoryTapped -= DisplayUpgradeMenu;
        }

        private void DisplayUpgradeMenu()
        {
            if (_upgradeMenu.activeSelf == false && _tradeMenu.activeSelf == false) 
                _upgradeMenu.SetActive(true);
            
        }
    }
}
