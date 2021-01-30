using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField]
    private Transform birdAI;

    [SerializeField]
    private Transform poopSpawnPoint;
    [SerializeField]
    private GameObject poop;

    [SerializeField]
    private float speed;
    private bool canPoop = true;

    [SerializeField]
    float poopingSpeed = 1;

    [Header("Waypoint")]

    [SerializeField]
    private Vector3 pointA;
    [SerializeField]
    private Vector3 pointB;

    void Update()
    {
        SetBirdPosition();
        Poop();
    }

    private void SetBirdPosition()
    {            
        birdAI.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time * speed, 1));
    }

    private void Poop()
    {
        if(canPoop)
        {
            StartCoroutine(SpawnPoop());    
        }
    }

    IEnumerator SpawnPoop()
    {
        canPoop = false;
        Instantiate(poop, poopSpawnPoint.position, poopSpawnPoint.rotation);
        yield return new WaitForSeconds(poopingSpeed);
        canPoop = true;
    }
}
