using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCash : MonoBehaviour
{
    public static PlayerCash Instance { get; private set; }

    public Player Player;

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
