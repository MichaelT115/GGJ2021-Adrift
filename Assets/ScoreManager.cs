using UnityEngine;

public static class ScoreManager {
	private const string KEY_GAME_TIME = "gameTime";
	private const string KEY_NEW_BEST_TIME = "newBestTime";
	private const string KEY_BEST_TIME = "bestTime";
	private const int VALUE_IS_NEW_BEST_TIME = 1;
	private const int VALUE_IS_NOT_NEW_BEST_TIME = 0;

	public static void SetTime(float gameTime)
	{
		PlayerPrefs.SetFloat(KEY_GAME_TIME, gameTime);

		bool newBestTime = BestTime > gameTime;

		PlayerPrefs.SetInt(KEY_NEW_BEST_TIME, newBestTime ? VALUE_IS_NEW_BEST_TIME : VALUE_IS_NOT_NEW_BEST_TIME);

		if (newBestTime)
		{
			PlayerPrefs.SetFloat(KEY_BEST_TIME, gameTime);
		}
		PlayerPrefs.Save();
	}

	public static float PreviousTime => PlayerPrefs.GetFloat(KEY_GAME_TIME, VALUE_IS_NOT_NEW_BEST_TIME);
	public static float BestTime => PlayerPrefs.GetFloat(KEY_BEST_TIME, float.PositiveInfinity);
	public static bool IsNewBestTime => PlayerPrefs.GetInt(KEY_NEW_BEST_TIME, VALUE_IS_NOT_NEW_BEST_TIME) == VALUE_IS_NEW_BEST_TIME;

}
