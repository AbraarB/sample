using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newUndeadMovePattern : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D undeadRigidbody;

    void Start()
    {
        undeadRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        undeadRigidbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipUndeadFacing();
    }

    void FlipUndeadFacing()
    {
        transform.localScale = new Vector2 (Mathf.Sign(undeadRigidbody.velocity.x), 1f);
    }
}
