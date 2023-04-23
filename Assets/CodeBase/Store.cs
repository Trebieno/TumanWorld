using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private Slider _sliderOreSell;
    [SerializeField] private TMP_Text _textCountSell;
    [SerializeField] private TMP_Text _textFinalPrice;
    [SerializeField] private int _oreCost;

    [SerializeField] private GameObject _itemFlashlight;
    [SerializeField] private GameObject _itemPickaxe;

    [SerializeField] private GameObject _pressMenu;

    [SerializeField] private MenuView _menuView;


    private bool _isTrigger;

    public bool IsTrigger 
    {
        get => _isTrigger;
        set{_isTrigger = value; 
            _pressMenu.SetActive(value);}
    }


    private void FixedUpdate()
    {
        Vector3 direction = _player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rb.rotation = angle;
        direction.Normalize();
    }

    private void Update()
    {
        if (Input.GetButtonDown("OpenStore") && IsTrigger)
        {
            _menuView.ExitMenu();
            UpdateSliderSell();
        }   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            IsTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            IsTrigger = false;
        }
    }

    private void UpdateSliderSell()
    {
        _sliderOreSell.maxValue = _player.Mining.CurrentOre;
        _sliderOreSell.value = 0;
        _textFinalPrice.text = (_oreCost * (int)_sliderOreSell.value).ToString();
        _textCountSell.text = $"{((int)_sliderOreSell.value).ToString()} / {_player.Mining.CurrentOre.ToString()}";
    }

    private void TypeBuy(int cost, Action callback)
    {
        callback?.Invoke();
        _player.Economic.Money -= cost;
        _player.UpdateUI();
    }

    private void TypeSell(int finalPrice, Action callback)
    {
        callback?.Invoke();
        _player.Economic.Money += finalPrice;
        UpdateSliderSell();
        _player.UpdateUI();
    }

    public void Buy(string type)
    {
        if (type == "ship" && _player.Economic.Money >= 40) TypeBuy(40, () => _player.SpikeTrapCount += 1);
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
        _sliderOreSell.maxValue = _player.Mining.CurrentOre;
    }

    
}