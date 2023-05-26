using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOreSystem : MonoBehaviour
{
    public static SpawnOreSystem Instance { get; private set; }

    [SerializeField] private GameObject[] _spawnObjects;
    [SerializeField] private float _spawnRadius = 2.0f;
    [SerializeField] private LayerMask _mask;

    private void Start()
    {
        Spawn(PlayerCash.Instance.Player.transform.position);
    }

    public void Spawn(Vector2 pos)
    {
        int maxTries = 10;
        bool foundValidPosition = false;

        while (!foundValidPosition && maxTries > 0)
        {

            Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, _spawnRadius, _mask);
            Debug.Log(colliders.Length);
            if (colliders.Length <= 0)
            {
                foundValidPosition = true;
            }

            maxTries--;
        }

        if (foundValidPosition)
        {
            Vector2 randomPoint = pos + new Vector2(Random.value-0.5f,Random.value-0.5f).normalized * _spawnRadius;

            int randomIndex = Random.Range(0, _spawnObjects.Length-1);
            Instantiate(_spawnObjects[randomIndex], randomPoint, Quaternion.identity);
            Debug.Log("Создал руду");
        }
        else
        {

        }
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
