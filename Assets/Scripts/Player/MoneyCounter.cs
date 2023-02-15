using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private Economic _economic;
    [SerializeField] private TMP_Text _textMoney;

    private void Awake()
    {
        _economic.MoneyChanged += Economic_OnMoneyChanged;
        _textMoney.text = _economic.Money.ToString();
    }

    private void OnDestroy()
    {
        _economic.MoneyChanged -= Economic_OnMoneyChanged;
    }

    private void Economic_OnMoneyChanged(int moneyValue)
    {
        _textMoney.text = _economic.Money.ToString();
    }
}