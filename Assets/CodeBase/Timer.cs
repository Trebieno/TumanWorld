using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private float _timeSecond;

    private void FixedUpdate()
    {
        _timeSecond += Time.fixedDeltaTime;
        TimeSpan t = TimeSpan.FromSeconds( _timeSecond );
        _timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                t.Hours,
                t.Minutes,
                t.Seconds);
    }
}
