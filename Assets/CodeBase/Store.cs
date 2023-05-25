using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[Serializable]
public struct ItemSell
{
    
    public GameObjects Type;
    public TMP_Text TextAmount;
    public TMP_Text TextCost;
    public Slider Sell;
    public int Cost;
}


public class Store : MonoCache
{
    [SerializeField] private Player _player;
    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private List<ItemSell> _sellList;

    [SerializeField] private TMP_Text _textCountSell;
    [SerializeField] private TMP_Text _textFinalPrice;

    [SerializeField] private GameObject _itemFlashlight;
    [SerializeField] private GameObject _itemPickaxe;
    [SerializeField] private GameObject _itemPickaxe_2;
    [SerializeField] private GameObject _itemPickaxe_3;

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

    public override void OnTick()
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
            for (int i = 0; i < _sellList.Count; i++)
            {
                GameObjects typeObject = (GameObjects)i;
                _sellList[i].Sell.maxValue = 0.1f;
                _sellList[i].Sell.value = 0;
                ChangeSlider(i);
            }
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
        for (int i = 0; i < _sellList.Count; i++)
        {
            GameObjects typeObject = (GameObjects)i;
            ChangeSlider(i);
        }
        

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
        _player.UpdateUI();
        UpdateSliderSell();
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
            _itemPickaxe_2.SetActive(true);
        }

        if(type == "pickaxe_2" && _player.Economic.Money >= 300)
        {
            TypeBuy(300, () => _player.UpdatePickaxe(2));
            _itemPickaxe_2.SetActive(false);
            _itemPickaxe_3.SetActive(true);
        }

        if(type == "pickaxe_3" && _player.Economic.Money >= 1500)
        {
            TypeBuy(1500, () => _player.UpdatePickaxe(3));
            _itemPickaxe_3.SetActive(false);
        }

        

        if (type == "mineTurret" && _player.Economic.Money >= 200) TypeBuy(200, () => _player.MineTurretCount += 1);
        if (type == "attackTurret" && _player.Economic.Money >= 300) TypeBuy(300, () => _player.AttackTurretCount += 1);
    }

    public void Sell(int id)
    {
        GameObjects typeObject = (GameObjects)id;

        //Текст итоговой цены на кнопку
        _sellList[id].TextCost.text = ((int)_sellList[id].Cost * (int)_sellList[id].Sell.value).ToString();

        switch (typeObject)
        {
            case GameObjects.Ore:
                TypeSell(_sellList[id].Cost * (int)_sellList[id].Sell.value, () => _player.Mining.RemoveOre((int)_sellList[id].Sell.value));
                break;

            case GameObjects.Light:
                TypeSell(_sellList[id].Cost * (int)_sellList[id].Sell.value, () => _player.LightCount -=((int)_sellList[id].Sell.value));
                break;
            
            case GameObjects.Ship:
                TypeSell(_sellList[id].Cost * (int)_sellList[id].Sell.value, () => _player.SpikeTrapCount -=((int)_sellList[id].Sell.value));
                break;

            case GameObjects.MiningTurret:                
                TypeSell(_sellList[id].Cost * (int)_sellList[id].Sell.value, () => _player.MineTurretCount -=((int)_sellList[id].Sell.value));
                break;

            case GameObjects.AttackTurret:
                TypeSell(_sellList[id].Cost * (int)_sellList[id].Sell.value, () => _player.AttackTurretCount -=((int)_sellList[id].Sell.value));                
                break;

            case GameObjects.Clip:
                TypeSell(_sellList[id].Cost * (int)_sellList[id].Sell.value, () => _player.Shooting.Clips -=((int)_sellList[id].Sell.value));                
                break;
        }        
    }

    public void ChangeSlider(int id)
    {
        GameObjects typeObject = (GameObjects)id;

        //Текст итоговой цены на кнопку
        _sellList[id].TextCost.text = ((int)_sellList[id].Cost * (int)_sellList[id].Sell.value).ToString();

        switch (typeObject)
        {
            case GameObjects.Ore:
                _sellList[id].TextAmount.text = $"{((int)_sellList[id].Sell.value).ToString()} / {(_player.Mining.CurrentOre).ToString()}";
                _sellList[id].Sell.maxValue = _player.Mining.CurrentOre;
                break;

            case GameObjects.Light:
                _sellList[id].TextAmount.text = $"{((int)_sellList[id].Sell.value).ToString()} / {(_player.LightCount).ToString()}";
                _sellList[id].Sell.maxValue = _player.LightCount;
                break;
            
            case GameObjects.Ship:
                _sellList[id].TextAmount.text = $"{((int)_sellList[id].Sell.value).ToString()} / {(_player.SpikeTrapCount).ToString()}";
                _sellList[id].Sell.maxValue = _player.SpikeTrapCount;
                break;

            case GameObjects.MiningTurret:
                _sellList[id].TextAmount.text = $"{((int)_sellList[id].Sell.value).ToString()} / {(_player.MineTurretCount).ToString()}";
                _sellList[id].Sell.maxValue = _player.MineTurretCount;
                break;

            case GameObjects.AttackTurret:
                _sellList[id].TextAmount.text = $"{((int)_sellList[id].Sell.value).ToString()} / {(_player.AttackTurretCount).ToString()}";
                _sellList[id].Sell.maxValue = _player.AttackTurretCount;
                break;

            case GameObjects.Clip:            
                _sellList[id].TextAmount.text = $"{((int)_sellList[id].Sell.value).ToString()} / {(_player.Shooting.Clips).ToString()}";
                _sellList[id].Sell.maxValue = _player.Shooting.Clips;
                break;

        }
    }

    
}