using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private UpgradablePerks _upgradablePerks;
    [SerializeField] private TMP_Text _textMainScreenScore;
    private TMP_Text _textScore;

    private void Awake()
    {
        _textScore = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _upgradablePerks.ScoreChanged += UpgradablePerks_OnScoreChanged;
        _textMainScreenScore.gameObject.SetActive(false);
        _textScore.text = $"Очки навыков: {_upgradablePerks.Score.ToString()}";
    }

    private void OnDisable()
    {
        _upgradablePerks.ScoreChanged -= UpgradablePerks_OnScoreChanged;
        if(_upgradablePerks.Score > 0)
            _textMainScreenScore.gameObject.SetActive(true);
        _textMainScreenScore.text = $"Очки навыков: {_upgradablePerks.Score.ToString()}";
    }

    private void UpgradablePerks_OnScoreChanged(int obj)
    {
        _textScore.text = $"Очки навыков: {obj.ToString()}";
        _textMainScreenScore.text = $"Очки навыков: {_upgradablePerks.Score.ToString()}";        
    }
}