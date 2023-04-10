using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public static Objects Instance { get; private set; }

    [SerializeField] private List<ObjectGame> _objectsGame = new List<ObjectGame>();

    public List<ObjectGame> ObjectsGame => _objectsGame;

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
