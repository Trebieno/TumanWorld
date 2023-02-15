using TMPro;
using UnityEngine;


public class OreCounter : MonoBehaviour
{
    [SerializeField] private Mining _mining;
    [SerializeField] private TMP_Text _textOre;

    private void Awake()
    {
        _mining.OreChanged += Mining_OnOreChanged;
        _textOre.text = _mining.CurrentOre.ToString();
    }

    private void OnDestroy()
    {
        _mining.OreChanged -= Mining_OnOreChanged;
    }

    private void Mining_OnOreChanged(int oreValue)
    {
        _textOre.text = oreValue.ToString();
    }
    
}