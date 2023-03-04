using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OreView : MonoBehaviour
{
    [SerializeField] private Ore _ore;
    private TMP_Text _textOre;

    private void Awake()
    {
        _textOre = GetComponent<TMP_Text>();
        _ore.OreChanged += Ore_OnOreChanged;
        _textOre.text =  _ore.CurrentOre.ToString();
    }

    private void OnDestroy()
    {
        _ore.OreChanged -= Ore_OnOreChanged;
    }

    private void Ore_OnOreChanged(int ore)
    {
        _textOre.text =  _ore.CurrentOre.ToString();
    }
}