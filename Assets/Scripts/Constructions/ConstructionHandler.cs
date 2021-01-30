using System;
using UnityEngine;
using UnityEngine.Events;

public sealed class ConstructionHandler : MonoBehaviour
{
	[SerializeField]
	private Storage storage;

	[SerializeField]
	private Instructions currentInstructions;

	[SerializeField]
	private GameSequence gameSequence;

	[SerializeField]
	private int tierLevel = 0;

	#region Events
	[Serializable]
	private sealed class InstructionsEvent : UnityEvent<Instructions> { }

	[Serializable]
	private sealed class StorageStateEvent : UnityEvent<Instructions, Storage> { }

	[Serializable]
	private sealed class TierEvent : UnityEvent<int> { };

	[SerializeField]
	private UnityEvent onShipComplete;
	[SerializeField]
	private TierEvent onTierComplete;
	[SerializeField]
	private InstructionsEvent onNewInstructions;
	[SerializeField]
	private StorageStateEvent onStorageState;
	#endregion


	private void Start() => SetTier(0);

	private void SetTier(int i)
	{
		TierLevel = i;
		CurrentInstructions = gameSequence.Tiers[tierLevel].Instructions;
	}

	public bool CanAddToStorage(MaterialType materialType)
	{
		var instructionsEntry = CurrentInstructions.GetEntry(materialType);
		var storageEntry = storage.GetStorageEntry(materialType);

		return instructionsEntry.Count > storageEntry.Count;
	}

	public void AddToStorage(MaterialType materialType)
	{
		storage.Add(materialType);

		onStorageState.Invoke(currentInstructions, storage);

		if (IsConstructionFinished(CurrentInstructions, storage))
		{
			storage.Clear();

			bool isFinalTier = gameSequence.TierCount == tierLevel + 1;
			if (isFinalTier)
			{
				CurrentInstructions = null;
				onShipComplete.Invoke();
			}
			else
			{
				SetTier(tierLevel + 1);
			}
		}
	}

	public static bool IsConstructionFinished(in Instructions instructions, in Storage storage)
	{
		foreach (var instruction in instructions.Entries)
		{
			var storageEntry = storage.GetStorageEntry(instruction.MaterialType);

			if (instruction.Count != storageEntry.Count)
			{
				return false;
			}
		}
		return true;
	}

	public UnityEvent OnShipComplete => onShipComplete;
	public UnityEvent<int> OnTierComplete => onTierComplete;
	public UnityEvent<Instructions> OnNewInstructions => onNewInstructions;
	public UnityEvent<Instructions, Storage> OnStorageState => onStorageState;
	public Instructions CurrentInstructions
	{
		get => currentInstructions;
		private set
		{
			currentInstructions = value;
			onNewInstructions.Invoke(currentInstructions ? currentInstructions : Instructions.EMPTY);
		}
	}
	public int TierLevel
	{
		get => tierLevel;
		private set
		{
			tierLevel = value;
			onTierComplete.Invoke(tierLevel);
		}
	}
}
