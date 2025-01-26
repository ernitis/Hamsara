using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void Menu(){
        SceneManager.LoadScene("Main Menu");
    }

    public void Game(){
        SceneManager.LoadScene("Game");
    }
    public void Credits(){
        SceneManager.LoadScene("Credits");
    }

    public void Exit(){
        Application.Quit();
    }
}
