using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject intro1, intro2, continues,start;
    public AudioSource hover;
    public AudioSource click;
    public void Continue(){
        Click();
        intro1.SetActive(false);
        intro2.SetActive(true);
        continues.SetActive(false);
        start.SetActive(true);
    }

    public void StartButton(){
        Click();
        intro2.SetActive(false);
        start.SetActive(false);
    }
    public void Hover(){
        hover.Play();
    }
    public void Click(){
        click.Play();
    }
}
