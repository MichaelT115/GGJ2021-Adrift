using UnityEngine;

[CreateAssetMenu(fileName = "New Material Type", menuName = "New Material Type")]
public sealed class MaterialType : ScriptableObject
{
	[SerializeField]
	private new string name;

	public string Name { get => name; set => name = value; }
}
