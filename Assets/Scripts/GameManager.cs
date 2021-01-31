using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{

	[Serializable]
	private class PauseEvent : UnityEvent<bool> { }
	[SerializeField]
	private PauseEvent pauseEvent;

	private static bool isPaused;


	public void Awake() => Instance = this;
	public void Start()
	{
		GameTime = 0;
		IsPaused = false;
	}
	public void Update() => GameTime += Time.deltaTime;

	public void EndGame()
	{
		ScoreManager.SetTime(GameTime);
		SceneManager.LoadScene("End Screen", LoadSceneMode.Single);
	}

	public static float GameTime { get; private set; }
	public static bool IsPaused
	{
		get => isPaused; 
		set
		{
			isPaused = value;
			Time.timeScale = IsPaused ? 0 : 1;
			Instance.pauseEvent.Invoke(IsPaused);
		}
	}

	public static GameManager Instance { get; private set; }

	public static void TogglePause()
	{
		IsPaused = !IsPaused;
	}

}
