using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillsUI : MonoBehaviour
{
    public TextMeshProUGUI HealthSkill;
    public TextMeshProUGUI SpeedSkill;
    public TextMeshProUGUI DamageSkill;
    public TextMeshProUGUI MoneySkill;
    public TextMeshProUGUI EpxSkill;
    public TextMeshProUGUI SpeedFireSkill;
    public TextMeshProUGUI AmmoSkill;

    public void UpdateSkill(string type) 
    {
        if(type == "health")  HealthSkill.text = (int.Parse(HealthSkill.text) + 1).ToString();
        if(type == "speed") SpeedSkill.text = (int.Parse(SpeedSkill.text) + 1).ToString();
        if(type == "damage") DamageSkill.text = (int.Parse(DamageSkill.text) + 1).ToString();
        if(type == "money") MoneySkill.text = (int.Parse(MoneySkill.text) + 1).ToString();
        if(type == "exp") EpxSkill.text = (int.Parse(EpxSkill.text) + 1).ToString();
        if(type == "speedFire") SpeedFireSkill.text = (int.Parse(SpeedFireSkill.text) + 1).ToString();
        if(type == "ammo")  AmmoSkill.text = (int.Parse(AmmoSkill.text) + 1).ToString();
    }    
}
