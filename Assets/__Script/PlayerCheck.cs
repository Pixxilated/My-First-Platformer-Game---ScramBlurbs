using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<EnemyHeadCheck>())
        {
            rb.bodyType = RigidbodyType2D.Static;   // Makes the enemy unable to move after death

            animator.SetTrigger("death");
            Destroy(transform.parent.gameObject, 0.4f);
        }
    }
}
