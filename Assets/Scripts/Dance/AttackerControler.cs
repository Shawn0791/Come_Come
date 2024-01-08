using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerControler : MonoBehaviour
{
    private Animator anim;
    private float attackTimer;

    public Animator dancerAnim;
    public Camera attackCam;
    public Transform DancerPos;
    public float attackCD;
    [Header("Mouse ray")]
    public LayerMask dancerHead;
    public float mouseRayDis;
    public Vector3 attackPos;
    [Header("Debug")]
    public bool isAttack;
    public bool pointToHead;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        Timer();

        //if attack the head
        Ray mouseRay = attackCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit headInfo;
        pointToHead = Physics.Raycast(mouseRay, out headInfo,mouseRayDis,dancerHead);
        //mouse position
        RaycastHit info;
        bool ray = Physics.Raycast(mouseRay, out info, mouseRayDis);
        attackPos = attackCam.ScreenToWorldPoint(info.point);//!!!!DOESN'T WORK!!!!

        if (!isAttack)
        {
            if (Input.GetMouseButtonDown(0) && !pointToHead)
            {
                //which point to attack
                if (info.point.x < DancerPos.position.x)
                {
                    //attack left
                    Debug.Log("attack left");
                    dancerAnim.SetTrigger("TwistLeft");
                }
                else
                {
                    //attack right
                    Debug.Log("attack right");
                    dancerAnim.SetTrigger("TwistRight");
                }

                attackTimer = attackCD;
                isAttack = true;
                Debug.Log(info.point);
                return;
            }
            else if (Input.GetMouseButtonDown(0) && pointToHead)
            {
                //attack head
                Debug.Log("attack head");

                attackTimer = attackCD;
                isAttack = true;
            }
        }
    }

    private void Timer()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        else if (isAttack == true && attackTimer <= 0) 
            isAttack = false;
    }
}
