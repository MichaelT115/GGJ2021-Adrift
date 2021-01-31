using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField]
    private GameObject poopSplatter;

    [SerializeField]
    private GameObject poopTargeter;
    GameObject poopTargeterObj;

    private RaycastHit targeter;

    private void Start()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, out targeter))
        {
            if (targeter.collider)
            {
                poopTargeterObj = Instantiate(poopTargeter, targeter.point, transform.rotation) as GameObject;
            }
        }      
    }

    private void Update()
    {
        Destroy(gameObject, 2);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject poopSplatterParticleObj = Instantiate(poopSplatter, transform.position, transform.rotation);

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            poopSplatterParticleObj.GetComponent<AudioSource>().Play();
            playerController.GetHit();
        }

        Destroy(poopTargeterObj);

        Destroy(poopSplatterParticleObj, 2);
        Destroy(gameObject);
    }
}
