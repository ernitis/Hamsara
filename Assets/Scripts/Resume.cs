using UnityEngine;

public class Resume : MonoBehaviour
{
    public GameObject canvas;
    public AudioSource hover;
    public AudioSource click;
    public void ResumeGame(){
        Click();
        canvas.SetActive(false);
    }
    public void Hover(){
        hover.Play();
    }
    public void Click(){
        click.Play();
    }
}
