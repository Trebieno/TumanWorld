using TMPro;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private Economic _economic;
    private TMP_Text _textMoney;

    private void Awake()
    {
        _textMoney = GetComponent<TMP_Text>();
        _economic.MoneyChanged += Economic_OnMoneyChanged;
        _textMoney.text = _economic.Money.ToString();
    }

    private void OnDestroy()
    {
        _economic.MoneyChanged -= Economic_OnMoneyChanged;
    }

    private void Economic_OnMoneyChanged(float moneyValue)
    {
        _textMoney.text = _economic.Money.ToString();
    }
}