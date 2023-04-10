using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _exitMenu;
    [SerializeField] private GameObject _ScrollMenu;
    private Shooting _shooting;
    private Leveling _leveling;

    private void Start() 
    {
        _shooting  = GetComponent<Shooting>();
        _leveling = GetComponent<Leveling>();
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
                if (_leveling.ImageNewLvl.activeSelf)
                    _leveling.ImageNewLvl.SetActive(false);
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
        _ScrollMenu.gameObject.SetActive(!_menu.activeSelf);
    }
}
