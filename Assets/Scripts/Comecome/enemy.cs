using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 4;
    public float attackDis = 2;
    public float attackCD = 1;
    public AudioClip bite;

    private Transform player;
    private bool isAttack;
    private bool isDead;
    private Animator anim;
    private float attackTimer;


    private void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttack && attackTimer <= 0 && !isDead)  
        {
            Attack();
        }

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isDead) 
        {
            player = other.transform;
            ChasingPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttack && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Dead();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void ChasingPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > attackDis)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            anim.SetBool("isWalking", true);

            //turn to player
            if (!isAttack)
            {
                transform.forward = -(player.position - transform.position);
            }
        }
        else
        {
            isAttack = true;
        }
    }

    private void Attack()
    {
        anim.SetTrigger("isAttack");
        attackTimer = attackCD;
        isAttack = false;
        AudioSource.PlayClipAtPoint(bite, transform.position);
    }

    public void Dead()
    {
        isDead = true;
        GetComponent<BreakDown>().BreakToPieces();
    }
}
