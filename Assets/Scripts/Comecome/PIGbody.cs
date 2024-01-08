using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIGbody : MonoBehaviour
{
    public GameObject attackArea;

    public void AttackAreaShow()
    {
        attackArea.SetActive(true);
    }

    public void AttackAreaHide()
    {
        attackArea.SetActive(false);
        transform.parent.GetComponent<PIG>().Attack1Finish();
    }
    public void Attack1Audio()
    {
        transform.parent.GetComponent<PIG>().Attack1Audio();
    }
    public void Attack2Dush()
    {
        transform.parent.GetComponent<PIG>().Attack2();
    }

    public void Attack1Finish()
    {
        transform.parent.GetComponent<PIG>().Attack1Finish();
    }
}
