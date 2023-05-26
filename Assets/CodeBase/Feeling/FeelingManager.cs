using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelingManager : MonoBehaviour
{
    public static FeelingManager Instance { get; private set; }

    public Vector3 ShootPosition;

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
