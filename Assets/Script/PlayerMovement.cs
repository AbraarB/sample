using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerWalkspeed = 5f;
    [SerializeField] float jumpHeight = 3f;
    //[SerializeField] float dashDistance = 10f;
    private float delay = 2f;
    private Animator anim;
    private bool isClicked;

    private enum MovementState {nc_idle, nc_walk, ncgmtrans, gm_idle, gm_walk, gm_jump}

    Vector2 moveInput;
    Rigidbody2D playerRigidbody;
    CapsuleCollider2D playerBodycollider;
    BoxCollider2D playerFeetcollider;

    //float dashCooldown;
    //bool isDashing;
    MovementState state;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerBodycollider = GetComponent<CapsuleCollider2D>();
        playerFeetcollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Walk();
        FlipSprite();
        Trans_nc_gm();

    }

    // JALAN
    //input key jalan (A, D)
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    //kecepatan jalan
    void Walk()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * playerWalkspeed, playerRigidbody.velocity.y);
        playerRigidbody.velocity = playerVelocity;
    }

    //player hadap kanan/kiri
    void FlipSprite()
    {
        
        bool isPlayerMoving = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        if (isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);

            if (isClicked)
            {
                state = MovementState.gm_walk;
            }
            else
            {
                state = MovementState.nc_walk;
            }

        }
        else if (!isPlayerMoving)
        {
            if (isClicked)
            {
                Invoke("gm_idle", delay);
            }
            else
            {
                state = MovementState.nc_idle;
            }
            
        }

        anim.SetInteger("state", (int) state);
    }

    // LONCAT
    //input key loncat (space)
    void OnJump(InputValue value)
    {
        if (!playerFeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            playerRigidbody.velocity += new Vector2(0f, jumpHeight);
            Debug.Log("Jumped!");
        }
    }

    // DASH
    //input key dash (shift)
    /*void OnDash(InputValue value)
    {
        //dash kiri
        
        
        //dash kanan
        
        
    }*/

    void Trans_nc_gm()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            
            state = MovementState.ncgmtrans;
            isClicked = true;
        }

        anim.SetInteger("state", (int)state);
    }

    void gm_idle()
    {
        state = MovementState.gm_idle;
    }



}
