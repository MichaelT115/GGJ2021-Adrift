using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class PauseMenuFunctions : MonoBehaviour
{
	private const string MENU_SCENE_NAME = "Menu";
	private static string ActiveSceneName => SceneManager.GetActiveScene().name;

	public void ResumeGame() => GameManager.IsPaused = false;
	public void ResetGame() => SceneManager.LoadScene(ActiveSceneName, LoadSceneMode.Single);
	public void QuitToMainMenu() => SceneManager.LoadScene(MENU_SCENE_NAME, LoadSceneMode.Single);
	public void QuitGame() => Application.Quit();
}
