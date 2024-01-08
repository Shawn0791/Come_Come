using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public Transform playerPos;
    public float smoothTime;
    public float chaseTime;
    public AudioClip bark;
 
    private bool isComing;
    private float distance;
    private float speed;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        StartChasing();
    }

    Vector3 velocity;
    public void Comecome()
    {
        distance= Vector3.Distance(playerPos.position, transform.position);
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
            AudioSource.PlayClipAtPoint(bark, transform.position);
        }
        else if (other.CompareTag("Unbreakable") || other.CompareTag("Player")) 
        {
            StopChasing();
            AudioSource.PlayClipAtPoint(bark, transform.position);
        }
        else if (other.CompareTag("Player2"))
        {
            other.GetComponent<Player2Movement>().Dead();
            AudioSource.PlayClipAtPoint(bark, transform.position);
        }
        else if (other.CompareTag("Dragon"))
        {
            other.transform.parent.GetComponent<enemy>().Dead();
            AudioSource.PlayClipAtPoint(bark, transform.position);
        }
        else if (other.CompareTag("Pig"))
        {
            other.GetComponent<PIG>().Dead();
            AudioSource.PlayClipAtPoint(bark, transform.position);
        }
    }
}
