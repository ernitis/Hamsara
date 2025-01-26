using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public AudioSource hover;
    public AudioSource click;
    public void Reincarnate(){
        Click();
        SceneManager.LoadScene("Main Menu");
    }

    public void Menu(){
        Click();
        SceneManager.LoadScene("Main Menu");
    }

    public void Game(){
        Click();
        SceneManager.LoadScene("Game");
    }
    public void Credits(){
        Click();
        SceneManager.LoadScene("Credits");
    }

    public void Exit(){
        Click();
        Application.Quit();
    }

    public void Hover(){
        hover.Play();
    }
    public void Click(){
        click.Play();
    }
}
