using System;
using UnityEngine;

using Random = UnityEngine.Random;

public sealed class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private Item[] spawnableItems;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int spawnCount = 5;

	private Transform[] GetShuffledSpawnPoints()
	{
		Transform[] shuffledSpawnPoints = new Transform[spawnPoints.Length];
		Array.Copy(spawnPoints, shuffledSpawnPoints, spawnPoints.Length);

		for (int i = shuffledSpawnPoints.Length - 1; i > 1; --i)
		{
			int target = Random.Range(0, i);
			var temp = shuffledSpawnPoints[i];
			shuffledSpawnPoints[i] = shuffledSpawnPoints[target];
			shuffledSpawnPoints[target] = temp;
		}

		return shuffledSpawnPoints;
	}

	private Item RandomItem => spawnableItems[Random.Range(0, spawnableItems.Length)];

	private void Awake()
	{
		var spawnPointObjects = GameObject.FindGameObjectsWithTag("Spawn Point");
		spawnPoints = new Transform[spawnPointObjects.Length];
		for (int i = 0; i < spawnPointObjects.Length; i++)
		{
			spawnPoints[i] = spawnPointObjects[i].transform;
		}
	}

	[ContextMenu(itemName: "Spawn Items")]
	public void SpawnItems()
    {
		var shuffledSpawnPoints = GetShuffledSpawnPoints();

		int maxPossibleSpawn = Math.Min(spawnCount, shuffledSpawnPoints.Length);
		for (int i = 0; i < maxPossibleSpawn; ++i)
		{
			Transform spawnPoint = shuffledSpawnPoints[i];
			Vector3 spawnPosition = spawnPoint.position;
			Quaternion rotation = Random.rotation;
			Instantiate(RandomItem, spawnPosition, rotation);
		}
    }
}
