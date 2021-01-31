using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
	public void Awake() => GameTime = 0;
	public void Update() => GameTime += Time.deltaTime;

	public void EndGame()
	{
		ScoreManager.SetTime(GameTime);
		SceneManager.LoadScene("End Screen", LoadSceneMode.Single);
	}

	public static float GameTime { get; private set; }

	public static bool Pause { get; private set; }

	public static void TogglePause()
	{
		Pause = !Pause;
		Time.timeScale = Pause ? 0 : 1;
	}
}
