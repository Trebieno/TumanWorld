using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loots : MonoBehaviour
{
    public static Loots Instance { get; private set; }

    [SerializeField] private List<Item> _items = new List<Item>();

    public List<Item> Items => _items;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
