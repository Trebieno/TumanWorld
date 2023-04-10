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
        var slot = slots.Find(x => x == item);
        
        if(slot != null)
            slot.Amount += amount;

        else
        {
            var emptySlot = slots.Find(x => x.IsEmpty == true);
            emptySlot.Item = item;
            emptySlot.Amount = amount;
            emptySlot.IsEmpty = false;
        }


    }
}
