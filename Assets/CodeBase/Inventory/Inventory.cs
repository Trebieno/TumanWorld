using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject UIPanel;
    public Transform InventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool IsOpened;

    private void Start()
    {
        for (int i = 0; i < InventoryPanel.childCount; i++)
        {
            if(InventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
                slots.Add(InventoryPanel.GetChild(i).GetComponent<InventorySlot>());
        }
        UIPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            IsOpened = !IsOpened;
            if(IsOpened)
                UIPanel.SetActive(true);
            
            else
                UIPanel.SetActive(false);
        }
    }

    public void AddItem(ItemScriptableObject item, int amount)
    {
        // var slot = slots.Find(x => x.Item == item && x.Amount < item.MaximumAmount);
        
        // if(slot != null)
        // {
        //     slot.Amount += amount;
        //     slot.SetText(slot.Amount);
        // }

        // else
        // {
        //     var emptySlot = slots.Find(x => x.IsEmpty == true);
        //     if(emptySlot != null)
        //     {
        //         emptySlot.Item = item;
        //         emptySlot.Amount = amount;
        //         emptySlot.IsEmpty = false;
        //         emptySlot.SetIcon(item.Icon);
        //         emptySlot.SetText(amount);
        //     }
        // }

        switch (item.ItemType)
        {
            case ItemType.Clip:
                PlayerCash.Instance.Player.AddClip(amount);
                break;
            
            case ItemType.Ore:
                PlayerCash.Instance.Player.Mining.AddOre(amount);
                break;

            case ItemType.AttackTurret:
                PlayerCash.Instance.Player.AttackTurretCount += amount;
                break;

            case ItemType.MiningTurret:
                PlayerCash.Instance.Player.MineTurretCount += amount;
                break;
            
            case ItemType.SpikeTrap:
                PlayerCash.Instance.Player.SpikeTrapCount += amount;
                break;
            
            case ItemType.MineTrap:
                PlayerCash.Instance.Player.MineTrapCount += amount;
                break;            
        }

        PlayerCash.Instance.Player.UpdateScrollView();
    }
}
