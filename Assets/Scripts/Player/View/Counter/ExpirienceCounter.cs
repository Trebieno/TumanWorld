using UnityEngine;
using UnityEngine.UI;


public class ExpirienceCounter : MonoBehaviour
{
    [SerializeField] private Leveling _leveling;
    private Slider _expSlider;

    private void Awake()
    {
        _expSlider = GetComponent<Slider>();
        _leveling.ExpirienceChanged += Leveling_OnExpirienceChanged;
        _expSlider.value = _leveling.CurrentExpirience;
        _expSlider.maxValue = _leveling.MaxExpirience;
    }

    private void OnDestroy()
    {
        _leveling.ExpirienceChanged -= Leveling_OnExpirienceChanged;
    }

    private void Leveling_OnExpirienceChanged(int curExp, int maxExp)
    {
        _expSlider.value = curExp;
        _expSlider.maxValue = maxExp;
    }
}