using System.Collections.Generic;
using UnityEngine;

public class TurretsAll : MonoBehaviour
{
    public static TurretsAll Instance { get; private set; }

    private List<Turret> _turrets = new List<Turret>();
    
    public List<Turret> Turrets => _turrets;


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
