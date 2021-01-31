using UnityEngine;

public sealed class UI_GameTime : MonoBehaviour
{
	private const double MONOSPACE_WIDTH = 18.0;
	[SerializeField]
	private TMPro.TMP_Text gameTime;

	private void Update() => gameTime.text = FormatGameTime(GameManager.GameTime);

	private static string FormatGameTime(float time)
	{
		var minutes = ((int)(time / 60));
		var seconds = ((int)(time % 60)).ToString("D2");
		var centiseconds = ((int)(time % 1 * 100)).ToString("D2");
		return $"<mspace=${MONOSPACE_WIDTH}>{minutes}:{seconds};{centiseconds}</mspace>";
	}
}
