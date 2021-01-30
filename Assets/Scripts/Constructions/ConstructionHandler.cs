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
	private int tier = 0;

	[SerializeField]
	private UnityEvent onShipComplete;
	[SerializeField]
	private UnityEvent onTierComplete;

	[Serializable]
	private sealed class InstructionsEvent : UnityEvent<Instructions> { }

	[Serializable]
	private sealed class StorageStateEvent : UnityEvent<Instructions, Storage> { }

	[SerializeField]
	private InstructionsEvent onNewInstructions;

	[SerializeField]
	private StorageStateEvent onStorageState;

	private void Start()
	{
		CurrentInstructions = gameSequence.Tiers[tier].Instructions;
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

			onTierComplete.Invoke();
			if (gameSequence.TierCount == tier + 1)
			{
				CurrentInstructions = null;
				onShipComplete.Invoke();
			}
			else
			{
				++tier;
				CurrentInstructions = gameSequence.Tiers[tier].Instructions;
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
	public UnityEvent OnTierComplete => onTierComplete;
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
}
