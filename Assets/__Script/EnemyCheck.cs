using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCheck : MonoBehaviour
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
        // Check if the object that collided with player contains an Enemy Script
        if (col.GetComponent<Enemy>())
        {
            rb.bodyType = RigidbodyType2D.Static;   // Makes the player unable to move after death
            animator.SetTrigger("death");

            StartCoroutine ("RestartGame");
        }
    }

    public IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
