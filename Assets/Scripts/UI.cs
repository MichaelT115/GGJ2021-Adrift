using System.Text;
using UnityEngine;

public sealed class UI : MonoBehaviour
{
	[SerializeField]
	private TMPro.TMP_Text header;
	[SerializeField]
	private RectTransform list;
	[SerializeField]
	private UI_ListItem listItemPrefab;
	[SerializeField]
	private HandleCard handleCard;

	public void DisplayInstructions(Instructions instructions)
	{
		DisplayStorageState(instructions, Storage.EMPTY);
		handleCard.DisplayInstructions(instructions);
	}

	public void DisplayStorageState(Instructions instructions, Storage storage)
	{
		header.text = $"Build a {instructions.ShipPartName}";

		foreach (var item in list.GetComponentsInChildren<UI_ListItem>())
		{
			Destroy(item.gameObject);
		}

		foreach (Instructions.Entry instruction in instructions.Entries)
		{
			var storageEntry = storage.GetStorageEntry(instruction.MaterialType);
			Instantiate(listItemPrefab, list)
				.DisplayInstrunction(instruction, storageEntry);
		}
	}
}
