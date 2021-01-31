using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInteractionVisuals : MonoBehaviour
{
    Outline outline;
    [SerializeField]
    private GameObject shipTextObj;

    [SerializeField]
    private GameObject arrowObj;

    [SerializeField]
    private GameObject player;

    Grabber grabber;
    bool canAdd = false;

    private void Start()
    {
        outline = GetComponentInChildren<Outline>();
        grabber = player.GetComponentInChildren<Grabber>();
    }

    private void Update()
    {
        if (grabber.GrabbedItem != null)
        {
            arrowObj.SetActive(true);
        }

        else
        {
            arrowObj.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!outline.enabled && grabber.GrabbedItem != null)
            {
                shipTextObj.SetActive(true);
                outline.enabled = true;

                canAdd = true;
            }
        }

        else
        {
            canAdd = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (outline.enabled)
            {
                shipTextObj.SetActive(false);
                outline.enabled = false;
            }
        }
    }
}
