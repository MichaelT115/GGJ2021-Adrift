using UnityEngine;

[CreateAssetMenu(fileName = "New Game Sequence", menuName = "New Game Sequence")]
public sealed class GameSequence : ScriptableObject
{
	[SerializeField]
	private Tier[] tiers = new Tier[4];

	public int TierCount => Tiers.Length;

	public Tier[] Tiers { get => tiers; set => tiers = value; }
}
