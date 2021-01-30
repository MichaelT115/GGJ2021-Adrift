using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Storage
{
	[Serializable]
	public struct StorageEntry
	{
		[SerializeField]
		private MaterialType materialType;
		[SerializeField]
		private int count;

		public static readonly StorageEntry EMTPY = new StorageEntry();

		public MaterialType MaterialType { get => materialType; set => materialType = value; }
		public int Count { get => count; set => count = value; }
	}


	[SerializeField]
	private List<StorageEntry> storageEntries;

	public void Add(MaterialType materialType)
	{
		for (int i = 0; i < storageEntries.Count; ++i)
		{
			StorageEntry entry = storageEntries[i];
			if (entry.MaterialType == materialType)
			{
				storageEntries[i] = new StorageEntry() { MaterialType = materialType, Count = entry.Count + 1 };
				return;
			}
		}

		storageEntries.Add(new StorageEntry()
		{
			MaterialType = materialType,
			Count = 1
		});
	}

	public void Clear()
	{
		storageEntries.Clear();
	}

	public StorageEntry GetStorageEntry(MaterialType materialType)
	{
		for (int i = 0; i < storageEntries.Count; ++i)
		{
			if (storageEntries[i].MaterialType == materialType)
			{
				return storageEntries[i];
			}
		}
		return StorageEntry.EMTPY;
	}

	public static readonly Storage EMPTY = new Storage()
	{
		storageEntries = new List<StorageEntry>()
	};
}
