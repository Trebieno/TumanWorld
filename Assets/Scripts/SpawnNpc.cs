using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNpc : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private GameObject _npcPrefub;
    [SerializeField] private float _delay;
    [SerializeField] private float _curTime;
    private int _countNpc;
    private bool _isStart;

    public IEnumerator StartSpawn(int count)
    {
        _isStart = true;
        for (int i = 0; i < count; )
        {
            foreach (var item in _points)
            {
                if(i<count)
                {
                    yield return new WaitForSeconds(_delay);
                    Instantiate(_npcPrefub, item.position, item.rotation);
                    i++;
                }                
            }
        }
        

        _curTime = Random.Range(1, _player.GetLevel()*10);
        _isStart = false;
    }

    private void Update() 
    {
        _curTime -= Time.deltaTime;
        if(_curTime <= 0 && !_isStart)
        {
            StartCoroutine(StartSpawn(Random.Range(0, _player.GetLevel()*7)));
        }
    }
}
