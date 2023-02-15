using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _menuExit;
    [SerializeField] private GameObject _menuSkills;
    [SerializeField] private GameObject _menuScroll;
    [SerializeField] private GameObject _pressMenu;

    [SerializeField] private Slider _sliderOreSell;
    [SerializeField] private TextMeshProUGUI _textCountSell;
    [SerializeField] private TextMeshProUGUI _textFinalPrice;
    [SerializeField] private int _oreCost;

    [SerializeField] private GameObject _itemFlashlight;
    [SerializeField] private GameObject _itemPickaxe;


    private bool _isTrigger;


    private void FixedUpdate()
    {
        Vector3 direction = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;
        direction.Normalize();
    }

    private void Update()
    {
        if (Input.GetButtonDown("OpenStore") && _isTrigger)
        {
            ExitMenu();
            UpdateSliderSell();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_menu.activeSelf)
            {
                _menu.SetActive(false);
                _player.GetComponent<Movement>().enabled = true;
                _player.StateShooting(true);
                _player.Movement.Rb.freezeRotation = false;
            }
            else if (_menuExit.activeSelf)
            {
                Time.timeScale = 1;
                _player.StateShooting(true);
                _menuScroll.SetActive(true);
                _menuExit.SetActive(false);
            }

            else if (_menuSkills.activeSelf)
            {
                _menuSkills.SetActive(false);
                _menuScroll.SetActive(true);
                _player.StateShooting(true);
            }

            else if (!_menu.activeSelf)
            {
                Time.timeScale = 0;
                _player.StateShooting(false);
                _menuScroll.SetActive(false);
                _menuExit.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _isTrigger = true;
            _pressMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _isTrigger = false;
            _pressMenu.SetActive(false);
        }
    }

    private void UpdateSliderSell()
    {
        _sliderOreSell.maxValue = _player.Mining.CurrentOre;
        _sliderOreSell.value = 0;
        _textFinalPrice.text = (_oreCost * (int)_sliderOreSell.value).ToString();
        _textCountSell.text = $"{((int)_sliderOreSell.value).ToString()} / {_player.Mining.CurrentOre.ToString()}";
    }

    private void ExitMenu()
    {
        _menu.SetActive(!_menu.activeSelf);
        Debug.Log((!_menu.activeSelf).ToString());
        if (_menu.activeSelf)
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

    private void TypeBuy(int cost, Action callback)
    {
        callback?.Invoke();
        _player.Economic.Money -= cost;
        _player.UpdateUI();
        _player.UpdateScrollView();
    }

    private void TypeSell(int finalPrice, Action callback)
    {
        callback?.Invoke();
        _player.Economic.Money += finalPrice;
        UpdateSliderSell();
        _player.UpdateUI();
        _player.UpdateScrollView();
    }

    public void Buy(string type)
    {
        if (type == "ship" && _player.Economic.Money >= 40) TypeBuy(40, () => _player.ShipCount += 1);
        if (type == "light" && _player.Economic.Money >= 65) TypeBuy(65, () => _player.LightCount += 1);
        if (type == "clip" && _player.Economic.Money >= 20) TypeBuy(20, () => _player.AddClip(1));
        if (type == "health" && _player.Economic.Money >= 15) TypeBuy(15, _player.Health.Healing);
        if (type == "battory" && _player.Economic.Money >= 35) TypeBuy(35, () => _player.BattoryCount += 1);
        if (type == "flashlight" && _player.Economic.Money >= 150)
        {
            TypeBuy(150, () => _player.ActivateFlashLight());
            _itemFlashlight.SetActive(false);
        }

        if (type == "pickaxe" && _player.Economic.Money >= 30)
        {
            TypeBuy(30, () => _player.ActivatePickaxe());
            _itemPickaxe.SetActive(false);
        }

        if (type == "mineTurret" && _player.Economic.Money >= 200) TypeBuy(200, () => _player.MineTurretCount += 1);
        if (type == "attackTurret" && _player.Economic.Money >= 300) TypeBuy(300, () => _player.AttackTurretCount += 1);
    }

    public void Sell(string type)
    {
        if (type == "ore")
            TypeSell(_oreCost * (int)_sliderOreSell.value, () => _player.Mining.RemoveOre((int)_sliderOreSell.value));
    }

    public void ChangeSlider()
    {
        _textFinalPrice.text = ((int)_oreCost * (int)_sliderOreSell.value).ToString();
        _textCountSell.text = $"{((int)_sliderOreSell.value).ToString()} / {(_player.Mining.CurrentOre).ToString()}";
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
        _menuScroll.SetActive(true);
        _menuExit.SetActive(false);
    }
}