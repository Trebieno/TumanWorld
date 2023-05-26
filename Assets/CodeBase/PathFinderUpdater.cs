using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PathFinderUpdater : MonoBehaviour
{
    public static PathFinderUpdater Instance { get; private set; }


    [SerializeField] private AstarPath _pathfinder;

    
    public void Scan()
    {
        _pathfinder.Scan();
    }

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
