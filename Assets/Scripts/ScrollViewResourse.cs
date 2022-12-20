using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollViewResourse : MonoBehaviour
{
    public List<GameObject> Inventory = new List<GameObject>();
    public TextMeshProUGUI TextInventory;
    public int Index = 0;
    public int PreviesIndex = 0;

    [SerializeField] private Player _player;
    private float _mouseWheel;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        _mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        
        if(_mouseWheel > 0)
        {
            if(Inventory.Count < Index + 1)
                Index = 0;
            else
                Index += 1;         
        }

        if(_mouseWheel < 0)
        {            
            if(Index - 1 < 0)
                Index = Inventory.Count - 1;
            else
                Index -= 1;                        
        }

        if(Index != PreviesIndex)
        {
            UpdateUI();
            PreviesIndex = Index;
        }
            
    }

    //Шипы, Свет, Боевая турель, копатель.

    public void UpdateUI()
    {
        if(Index == Inventory.Count)
            Index = 0;
        if(Index < 0)
            Index = Inventory.Count;

        Inventory[PreviesIndex].gameObject.SetActive(false);
        Inventory[Index].gameObject.SetActive(true);
        _player.UpdateScrollView();
    }
}
