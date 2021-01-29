using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public bool isGrabbable;
    public bool Grabbed;

    [SerializeField]
    private Transform grabbedObjectAnchor;
    
    [SerializeField]
    private GameObject grabbableObject;

    [SerializeField]
    private List<GameObject> grabbableObjects = new List<GameObject>();

    private Transform grabbableObjectSize;

    public float GrabbedObjectWeight;
    private float grabbedObjectMass;

    BoxCollider grabbingCollider;

    private void Start()
    {
        grabbingCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            isGrabbable = true;
            grabbableObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            isGrabbable = false;
            grabbableObject = null;
        }
    }

    public void Grab()
    {
        grabbingCollider.enabled = false;

        Grabbed = true;
        grabbableObject.transform.SetParent(transform);

        Item itemObj = grabbableObject.GetComponent<Item>();

        if (itemObj != null)
        {
            itemObj.PickedUp();
        }

        grabbableObject.transform.localPosition = grabbedObjectAnchor.transform.localPosition;
        grabbableObject.transform.localRotation = grabbedObjectAnchor.transform.localRotation;

        Rigidbody rb = grabbableObject.GetComponent<Rigidbody>();
        Destroy(rb);

        GrabbedObjectWeight = rb.mass;
        grabbedObjectMass = rb.mass;
    }

    public void Drop()
    {
      grabbingCollider.enabled = true;

        Grabbed = false;

        grabbableObject.transform.SetParent(null);

        Rigidbody rb = grabbableObject.AddComponent<Rigidbody>();
        rb.mass = grabbedObjectMass;
        rb.useGravity = true;
        rb.isKinematic = false;

        GrabbedObjectWeight = 0;

        Item itemObj = grabbableObject.GetComponent<Item>();

        if (itemObj != null)
        {
            itemObj.Dropped();
        }
    }
}

