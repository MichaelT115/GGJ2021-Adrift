using UnityEngine;
using UnityEngine.Events;

public sealed class WaterManager : MonoBehaviour
{
	[SerializeField]
	private bool hasCrested = false;

	[SerializeField]
	private float time = 10;

	[SerializeField]
	private float highTideCountdown = 15;
	[SerializeField]
	private float lowTideCountdown = 5;

	private enum Tide
	{
		LOW_TIDE,
		HIGH_TIDE
	}

	[SerializeField]
	private Tide tideLevel;

	[SerializeField]
	private UnityEvent onWaterCreasted;

	private void Start()
	{
		hasCrested = true;

	}

	private void Update()
	{
		UpdateWaterHeight();
	}

	public float WaterHeight
	{
		get => transform.position.y;
		private set
		{
			Vector3 position = transform.position;
			transform.position = new Vector3(position.x, value, position.z);
		}
	}

	private void UpdateWaterHeight()
	{
		switch (tideLevel)
		{
			case Tide.LOW_TIDE:
				WaterHeight = Mathf.Lerp(WaterHeight, DesiredLowTideHeight, Time.deltaTime * 1.5f);

				highTideCountdown -= Time.deltaTime;

				if (highTideCountdown <= 0)
				{
					highTideCountdown += 15;

					tideLevel = Tide.HIGH_TIDE;
					hasCrested = false;
				}
				break;
			case Tide.HIGH_TIDE:
				WaterHeight = Mathf.Lerp(WaterHeight, DesiredHighTideHeight, Time.deltaTime * 0.5f);

				lowTideCountdown -= Time.deltaTime;

				if (lowTideCountdown <= 0)
				{
					lowTideCountdown += 5;

					tideLevel = Tide.LOW_TIDE;

					onWaterCreasted.Invoke();
					hasCrested = true;
				}
				break;
		}
	}

	private float DesiredHighTideHeight => Mathf.Sin(GameManager.GameTime) * 0.10f + 1.0f;
	private float DesiredLowTideHeight => (Mathf.Sin(GameManager.GameTime * 0.5f) * 0.025f) + 0.025f;

	private void OnTriggerExit(Collider other)
	{
		var maximumPointOnColliderY = other.bounds.max.y;
		var isUnderneathWater = maximumPointOnColliderY < transform.position.y;

		if (!hasCrested && isUnderneathWater && other.TryGetComponent(out Item item))
		{
			item.Remove();
		}		
	}
}
