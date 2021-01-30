using System.Text;
using UnityEngine;

public sealed class UI : MonoBehaviour
{
	[SerializeField]
	private TMPro.TMP_Text text;

	public void DisplayInstructions(Instructions instructions)
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("You need to find these materials:");
		for (int i = 0; i < instructions.Entries.Length; ++i)
		{
			var instruction = instructions.Entries[i];
			stringBuilder.AppendLine($"{i + 1}. {instruction.MaterialType.DisplayName} x{instruction.Count}");
		}

		text.text = stringBuilder.ToString();
	}
}
