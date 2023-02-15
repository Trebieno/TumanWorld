using UnityEngine;
using TMPro;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _exitMenu;
    [SerializeField] private GameObject _ScrollMenu;
    [SerializeField] private TextMeshProUGUI _textScoreMainMenu;
    public TextMeshProUGUI TextScoreMainMenu => _textScoreMainMenu;
    private Shooting _shooting;
    private Player _player;

    private void Start() 
    {
        _shooting  = GetComponent<Shooting>();
        _player = GetComponent<Player>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !_exitMenu.activeSelf) 
        {
            _menu.SetActive(!_menu.activeSelf);
            if(_menu.activeSelf)
            {
                _shooting.enabled = false;
                Switching();
            }
            else
            {
                _shooting.enabled = true;
                Switching();
            }   
        }
    }

    public void Switching()
    {
        _ScrollMenu.SetActive(!_menu.activeSelf);
        _textScoreMainMenu.gameObject.SetActive(!_menu.activeSelf);

        if(_player.Score <= 0)
            _textScoreMainMenu.gameObject.SetActive(false);
    }
}
