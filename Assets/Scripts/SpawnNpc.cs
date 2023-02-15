using System.Collections.Generic;
using UnityEngine;

public class SpawnNpc : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Character  _npcPrefub;

    [SerializeField] private float _maxdelay;
    [SerializeField] private float _curDelay;

    [SerializeField] private float _curTime;
    private int _countNpc;
    private bool _isStart;

    public void StartSpawn(int count)
    {
        _isStart = true;
        _curTime = Random.Range(1, _player.GetLevel()*7); // Изменил с 10 на 7
        _countNpc = count;
    }

    private void Update() 
    {

        if(_curDelay <= 0 && _countNpc > 0)
        {
            Instantiate(_npcPrefub, _points[Random.Range(0, _points.Count)].position, Quaternion.identity);
            _curDelay = _maxdelay;
            _countNpc--;

            if(_countNpc - 1 <= 0)
                _isStart = false;
        }

        if(_curDelay > 0)
            _curDelay -= Time.deltaTime;

        if(_curTime > 0 && !_isStart)
            _curTime -= Time.deltaTime;

        if(_curTime <= 0 && !_isStart)
            StartSpawn(Random.Range(4, _player.GetLevel()*5));
    }
}
