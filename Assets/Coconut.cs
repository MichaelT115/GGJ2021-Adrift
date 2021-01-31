using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coconut : MonoBehaviour
{

    [SerializeField]
    private GameObject coconutParticles;

    private void OnCollisionEnter(Collision other)
    {
        GameObject coconutParticlesObj = Instantiate(coconutParticles, transform.position, transform.rotation);

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            playerController.GetHit();
            AudioSource audioSource = coconutParticlesObj.GetComponent<AudioSource>();
            audioSource.Play();
        }

        Destroy(coconutParticlesObj, 2);
        Destroy(gameObject);
    }
}
