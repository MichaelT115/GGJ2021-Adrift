using UnityEngine;

[CreateAssetMenu(fileName = "New Material Type", menuName = "New Material Type")]
public sealed class MaterialType : ScriptableObject
{
	[SerializeField]
	private string displayName;
	public string DisplayName => displayName;
}
