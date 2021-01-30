using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Material Type", menuName = "New Material Type")]
public sealed class MaterialType : ScriptableObject
{
	[SerializeField]
	private string displayName;
	public string DisplayName => displayName;
}
