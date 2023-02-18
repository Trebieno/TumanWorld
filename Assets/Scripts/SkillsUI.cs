using UnityEngine;
using TMPro;

public class SkillsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthSkill;
    [SerializeField] private TMP_Text _speedSkill;
    [SerializeField] private TMP_Text _damageSkill;
    [SerializeField] private TMP_Text _moneySkill;
    [SerializeField] private TMP_Text _expSkill;
    [SerializeField] private TMP_Text _speedFireSkill;
    [SerializeField] private TMP_Text _ammoSkill;

    public void UpdateSkill(Perks type)
    {
        if (type == Perks.Health) _healthSkill.text = (int.Parse(_healthSkill.text) + 1).ToString();
        if (type == Perks.Speed) _speedSkill.text = (int.Parse(_speedSkill.text) + 1).ToString();
        if (type == Perks.Damage) _damageSkill.text = (int.Parse(_damageSkill.text) + 1).ToString();
        if (type == Perks.Money) _moneySkill.text = (int.Parse(_moneySkill.text) + 1).ToString();
        if (type == Perks.Expirience) _expSkill.text = (int.Parse(_expSkill.text) + 1).ToString();
        if (type == Perks.FireSpeed) _speedFireSkill.text = (int.Parse(_speedFireSkill.text) + 1).ToString();
        if (type == Perks.Ammo) _ammoSkill.text = (int.Parse(_ammoSkill.text) + 1).ToString();
    }
}