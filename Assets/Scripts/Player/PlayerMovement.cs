using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//CODIGO PARA MOVERSE Y SALTAR -> ACCIONES BASICAS DEL PERSONAJE
public class PlayerMovement : MonoBehaviour {

    public float jumpLockTime = 0.2f;
    //Generales
    private float horizontalMovement;
    public int maxJumps;
    public float jumpModifier = 1.5f;
    private int availableJumps;
    private Rigidbody2D rb;
    private PlayerAnimations pAnim;
    private PlayerWorldInfo pWorldInfo;
    private Stats statistics;

    private void Awake ()
    {
        pWorldInfo = GetComponent<PlayerWorldInfo>();
        pAnim = GetComponent<PlayerAnimations> ();
        rb = GetComponent<Rigidbody2D> ();
        statistics = GetComponent<Stats>();
        availableJumps = maxJumps;
    }

    private void Update ()
    {
        BetterJumpGravity ();
        //Receive information from feet
        if (pWorldInfo.IsGrounded ())
        {
            availableJumps = maxJumps;
        }

        if(horizontalMovement == 0)
        {
            pAnim.StopRunningAnimation();
        }
    }

    public void Run()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        pAnim.StartRunningAnimation();
        if (horizontalMovement > 0.1f) pAnim.flipRight();
        if (horizontalMovement < -0.1f) pAnim.flipLeft();
        rb.velocity = new Vector2(statistics.speed.Value * horizontalMovement, rb.velocity.y);
    }

    public void Jump ()
    {
        if (availableJumps > 0) {
            availableJumps--;
            rb.velocity = new Vector2(rb.velocity.x, statistics.speed.Value * jumpModifier);
            pAnim.StartJumpingAnimation();
        }
    }

    private void BetterJumpGravity () {
        if (Input.GetButtonUp ("Jump") || rb.velocity.y < -1f) rb.gravityScale = 4.5f;
        else if (Input.GetButton ("Jump")) rb.gravityScale = 0.8f;
        else if (pWorldInfo.IsGrounded ()) rb.gravityScale = 3;
    }

    public void ResetVelocity()
    {
        rb.velocity = Vector2.zero;
    }
}