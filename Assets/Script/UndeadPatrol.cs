using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndeadPatrol : MonoBehaviour
{
    [SerializeField] private float ms;
    private Rigidbody2D rb;
    public bool balik;
    private SpriteRenderer sprite;

    void Start()
    {
        balik = true;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (balik)
        {
            rb.velocity = new Vector2(ms, rb.velocity.y);
            sprite.flipX = true;

        }
        else
        {
            rb.velocity = new Vector2(-ms, rb.velocity.y);
            sprite.flipX = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Berbalik"))
        {
            balik = !balik;
        }
    }
}
