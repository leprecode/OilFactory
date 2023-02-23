using TMPro;
using UnityEngine;

namespace Assets.Scripts.Bank
{
    public class BankView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _money;

        public void InitialMoneyCounter(int StartValue)
        {
            UpdateView(StartValue);
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            BankPresenter.OnAddMoney += UpdateView;
            BankPresenter.OnSpendMoney += UpdateView;
        }

        private void OnDisable()
        {
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            BankPresenter.OnAddMoney -= UpdateView;
            BankPresenter.OnSpendMoney -= UpdateView;
        }


        private void UpdateView(int newMoneyCount)
        {
            _money.SetText(newMoneyCount.ToString());
        }
    }
}
