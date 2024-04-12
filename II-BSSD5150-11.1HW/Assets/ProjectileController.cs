using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;

    private Rigidbody2D rb2d;

    private int wallCollisions = 0; // Counter for wall collisions

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed; // Start moving in the projectile's up direction
    }

    private void Update()
    {
        // Ensure the top of the projectile faces the direction of motion
        if (rb2d.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb2d.velocity.y, rb2d.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reflect the projectile's direction upon colliding with a wall
            Vector2 reflection = Vector2.Reflect(rb2d.velocity.normalized, collision.contacts[0].normal);
            rb2d.velocity = reflection * speed;

            wallCollisions++; // Increment wall collision counter
            FindObjectOfType<ScoreManager>().IncrementScore();

            // Destroy the projectile if it collides with walls 3 times
            if (wallCollisions >= 3)
            {
                
                Destroy(gameObject);
            }

        }
        else if (collision.gameObject.CompareTag("Tree"))
        {
            // Decrement the score when the projectile hits a tree
            FindObjectOfType<ScoreManager>().DecrementScore();
        }
    }
}
