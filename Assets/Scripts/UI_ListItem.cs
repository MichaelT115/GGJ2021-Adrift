using UnityEngine;
using UnityEngine.UI;

public sealed class UI_ListItem : MonoBehaviour
{
	[SerializeField]
	private Image icon;
	[SerializeField]
	private TMPro.TMP_Text materialName;
	[SerializeField]
	private TMPro.TMP_Text count;

	public void DisplayInstrunction(Instructions.Entry instruction, Storage.StorageEntry storage)
	{
		icon.sprite = instruction.MaterialType.Icon;
		materialName.text = instruction.MaterialType.DisplayName;
		count.text = $"{storage.Count}/{instruction.Count}";
		icon.color = storage.Count != instruction.Count ? new Color(1, 1, 1, 1) : new Color(1, 1, 1, 0.5f);
	}
}
