using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public static SpawnPoints Instance { get; private set; }

    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    public List<SpawnPoint> SpawnPointsList => _spawnPoints;

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
