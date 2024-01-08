using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDown : MonoBehaviour
{
    public GameObject normalObj;
    public GameObject piecesObj;
    public float destroyTime = 3;
    public AudioClip broken;

    public void BreakToPieces()
    {
        AudioSource.PlayClipAtPoint(broken, transform.position);
        normalObj.SetActive(false);
        piecesObj.SetActive(true);
        Destroy(gameObject, destroyTime);
    }
}
