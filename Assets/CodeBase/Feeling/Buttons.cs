using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public static Buttons Instance { get; private set; }

    [Range(0, 1)] [SerializeField] private float _volume;
    [Range(0, 1)] [SerializeField] private float _pitch;

    public float Volume => _volume;
    public float Pitch => _pitch;

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
