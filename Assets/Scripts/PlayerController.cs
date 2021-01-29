using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float step;
    public float itemWeight;

    Grabber playerGrabber;
    Animator anim;

    private void Start()
    {
        itemWeight = 0;

        anim = GetComponent<Animator>();
        playerGrabber = GetComponentInChildren<Grabber>();
    }

    void Update()
    {
        Controls();
        CheckForGrabber();
    }

    private void Controls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            walking(-90);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            walking(180);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            walking(90);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            walking(0);
        }

        if(!Input.GetKey(KeyCode.W) &&
            !Input.GetKey(KeyCode.A) &&
            !Input.GetKey(KeyCode.S) &&
            !Input.GetKey(KeyCode.D))
        {
            if (!playerGrabber.Grabbed)
            {
                anim.SetBool("Carrying Idle", false);              
                anim.SetBool("Idle", true);
            }

            if (playerGrabber.Grabbed)
            {
                anim.SetBool("Carrying Idle", true);
                anim.SetBool("Idle", false);             
            }

            anim.SetBool("Walking", false);
            anim.SetBool("Carrying Walking", false);
        }
    }

    private void CheckForGrabber()
    {
        if (!playerGrabber.Grabbed && playerGrabber.isGrabbable && Input.GetKeyDown(KeyCode.F))
        {
            playerGrabber.Grab();
        }

        else if (playerGrabber.Grabbed && Input.GetKeyDown(KeyCode.F))
		{
			ConstructionHandler constructionHandler = ConstructionZone;
			if (constructionHandler != null)
			{
				var item = playerGrabber.GrabbedItem;
				var canAddToStorage = constructionHandler.CanAddToStorage(item.MaterialType);

				if (canAddToStorage)
				{
					constructionHandler.AddToStorage(item.MaterialType);
					playerGrabber.RemoveItem();
				}
			}
			else
			{
				playerGrabber.Drop();
			}
		}

		itemWeight = playerGrabber.GrabbedObjectWeight;
    }

	private ConstructionHandler ConstructionZone
	{
		get
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
			for (int i = 0; i < colliders.Length; ++i)
			{
				if (colliders[i].CompareTag("Construction Zone"))
				{
					return colliders[i].GetComponent<ConstructionHandler>();
				}
			}

			return null;
		}
	}

	private void walking(float zRotation)
    {
        float speed = (step - itemWeight) * Time.deltaTime;
        transform.localRotation =  Quaternion.Euler(0, zRotation, 0);
        transform.Translate(Vector3.forward * speed);

        if (!playerGrabber.Grabbed)
        {
            anim.SetBool("Walking", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Carrying Idle", false);
            anim.SetBool("Carrying Walking", false);
        }

        if (playerGrabber.Grabbed)
        {
            anim.SetBool("Walking", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Carrying Idle", false);
            anim.SetBool("Carrying Walking", true);
        }
    }
}
