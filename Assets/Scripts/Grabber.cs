using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	private const string ITEM_TAG = "Item";

	[SerializeField]
    private Transform grabbedObjectAnchor;
    [SerializeField]
	private GameObject nearGrabbableObject;
    [SerializeField]
    private Item grabbedItem;

    private BoxCollider grabbingCollider;

	private void Start()
    {
        grabbingCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(ITEM_TAG))
        {
            nearGrabbableObject = other.gameObject;
        }
    }

	private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(ITEM_TAG))
        {
            nearGrabbableObject = null;
        }
    }

    public void GrabItem()
    {
        grabbedItem = nearGrabbableObject.GetComponent<Item>();
        grabbedItem.PickedUp();

        grabbingCollider.enabled = false;
        grabbedItem.transform.SetParent(transform);
        grabbedItem.transform.localPosition = grabbedObjectAnchor.localPosition;
        grabbedItem.transform.localRotation = grabbedObjectAnchor.localRotation;

        grabbedItem.Rigidbody.isKinematic = true;
        grabbedItem.Rigidbody.detectCollisions = false;
    }

    public void DropItem()
    {
        grabbingCollider.enabled = true;
        grabbedItem.transform.SetParent(null);
        grabbedItem.Rigidbody.isKinematic = false;
        grabbedItem.Rigidbody.detectCollisions = true;

     //   grabbedItem.Dropped();
        grabbedItem = null;
    }

    public void RemoveItem()
    {
        grabbingCollider.enabled = true;
        Destroy(grabbedItem.gameObject);
        grabbedItem = null;
    }

    public Item GrabbedItem => grabbedItem;
    public bool IsNearGrabbableObject => nearGrabbableObject != null;
    public GameObject NearGrabableObject => nearGrabbableObject;
    public bool IsHoldingObject => grabbedItem != null;
    public float GrabbedObjectWeight => GrabbedItem != null ? GrabbedItem.Rigidbody.mass : 0;
}
