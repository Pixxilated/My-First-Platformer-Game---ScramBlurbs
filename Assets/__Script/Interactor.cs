using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Interactor : MonoBehaviour
{

    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactEvent;


    private void Update()
    {
        if(isInRange)   // If we are in range to interact
        {
            if (Input.GetKeyDown(interactKey)) // and if player Presses key 
            {
                interactEvent.Invoke(); // Fire Event
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            print("Player");
        }
    }
}
