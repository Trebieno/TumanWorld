using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreAll : MonoBehaviour
{
    public static OreAll Instance { get; private set; }

    private List<Ore> _ores = new List<Ore>();
    
    public List<Ore> Ores => _ores;

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
