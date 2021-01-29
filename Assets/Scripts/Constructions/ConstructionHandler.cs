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


	[SerializeField]
	private InstructionsEvent onNewInstructions;

	private void Start()
	{
		currentInstructions = gameSequence.Tiers[tier].Instructions;
	}

	public bool CanAddToStorage(MaterialType materialType)
	{
		var instructionsEntry = currentInstructions.GetEntry(materialType);
		var storageEntry = storage. GetStorageEntry(materialType);

		return instructionsEntry.Count > storageEntry.Count;
	}

	public void AddToStorage(MaterialType materialType)
	{
		storage.Add(materialType);

		var constructionIsFinished = IsConstructionFinished(currentInstructions, storage);

		if (constructionIsFinished)
		{
			onTierComplete.Invoke();
			if (gameSequence.TierCount == tier + 1)
			{
				currentInstructions = null;
				onShipComplete.Invoke();
			} 
			else
			{
				currentInstructions = gameSequence.Tiers[tier].Instructions;
				onNewInstructions.Invoke(currentInstructions);
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
}
