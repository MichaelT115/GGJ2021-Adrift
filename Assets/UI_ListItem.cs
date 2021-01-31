using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class UI_ListItem : MonoBehaviour
{
	[SerializeField]
	private Image icon;
	[SerializeField]
	private TMPro.TMP_Text text;

	public TMPro.TMP_Text Text => text;
	public Image Icon => icon;
}
