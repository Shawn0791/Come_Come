using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIG : MonoBehaviour
{
    public Transform player;
    public Transform pigPos;
    public BoxCollider attack2Coll;
    public GameObject[] pigs;
    public float speed = 3;
    public float dushSpeed = 10;
    public float attackDis = 5;
    public float attack1CD;
    public float attack2CD;
    [Header("Audio")]
    public AudioClip attack1Audio;
    public AudioClip attack2Audio;
    public AudioClip rushingAudio;

    private Animator anim;
    private bool attack1;
    private bool attack2;
    private bool dushing;
    private bool isDead;
    private float attack1Timer;
    private float attack2Timer;
    private Vector3 attack2Target;

    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Dushing();
        AttackChoose();

        if (attack1Timer > 0)
            attack1Timer -= Time.deltaTime;

        if (attack2Timer > 0)
            attack2Timer -= Time.deltaTime;

        if (player != null && !attack1 && !dushing && !attack2 && !isDead)   
        {
            ChasingPlayer();
        }
    }

    private void ChasingPlayer()
    {
        if (Vector3.Distance(pigPos.position, player.position) > attackDis)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            anim.SetBool("isWalking", true);

            //turn to player
            if (!attack1 && !isDead) 
            {
                transform.forward = pigPos.position - player.position;
            }
        }
        else
        {
            attack1 = true;
        }
    }

    private void Attack1()
    {
        anim.SetTrigger("Attack1");
        attack1Timer = attack1CD;
    }

    public void Attack1Finish()
    {
        attack1 = false;
    }
    public void Attack1Audio()
    {
        AudioSource.PlayClipAtPoint(attack1Audio, player.position);
    }
    public void Attack2()
    {
        attack2Target = player.position;
        dushing = true;
        attack2 = false;
        AudioSource.PlayClipAtPoint(rushingAudio, player.position);
    }

    private void Dushing()
    {
        if (dushing && Vector3.Distance(pigPos.position, attack2Target) > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, attack2Target, dushSpeed * Time.deltaTime);
            attack2Coll.enabled = true;
        }
        else if (dushing && Vector3.Distance(pigPos.position, attack2Target) < 1)  
        {
            dushing = false;
            attack2Coll.enabled = false;
            Debug.Log("close");
        }
    }

    private void AttackChoose()
    {
        if (attack1 && attack1Timer <= 0 && !isDead && !dushing && !attack2) 
        {
            Attack1();
        }

        if (!attack2 && attack2Timer <= 0 && !isDead && player != null)  
        {
            anim.SetTrigger("Attack2");
            attack2Timer = attack2CD;
            attack2 = true;
            AudioSource.PlayClipAtPoint(attack2Audio, player.position);
        }
    }

    public void Dead()
    {
        if (pigs.Length != 0)  
        {
            StartCoroutine(Reborn());
            GetComponent<BreakDown>().BreakToPieces();
        }
        else
        {
            GameManager.instance.GameSuccess();
            GetComponent<BreakDown>().BreakToPieces();
        }

        isDead = true;
    }

    private IEnumerator Reborn()
    {
        pigs[0].transform.position = transform.position;
        yield return new WaitForSeconds(0.5f);
        pigs[0].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Dead();
        }
        else if (other.CompareTag("Breakable"))
        {
            other.GetComponent<BreakDown>().BreakToPieces();
        }
    }
}
