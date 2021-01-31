using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    Animator monkeyAnimator;
    bool canThrow = true;

    [SerializeField]
    private GameObject Coconut;

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
       Pickup();

       yield return new WaitForSeconds(1.2f);

       Throw();

       yield return new WaitForSeconds(1);

       canThrow = true;
    }

    private void Pickup()
    {
      //  Instantiate(Coconut, )
    }

      private void Throw()
    {

    }
}
