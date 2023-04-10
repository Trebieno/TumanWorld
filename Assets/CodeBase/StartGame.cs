using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
   [SerializeField] private Animator _anim;

    public void StartAnim() => _anim.gameObject.SetActive(true);
    public void SceneGame () => SceneManager.LoadScene("Game");
  

    public void ExitGame()
    {
        Application.Quit();
    }


    public void InfoGame(GameObject infoPanel)
    {
      infoPanel.SetActive(!infoPanel.activeSelf);
    }
}

