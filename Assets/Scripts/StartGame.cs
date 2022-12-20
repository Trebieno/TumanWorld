using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
   // [SerializeField] private AudioSource _audio;

   // private bool _isPlaying;
   // private void Start() 
   // {
        
   // }

   // private void Update() 
   // {
   //    if(!_audio.isPlaying && _isPlaying)
   //    {
   //       _isPlaying = false;
   //       SceneManager.LoadScene("Game");         
   //    }
   // }
   public void SceneGame () 
   {
      // _audio.Play();
      // _isPlaying = true;
      SceneManager.LoadScene("Game");         
   }
   public void ExitGame()
   {
      Application.Quit();
   }

   public void InfoGame(GameObject infoPanel)
   {
      infoPanel.SetActive(!infoPanel.activeSelf);
   }
}

