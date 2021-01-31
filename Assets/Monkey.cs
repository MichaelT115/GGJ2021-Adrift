using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    Animator monkeyAnimator;
    bool canThrow = true;

    [SerializeField]
    private GameObject Coconut;

    [SerializeField]
    private Transform handAnchor;

    private float exitTime = 0;
    private float throwSpeed = 12;
    private float upwardsForce = 2;

    private void Update()
    {
        if (canThrow)
        {
            canThrow = false;
            StartCoroutine(ThrowObject());
        }
    }

    private IEnumerator ThrowObject()
    {

        yield return new WaitForSeconds(4.767f + exitTime); //length of dance animation and buffer

        Pickup();

       yield return new WaitForSeconds(.8f);

       Throw();

       yield return new WaitForSeconds(1.4f + exitTime);

       canThrow = true;
    }

    private GameObject coconutObj;

    private void Pickup()
    {
       coconutObj = Instantiate(Coconut, handAnchor.position, handAnchor.rotation) as GameObject;
       coconutObj.transform.SetParent(handAnchor);
    }

      private void Throw()
    {
        Rigidbody rb = coconutObj.GetComponent<Rigidbody>();
        coconutObj.transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(transform.forward * throwSpeed, ForceMode.Impulse);
        rb.AddForce(transform.up * upwardsForce, ForceMode.Impulse);
    }
}
