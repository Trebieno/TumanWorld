using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuView : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private GameObject _scoreMenu;
    [SerializeField] private GameObject _storeMenu;
    [SerializeField] private GameObject _exitMenu;
    [SerializeField] private GameObject _scrollMenu;
    
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
                Time.timeScale = 1;
                _player.StateShooting(true);
                _scrollMenu.SetActive(true);
                _exitMenu.SetActive(false);
            }

            else if (_scoreMenu.activeSelf)
            {
                _scoreMenu.SetActive(false);
                _scrollMenu.SetActive(true);
                _player.StateShooting(true);
            }

            else if (!_storeMenu.activeSelf)
            {
                Time.timeScale = 0;
                _player.StateShooting(false);
                _scrollMenu.SetActive(false);
                _exitMenu.SetActive(true);
            }
        }
    }

    public void ExitMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void BackGame()
    {
        Time.timeScale = 1;
        _player.StateShooting(true);
        _scrollMenu.SetActive(true);
        _exitMenu.SetActive(false);
    }

    public void ExitMenu()
    {
        _storeMenu.SetActive(!_storeMenu.activeSelf);
        Debug.Log((!_storeMenu.activeSelf).ToString());
        if (_storeMenu.activeSelf)
        {
            _player.GetComponent<Movement>().enabled = false;
            _player.StateShooting(false);
            _player.Movement.Rb.freezeRotation = true;
        }
        else
        {
            _player.GetComponent<Movement>().enabled = true;
            _player.StateShooting(true);
            _player.Movement.Rb.freezeRotation = false;
        }
    }

}
