using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemScriptableObject ItemScriptableObject;
    public int Amount;

    private void Start()
    {
        Loots.Instance.Items.Add(this);
    }
}
