using System.Text;
using UnityEngine;

public sealed class UI : MonoBehaviour
{
	[SerializeField]
	private TMPro.TMP_Text text;

	public void DisplayInstructions(Instructions instructions) => text.text = FormatStorageState(instructions, Storage.EMPTY);

	public void DisplayStorageState(Instructions instructions, Storage storage) => text.text = FormatStorageState(instructions, storage);

	private static string FormatStorageState(in Instructions instructions, in Storage storage)
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"Build a: {instructions.ShipPartName}");
		for (int i = 0; i < instructions.Entries.Length; ++i)
		{
			var instruction = instructions.Entries[i];
			var storageEntry = storage.GetStorageEntry(instruction.MaterialType);
			stringBuilder.AppendLine($"{i + 1}. {instruction.MaterialType.DisplayName}: {storageEntry.Count}/{instruction.Count}");
		}

		string text = stringBuilder.ToString();
		return text;
	}
}
