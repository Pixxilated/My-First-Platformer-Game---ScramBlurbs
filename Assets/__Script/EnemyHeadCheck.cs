using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerrb;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>())
        {
            playerrb.velocity = new Vector2(playerrb.velocity.x, 0f);
            playerrb.AddForce(Vector2.up * 300f);
        }
    }
}
