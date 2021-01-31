using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
	private const string PARAMETER_CARRYING_WALKING = "Carrying Walking";
	private const string PARAMETER_WALKING = "Walking";
	private const string PARAMETER_CARRYING_IDLE = "Carrying Idle";
	private const string PARAMETER_IDLE = "Idle";
	private const string TRIGGER_GET_HIT = "Get Hit";
	private const string TAG_WATER = "Water";

	[SerializeField]
    private float step;
	[SerializeField]
	private float itemWeight;

	private Grabber playerGrabber;
	private Animator anim;

    bool canMove = true;

    Rigidbody rb;

    [SerializeField]
    private ParticleSystem playerStunParticles;
    private float waterSlowingValue = 1;

    AudioSource playerAudioSource;

    [SerializeField]
    private AudioClip confirmAudioClip;
    [SerializeField]
    private AudioClip rejectAudioClip;
    [SerializeField]
    private AudioClip takeDamageAudioClip;
    

    private void Start()
    {
        itemWeight = 0;
        rb = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
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
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			GameManager.TogglePause();
			anim.speed = GameManager.IsPaused ? 0 : 1;
		}

		if (!GameManager.IsPaused)
		{
			if (IsWalking && canMove)
			{
				WalkInDirection(Direction);
			}

			if (IsIdle)
			{
				anim.SetBool(PARAMETER_CARRYING_WALKING, false);
				anim.SetBool(PARAMETER_WALKING, false);

				if (playerGrabber.IsHoldingObject)
				{
					anim.SetBool(PARAMETER_CARRYING_IDLE, true);
					anim.SetBool(PARAMETER_IDLE, false);
				}
				else
				{
					anim.SetBool(PARAMETER_CARRYING_IDLE, false);
					anim.SetBool(PARAMETER_IDLE, true);
				}
			}
			else
			{
				anim.SetBool(PARAMETER_CARRYING_IDLE, false);
				anim.SetBool(PARAMETER_IDLE, false);

				if (playerGrabber.IsHoldingObject)
				{
					anim.SetBool(PARAMETER_CARRYING_WALKING, true);
					anim.SetBool(PARAMETER_WALKING, false);
				}
				else
				{
					anim.SetBool(PARAMETER_CARRYING_WALKING, false);
					anim.SetBool(PARAMETER_WALKING, true);
				}
			}
		}
	}

	private bool IsIdle => !IsWalking && canMove;

	private void CheckForGrabber()
    {
        if (!playerGrabber.IsHoldingObject && playerGrabber.IsNearGrabbableObject && Input.GetKeyDown(KeyCode.F))
        {
            playerGrabber.GrabItem();
        }

        else if (playerGrabber.IsHoldingObject && Input.GetKeyDown(KeyCode.F))
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
                    playerAudioSource.PlayOneShot(confirmAudioClip);

                }

                else
                {
                    playerGrabber.DropItem();
                    playerAudioSource.PlayOneShot(rejectAudioClip);
                }
			}
			else
			{
				playerGrabber.DropItem();
			}
		}

		itemWeight = playerGrabber.GrabbedObjectWeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(TAG_WATER))
        {
            waterSlowingValue = 3;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(TAG_WATER))
        {
            waterSlowingValue = 1;
        }
    }

    private void WalkInDirection(float zRotation)
    {
        float speed = ((step - itemWeight)/waterSlowingValue) * Time.deltaTime;
        transform.localRotation =  Quaternion.Euler(0, zRotation, 0);
        transform.Translate(Vector3.forward * speed);

        if (!playerGrabber.IsHoldingObject)
        {
            anim.SetBool(PARAMETER_WALKING, true);
            anim.SetBool(PARAMETER_IDLE, false);
            anim.SetBool(PARAMETER_CARRYING_IDLE, false);
            anim.SetBool(PARAMETER_CARRYING_WALKING, false);
        }

        if (playerGrabber.IsHoldingObject)
        {
            anim.SetBool(PARAMETER_CARRYING_WALKING, true);
            anim.SetBool(PARAMETER_WALKING, false);
            anim.SetBool(PARAMETER_IDLE, false);
            anim.SetBool(PARAMETER_CARRYING_IDLE, false);          
        }
    }

	public bool IsWalking =>
		Input.GetKey(KeyCode.W) ||
		Input.GetKey(KeyCode.A) ||
		Input.GetKey(KeyCode.S) ||
		Input.GetKey(KeyCode.D);

	public float Direction
	{
		get
		{
			Vector3 direction = new Vector3();
			if (Input.GetKey(KeyCode.W))
			{
				direction += Vector3.forward;
			}
			if (Input.GetKey(KeyCode.A))
			{
				direction += Vector3.left;
			}
			if (Input.GetKey(KeyCode.S))
			{
				direction += Vector3.back;
			}
			if (Input.GetKey(KeyCode.D))
			{
				direction += Vector3.right;
			}

			return Mathf.Atan2(-direction.z, direction.x) * Mathf.Rad2Deg;
		}
	}

    public void GetHit()
    {
        if (canMove)
        {
            StartCoroutine(GettingHit());
        }
    }

    public IEnumerator GettingHit()
    {
        canMove = false;

        playerStunParticles.Play();
        playerAudioSource.PlayOneShot(takeDamageAudioClip);

        if (playerGrabber.GrabbedItem != null)
        {
            playerGrabber.DropItem();
        }

        anim.SetTrigger(TRIGGER_GET_HIT);

        yield return new WaitForSeconds(1.6f); //lenght of animation

        playerStunParticles.Stop();
        canMove = true;
    }

    public ConstructionHandler ConstructionZone
	{
		get
		{
			Collider[] colliders = Physics.OverlapSphere(transform.position, 0.2f);
			for (int i = 0; i < colliders.Length; ++i)
			{
				if (colliders[i].TryGetComponent(out ConstructionHandler constructionHandler))
				{
					return constructionHandler;
				}
			}

			return null;
		}
	}

	public float ItemWeight => itemWeight;
}
