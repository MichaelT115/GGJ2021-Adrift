using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Item : MonoBehaviour
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
    public Rigidbody Rigidbody { get; private set; }

	private void Start()
    {
        outline = GetComponentInChildren<Outline>();
		itemAudioSource = GetComponent<AudioSource>();
        cameraPosition = GameObject.FindWithTag("MainCamera").transform;

        Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!outline.enabled)
        {
           outline.enabled = true;
        }

        textObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        HideTextAndOutline();
    }

    public void PickedUp()
    {
		if (pickupItemAudioClip != null)
		{
			itemAudioSource.PlayOneShot(pickupItemAudioClip);
		}

		HideTextAndOutline();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (dropItemAudioClip != null && collision.gameObject.CompareTag(("Terrain")))
        {
            itemAudioSource.PlayOneShot(dropItemAudioClip);
        }
    }

    public void Remove()
	{
        Destroy(gameObject);
	}

	public void HideTextAndOutline()
    {
        textObject.SetActive(false);
        outline.enabled = false;
    }
}
