using System;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    public event Action Enabled;
    public event Action Disable;

    private void OnEnable()
    {
        Enabled.Invoke();
    }

    private void OnDisable()
    {
        Disable.Invoke();
    }
}
