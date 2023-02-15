using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

   public void SceneGame () 
   {
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

