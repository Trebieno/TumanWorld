using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBack : MonoBehaviour
{
    [SerializeField] private GameObject _menuExit;
    [SerializeField] private GameObject _menuOpen;

    public void Back()
    {
        _menuOpen.SetActive(true);
        _menuExit.SetActive(false);
    }

}
