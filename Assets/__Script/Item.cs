using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject player;
    public Transform grabPoint;

    public float grabbedObjectYValue;
    public bool isGrabbing;

    public GameObject itemPrefab;

    public void GrabDrop()
    {
        if (isGrabbing)
        {
            print("try drop");
            // make isGrabbing false
            isGrabbing = false;
            transform.parent = null;
            transform.position = new Vector3(transform.position.x, grabbedObjectYValue, transform.position.z);
        }
        else
        {
            // enable isGrabbing bool
            isGrabbing = true;
            // parent the object to the player
            transform.parent = player.transform;
            // store the y value
            grabbedObjectYValue = transform.position.y;
            // adjust the position of the grabbed object
            transform.localPosition = grabPoint.localPosition;
        }
    }
}
