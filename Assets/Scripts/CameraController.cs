using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset = new Vector3(12, 5, 0);

    [SerializeField]
    private float interpolant = 2;

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        Vector3 desiredPosition = target.position + offset;

        Vector3 newPosition = Vector3.Lerp(currentPosition, desiredPosition, interpolant * Time.deltaTime);
        transform.position = newPosition;
        transform.forward = target.position - transform.position;
    }
}
