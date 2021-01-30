using UnityEngine;
using UnityEngine.Events;

public sealed class WaterManager : MonoBehaviour
{
	[SerializeField]
	private bool hasCrested = false;

	[SerializeField]
	private float time = 10;

	[SerializeField]
	private UnityEvent onWaterCreasted;

	private void Start()
	{
		hasCrested = true;
	}

	private void Update()
    {
        var position = transform.position;
		position.y = Mathf.Abs(Mathf.Sin(Time.time / time));
		transform.position = position;

		if (!hasCrested && position.y > 0.95f)
		{
			hasCrested = true;
			onWaterCreasted.Invoke();
		}

		if (hasCrested && position.y < 0.05f)
		{
			hasCrested = false;
		}
	}

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
