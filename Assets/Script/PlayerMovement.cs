using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerWalkspeed = 5f;
    [SerializeField] float jumpHeight = 3f;
    //[SerializeField] float dashDistance = 10f;
    [SerializeField] float transCooldown = 0f;
    
    private float delay = 0.42f;
    private Animator anim;
    private bool isClicked;
    private float lastTransform;
    private int transformState = 1; //1 -> nc , 2 -> gr

    private enum MovementState {nc_idle, nc_walk, ncgmtrans, gm_idle, gm_walk, gmnctrans}

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

        /*if (Input.GetKeyDown(KeyCode.X) && transformState == 1 && Time.time - lastTransform < transCooldown)
        {
            lastTransform = Time.time;
            Trans_nc_gm();
        }
        else if (Input.GetKeyDown(KeyCode.X) && transformState == 2 && Time.time - lastTransform < transCooldown)
        {
            lastTransform = Time.time;
            Trans_gm_nc();
        }
        else
        {
            return;
        }*/


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
        else
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
    void OnDash(InputValue value)
    {
        //dash kiri
        Debug.Log("Dashed left!");

        //dash kanan
        Debug.Log("Dashed right!");

    }


    //berubah
    void Trans_nc_gm()
    {

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Berubah nc-gm!");
            state = MovementState.ncgmtrans;
            isClicked = true;
        }

        anim.SetInteger("state", (int)state);
        transformState = 2;
    }

    void gm_idle()
    {
        state = MovementState.gm_idle;
    }

/*    void Trans_gm_nc()
    {

        //if (Input.GetKeyDown(KeyCode.X))
        //{
            Debug.Log("Berubah gm-nc!");
            state = MovementState.gmnctrans;
            isClicked = false;
        //}

        anim.SetInteger("state", (int)state);
        transformState = 1;
    }*/



}
