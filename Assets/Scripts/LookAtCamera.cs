using UnityEngine;

public sealed class LookAtCamera : MonoBehaviour
{
	private Transform cameraTransform = null;

	private void Start() => cameraTransform = Camera.main.transform;

	private void Update() => transform.LookAt(cameraTransform, Vector3.up);
}
