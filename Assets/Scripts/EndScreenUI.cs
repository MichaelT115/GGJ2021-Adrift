using UnityEngine;

public sealed class EndScreenUI : MonoBehaviour
{
	[SerializeField]
	private TMPro.TMP_Text previousTimeText;
	[SerializeField]
	private TMPro.TMP_Text bestTimeText;

	private void Start()
	{
		previousTimeText.text = FormatGameTime(ScoreManager.PreviousTime);
		bestTimeText.text = FormatGameTime(ScoreManager.PreviousTime);
	}

	private static string FormatGameTime(float time)
	{
		var minutes = ((int)(time / 60));
		var seconds = ((int)(time % 60)).ToString("D2");
		var centiseconds = ((int)(time % 1 * 100)).ToString("D2");
		return $"{minutes}:{seconds}:{centiseconds}";
	}
}
