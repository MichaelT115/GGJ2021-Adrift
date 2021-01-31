using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaUrchin : MonoBehaviour
{
    [SerializeField]
    private AudioClip seaUrchinPop;

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player")) {

            GetComponent<AudioSource>().PlayOneShot(seaUrchinPop);
            GetComponent<Animation>().Play("Pop");

            Vector3 dir = c.contacts[0].point - transform.position;
            dir = -dir.normalized;
            c.gameObject.GetComponent<Rigidbody>().AddForce(dir * 15000);
            c.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 25000);
            c.gameObject.GetComponent<PlayerController>().GetHit();
        }
    }
}
