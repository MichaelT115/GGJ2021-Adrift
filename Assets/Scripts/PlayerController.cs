using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float step;
	[SerializeField]
	private float itemWeight;

	private Grabber playerGrabber;
	private Animator anim;

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
		if (IsWalking)
		{
			WalkInDirection(Direction);
		}

		if (!IsWalking)
		{
			if (!playerGrabber.IsHoldingObject)
			{
				anim.SetBool("Carrying Idle", false);
				anim.SetBool("Idle", true);
			}

			if (playerGrabber.IsHoldingObject)
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
				}
			}
			else
			{
				playerGrabber.DropItem();
			}
		}

		itemWeight = playerGrabber.GrabbedObjectWeight;
	}

	private void WalkInDirection(float zRotation)
	{
		float speed = (step - itemWeight) * Time.deltaTime;
		transform.localRotation = Quaternion.Euler(0, zRotation, 0);
		transform.Translate(Vector3.forward * speed);

		if (!playerGrabber.IsHoldingObject)
		{
			anim.SetBool("Walking", true);
			anim.SetBool("Idle", false);
			anim.SetBool("Carrying Idle", false);
			anim.SetBool("Carrying Walking", false);
		}

		if (playerGrabber.IsHoldingObject)
		{
			anim.SetBool("Walking", false);
			anim.SetBool("Idle", false);
			anim.SetBool("Carrying Idle", false);
			anim.SetBool("Carrying Walking", true);
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
