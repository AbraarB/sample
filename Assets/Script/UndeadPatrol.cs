using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadPatrol : MonoBehaviour
{
    [SerializeField] private float ms;
    private Rigidbody2D rb;
    public bool balik;
    private SpriteRenderer sprite;
    [SerializeField]
    private int health;
    private enum MovementState {undead_walk, undead_dead}
    private Animator anim;
    private float delay = 0.9f;
    private bool hidup = true;


    MovementState state;
    void Start()
    {
        balik = true;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hidup)
        {
            state = MovementState.undead_walk;
            if (balik)
            {
                rb.velocity = new Vector2(ms, rb.velocity.y);
                sprite.flipX = true;

            }
            else
            {
                rb.velocity = new Vector2(-ms, rb.velocity.y);
                sprite.flipX = false;
            };
        }
        else
        {
            state = MovementState.undead_dead;
        }
        anim.SetInteger("state", (int)state);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Berbalik"))
        {
            balik = !balik;
        }

        if (collision.CompareTag("projectile"))
        {
            Destroy(collision.gameObject);
            health -= 3;
            if (health <= 0)
            {
                hidup = false;
                ms = 0;
                Invoke("undeadDestroy", delay);
            }
        }
    }

    void undeadDestroy()
    {
        Destroy(this.gameObject);
    }
}
