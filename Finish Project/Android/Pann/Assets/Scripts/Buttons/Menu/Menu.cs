using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject buttonOn, buttonOff;
    public AudioSource runSound, oceanSound, backgroundSound;

    private bool menuOpened = false;

    public void StartGame()
    {
        if (menuOpened)
        {
            runSound.Play();
            oceanSound.Play();
        }
        
        menu.SetActive(false);
        Time.timeScale = 1f;
    }   

    public void OpenMenu()
    {
        menuOpened = true;
        runSound.Pause();
        oceanSound.Pause();
        menu.SetActive(true);
        Time.timeScale = 0;
    } 

    public void SoundOn()
    {
        backgroundSound.Play();
        buttonOn.SetActive(true);
        buttonOff.SetActive(false);
    }

    public void SoundOff()
    {
        backgroundSound.Pause();
        buttonOn.SetActive(false);
        buttonOff.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }    
}
