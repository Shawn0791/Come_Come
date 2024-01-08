using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTheDog : MonoBehaviour
{
    public Transform[] dog;
    public AudioClip comecome;

    void Update()
    {
        Comecome();
    }

    private void Comecome()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton1)) 
        {
            AudioSource.PlayClipAtPoint(comecome, transform.position);
            for (int i = 0; i < dog.Length; i++)
            {
                dog[i].GetComponent<Dog>().Comecome();
            }
        }
    }
}
