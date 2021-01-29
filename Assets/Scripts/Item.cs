using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private MaterialType materialType;
    [SerializeField]
    private GameObject textObject;
    Transform cameraPosition;

    Outline outline;

    [Header("Audio")]
    AudioSource itemAudioSource;
    [SerializeField]
    private AudioClip pickupItemAudioClip;
    [SerializeField]
    private AudioClip dropItemAudioClip;

	public MaterialType MaterialType { get => materialType; set => materialType = value; }

	private void Start()
    {
        outline = GetComponentInChildren<Outline>();
        itemAudioSource = GetComponent<AudioSource>();
        cameraPosition = GameObject.FindWithTag("MainCamera").transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!outline.enabled)
        {
           outline.enabled = true;
        }

        textObject.SetActive(true);
        textObject.transform.LookAt(cameraPosition.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        HideTextAndOutline();
    }

    public void PickedUp()
    {
        transform.localScale = new Vector3(.5f, .5f, .5f);
		if (pickupItemAudioClip != null)
		{
			itemAudioSource.PlayOneShot(pickupItemAudioClip);
		}

		HideTextAndOutline();
    }

    public void Dropped()
    {
        transform.localScale = new Vector3(.5f, .5f, .5f);

        if (dropItemAudioClip != null)
		{
			itemAudioSource.PlayOneShot(dropItemAudioClip);
		}
	}

    public void HideTextAndOutline()
    {
        textObject.SetActive(false);
        outline.enabled = false;
    }
}
