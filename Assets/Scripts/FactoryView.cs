using TMPro;
using UnityEngine;

public class FactoryView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _oilCounter;

    public void InitialValueint(int totalOilValue)
    {
        UpdateView(totalOilValue);
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
        FactoryPresenter.OnOilExtract += UpdateView;
        FactoryPresenter.OnOilSpend += UpdateView;
    }

    private void Unsubscribe()
    {
        FactoryPresenter.OnOilExtract -= UpdateView;
        FactoryPresenter.OnOilSpend -= UpdateView;
    }

    private void UpdateView(int totalOilValue)
    {
        _oilCounter.SetText(totalOilValue.ToString());
    }
}