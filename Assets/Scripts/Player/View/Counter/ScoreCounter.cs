using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private UpgradablePerks _upgradablePerks;
    private TMP_Text _textScore;
    private TMP_Text _TextMainScreenScore;

    private void Awake()
    {
        _textScore = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _upgradablePerks.ScoreChanged += UpgradablePerks_OnScoreChanged;
        _textScore.text = $"Очки навыков: {_upgradablePerks.Score.ToString()}";
    }

    private void OnDisable()
    {
        _upgradablePerks.ScoreChanged -= UpgradablePerks_OnScoreChanged;
    }

    private void UpgradablePerks_OnScoreChanged(int obj)
    {
        _textScore.text = $"Очки навыков: {obj.ToString()}";
    }
}