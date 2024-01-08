using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartBossFight : MonoBehaviour
{
    public PIG pig;
    public GameObject cam2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pig.player = other.transform;
            cam2.SetActive(true);
            Destroy(gameObject);
        }
    }
}
