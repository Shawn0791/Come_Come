using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Movement : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    private float velocityX;
    private float velocityY;
    private float velocityZ;

    public float speed;
    public float smoothTime;
    public bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    void Update()
    {
        if (!isDead)
        {
            Movement();
            Jump();
        }
    }
    Vector3 velocity;
    void Movement()
    {
        if (Input.GetAxisRaw("Horizontal2") > 0)
        {
            //rb.velocity = new Vector3(Mathf.SmoothDamp(rb.velocity.x, speed, ref velocity.x, smoothTime), rb.velocity.y, rb.velocity.z);
            velocityX = Mathf.SmoothDamp(rb.velocity.x, speed, ref velocity.x, smoothTime);
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetAxisRaw("Horizontal2") < 0)
        {
            //rb.velocity = new Vector3(Mathf.SmoothDamp(rb.velocity.x, -speed, ref velocity.x, smoothTime), rb.velocity.y, rb.velocity.z);
            velocityX = Mathf.SmoothDamp(rb.velocity.x, -speed, ref velocity.x, smoothTime);
            anim.SetBool("isMoving", true);
        }
        else
        {
            //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
            velocityX = 0;
        }

        if (Input.GetAxisRaw("Vertical2") > 0)
        {
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.SmoothDamp(rb.velocity.z, speed, ref velocity.z, smoothTime));
            velocityZ = Mathf.SmoothDamp(rb.velocity.z, speed, ref velocity.z, smoothTime);
            anim.SetBool("isMoving", true);
        }
        else if (Input.GetAxisRaw("Vertical2") < 0)
        {
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, Mathf.SmoothDamp(rb.velocity.z, -speed, ref velocity.z, smoothTime));
            velocityZ = Mathf.SmoothDamp(rb.velocity.z, -speed, ref velocity.z, smoothTime);
            anim.SetBool("isMoving", true);
        }
        else
        {
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
            velocityZ = 0;
        }

        rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(velocityX, rb.velocity.y, velocityZ).normalized * speed, ref velocity, smoothTime, speed);
        if (Input.GetAxisRaw("Vertical2") == 0 && Input.GetAxisRaw("Horizontal2") == 0)
            anim.SetBool("isMoving", false);
    }

    void Jump()
    {

    }

    public void Dead()
    {
        isDead = true;
        anim.SetTrigger("isDead");
        GameManager.instance.GameOver();
    }

}
