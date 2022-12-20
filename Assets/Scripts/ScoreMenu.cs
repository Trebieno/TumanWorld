using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _ScrollMenu;
    private Shooting _shooting;

    private void Start() 
    {
        _shooting  = GetComponent<Shooting>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            _menu.SetActive(!_menu.activeSelf);
            if(_menu.activeSelf)
            {
                _shooting.enabled = false;
                Switching();
            }
            else
            {
                _shooting.enabled = true;
                Switching();
            }   
        }
    }

    public void Switching()
    {
        _ScrollMenu.gameObject.SetActive(!_menu.activeSelf);
    }
}
