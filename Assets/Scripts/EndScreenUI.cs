using UnityEngine;

public sealed class EndScreenUI : MonoBehaviour
{
	[SerializeField]
	private TMPro.TMP_Text text;

	private void Start() => text.text = $"Game Over" +
			$"\nScore: {ScoreManager.PreviousTime}" +
			$"\nBest Time: {ScoreManager.BestTime}";
}
