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

	public void DisplayInstructions(Instructions instructions)
	{
		header.text = $"Build a: {instructions.ShipPartName}";

		foreach (var item in list.GetComponentsInChildren<UI_ListItem>())
		{
			Destroy(item.gameObject);
		}

		foreach(Instructions.Entry instruction in instructions.Entries)
		{
			var listItem = Instantiate<UI_ListItem>(listItemPrefab, list);
			listItemPrefab.Icon.sprite = instruction.MaterialType.Icon;
			listItemPrefab.Text.text = $"{instruction.MaterialType.DisplayName} {0}/{instruction.Count}";
		}
	}

	public void DisplayStorageState(Instructions instructions, Storage storage)
	{
		header.text = $"Build a: {instructions.ShipPartName}";

		foreach (var item in list.GetComponentsInChildren<UI_ListItem>())
		{
			Destroy(item.gameObject);
		}

		foreach (Instructions.Entry instruction in instructions.Entries)
		{
			var storageEntry = storage.GetStorageEntry(instruction.MaterialType);

			var listItem = Instantiate<UI_ListItem>(listItemPrefab, list);
			listItemPrefab.Icon.sprite = instruction.MaterialType.Icon;
			listItemPrefab.Text.text = $"{instruction.MaterialType.DisplayName} {storageEntry.Count}/{instruction.Count}";
		}
	}
}
