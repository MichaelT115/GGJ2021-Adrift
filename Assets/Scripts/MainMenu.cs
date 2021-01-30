using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip;
    
    public void playMenuButtonPress() {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        //Application.QuitGame();
    }
}