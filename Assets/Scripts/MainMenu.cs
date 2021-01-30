using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField]
    private string sceneName;
    
    public void playMenuButtonPress() {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void PlayGame() {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() {
        Debug.Log("Quit");
        //Application.QuitGame();
    }
}