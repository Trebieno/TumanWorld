using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject[] _menus;
    [SerializeField] private GameObject _scoreMenu;
    [SerializeField] private GameObject _storeMenu;
    [SerializeField] private GameObject _exitMenu;
    [SerializeField] private GameObject _scrollMenu;
    [SerializeField] private GameObject _deadMenu;
    [SerializeField] private GameObject _updateTurretMenu;
    // [SerializeField] private GameObject _
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {            
            if (_storeMenu.activeSelf)
            {
                _storeMenu.SetActive(false);
                _player.GetComponent<Movement>().enabled = true;
                _player.StateShooting(true);
                _player.Movement.Rb.freezeRotation = false;
            }
            else if (_exitMenu.activeSelf)
            {
                AudioListener.pause = false;
                Time.timeScale = 1;
                _player.StateShooting(true);
                _player.Movement.enabled = true;
                _player.Movement.Rb.constraints = RigidbodyConstraints2D.None;
                _scrollMenu.SetActive(true);
                _exitMenu.SetActive(false);            
            }

            else if (_scoreMenu.activeSelf)
            {
                _scoreMenu.SetActive(false);
                _scrollMenu.SetActive(true);
                _player.StateShooting(true);
                _updateTurretMenu.SetActive(false);
            }

            else if (!_storeMenu.activeSelf)
            {
                Time.timeScale = 0;
                _player.StateShooting(false);
                _scrollMenu.SetActive(false);
                _exitMenu.SetActive(true);
            }
        }

        if(_deadMenu.activeSelf)
        {
            Time.timeScale = 0;
            _scrollMenu.SetActive(false);
            _exitMenu.SetActive(false);
            _player.StateShooting(false);
            _storeMenu.SetActive(false);
            _scoreMenu.SetActive(false);
            _updateTurretMenu.SetActive(false);
        }
        

    }

    public void ExitMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void BackGame()
    {
        AudioListener.pause = false;
        Time.timeScale = 1;
        _player.StateShooting(true);
        _scrollMenu.SetActive(true);
        _exitMenu.SetActive(false);
    }

    public void ExitMenu()
    {
        _storeMenu.SetActive(!_storeMenu.activeSelf);
        if (_storeMenu.activeSelf)
        {
            _player.GetComponent<Movement>().enabled = false;
            _player.StateShooting(false);
            _player.Movement.enabled = false;
            _player.Movement.Rb.freezeRotation = true;
        }
        else
        {
            _player.Movement.enabled = true;
            _player.StateShooting(true);
            _player.Movement.Rb.freezeRotation = false;
        }
    }
    public void CloseMenus()
    {
        for (int i = 0; i < _menus.Length; i++)
        {
            _menus[i].SetActive(false);
        }
        _player.Movement.enabled = true;
        _player.Movement.Rb.constraints = RigidbodyConstraints2D.None;
    }

    public void CloseMenu(int index)
    {
        _menus[index].SetActive(false);
        _player.Movement.enabled = true;
        _player.Movement.Rb.constraints = RigidbodyConstraints2D.None; 
        Time.timeScale = 1;
    }
    public void OpenMenu(bool moveMouse, bool time, int index)
    {
        _menus[index].SetActive(true);
        if (!moveMouse)
        {
            _player.Movement.enabled = false;
            _player.Movement.Rb.constraints = RigidbodyConstraints2D.FreezeAll;

        }
        else if (!time)
            Time.timeScale = 0;
    }

}
