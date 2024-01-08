using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallTheDog2 : MonoBehaviour
{
    public Transform dog;

    void Update()
    {
        Comecome();
    }

    private void Comecome()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            dog.GetComponent<Dog2>().Comecome();
        }
    }
}
