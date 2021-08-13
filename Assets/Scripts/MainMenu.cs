using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     AudioSource audio;
    public AudioClip Hover;
    public AudioClip click;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        
    }
    public void hoversound()
    {
        audio.PlayOneShot(Hover);
    }
    public void clicksound()
    {
        audio.PlayOneShot(click);
    }
    public void StartGame()
    {
        //Debug.Log("Game is running");
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        Debug.Log("Opening options menu");
    }

    public void Tutorial()
    {
        Debug.Log("Opening tutorial menu");
    }

    public void QuitGame()
    {

        //Debug.Log("Qutting");
        Application.Quit();
    }
}
