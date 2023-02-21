using TMPro;
using UnityEngine;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Leveling _leveling;
    private TMP_Text _textLevel;



    private void Awake()
    {
        _textLevel = GetComponent<TMP_Text>();
        _leveling.LevelChanged += Leveling_OnLevelChanged;
        _textLevel.text =  $"Level {_leveling.Level.ToString()}";
    }

    private void OnDestroy()
    {
        _leveling.LevelChanged -= Leveling_OnLevelChanged;
    }

    private void Leveling_OnLevelChanged(int level)
    {        
        
        _textLevel.text =  $"Level {_leveling.Level.ToString()}";
    }
}