using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClipCounter : MonoBehaviour
{
    [SerializeField] private Shooting _shooting;
    private TMP_Text _textClip;

    private void Awake()
    {
        _textClip = GetComponent<TMP_Text>();
        _shooting.ClipChanged += Shooting_OnClipChanged;
        _textClip.text = _shooting.Clips.ToString();
    }

    private void OnDestroy()
    {
        _shooting.ClipChanged -= Shooting_OnClipChanged;
    }

    private void Shooting_OnClipChanged(int curClip)
    {
        _textClip.text = curClip.ToString();
    }
}
