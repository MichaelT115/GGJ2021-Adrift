using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Instructions", menuName = "New Instructions")]
public sealed class Instructions : ScriptableObject
{
	[Serializable]
	public struct Entry
	{
		[SerializeField]
		private MaterialType materialType;
		[SerializeField]
		private int count;

		public MaterialType MaterialType { get => materialType; set => materialType = value; }
		public int Count { get => count; set => count = value; }

		public static readonly Entry EMTPY = new Entry();
	}

	[SerializeField]
	private Entry[] entries;
	[SerializeField]
	private string shipPartName;
	[SerializeField]
	private Sprite card;

	public Entry[] Entries => entries;
	public string ShipPartName => shipPartName;

	public ref readonly Entry GetEntry(MaterialType materialType)
	{
		for (int i = 0; i < entries.Length; ++i)
		{
			if (entries[i].MaterialType == materialType)
			{
				return ref entries[i];
			}
		}
		return ref Entry.EMTPY;
	}

	public static Instructions EMPTY
	{
		get
		{
			var instructions = CreateInstance<Instructions>();

			instructions.entries = new Entry[0];

			return instructions;
		}
	}

	public Sprite Card => card;
}
