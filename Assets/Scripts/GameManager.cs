using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
	private static float startTime;

	public void Awake() => startTime = Time.time;

	public void EndGame()
	{
		ScoreManager.SetTime(GameTime);
		SceneManager.LoadScene("End Screen", LoadSceneMode.Single);
	}

	public static float GameTime => Time.time - startTime;
}
