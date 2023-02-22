using TMPro;
using UnityEngine;

public class FactoryView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _oilCounter;

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
    }

    private void Unsubscribe()
    {
        FactoryPresenter.OnOilExtract -= UpdateView;
    }

    private void UpdateView(int totalOilValue)
    {
        _oilCounter.SetText(totalOilValue.ToString());
    }
}