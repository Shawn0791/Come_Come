using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog2 : MonoBehaviour
{
    public Transform playerPos;
    public float smoothTime;
    public float chaseTime;

    private bool isComing;
    private float distance;
    private float speed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartChasing();
    }

    Vector3 velocity;
    public void Comecome()
    {
        distance = Vector3.Distance(playerPos.position, transform.position);
        float a = distance / chaseTime;

        if (a >= 15)
            speed = a;
        else
            speed = 15;

        isComing = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    private void StartChasing()
    {
        if (isComing)
        {
            transform.position = Vector3.SmoothDamp(transform.position, playerPos.position, ref velocity, smoothTime, speed);
            //heard forward
            transform.forward = -(playerPos.position - transform.position);
        }
    }

    private void StopChasing()
    {
        isComing = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Breakable"))
        {
            other.GetComponent<BreakDown>().BreakToPieces();
        }
        else if (other.CompareTag("Unbreakable") || other.CompareTag("Player2"))
        {
            StopChasing();
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Dead();
        }
    }
}
