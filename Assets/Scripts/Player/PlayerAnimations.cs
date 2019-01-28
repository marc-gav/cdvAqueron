using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    public GameObject leftHit;
    public GameObject rightHit;
    public LayerMask layerGround;
    private PlayerMovement pMov;
    private SpriteRenderer sRend;
    private Animator pAnimator;
    private bool lookingRight = true;
    private Stats stats;
    public GameObject rotationCenter;
    private Rigidbody2D rb;
    private float lastAttack;
    public float attackComboEnd;

    private void Awake() {
        //Asignacion de variables
        rb = GetComponent<Rigidbody2D>();
        pAnimator = GetComponent<Animator>();
        sRend = GetComponent<SpriteRenderer>();
        stats = GetComponent<Stats>();
        pMov = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        pAnimator.SetFloat("vida", stats.Health);
        SetHorizontalSpeed();
        SetVerticalHorizontalSpeed();
        DistanceToGround();
        timeToIdle2();
        ataquesNormalesContador();
    }

    //El personaje esta mirando a la derecha o no
    public bool LookingRight() {
        return lookingRight;
    }

    public void RotatePlayer() {
        transform.RotateAround(rotationCenter.transform.position, new Vector3(0,1,0), 180);
        //The positions rotate 180 degrees too
        Vector2 auxPos = rightHit.transform.position;
        rightHit.transform.position = leftHit.transform.position;
        leftHit.transform.position = auxPos;

        lookingRight = !lookingRight;
    }

    //Rotar el personaje a la derecha 
    public void flipRight() {
        if(!lookingRight) {
            //Girando su transform 180 grados
            transform.RotateAround(rotationCenter.transform.position, new Vector3(0,1,0), 180);
            //The positions rotate 180 degrees too
            Vector2 auxPos = rightHit.transform.position;
            rightHit.transform.position = leftHit.transform.position;
            leftHit.transform.position = auxPos;
            lookingRight = true;
        }
    }

    //Rotar el personaje a la derecha 
    public void flipLeft() {
        if(lookingRight) {
            //Girando su transform 180 grados
            transform.RotateAround(rotationCenter.transform.position, new Vector3(0,1,0), 180);
            //The positions rotate 180 degrees too
            Vector2 auxPos = rightHit.transform.position;
            rightHit.transform.position = leftHit.transform.position;
            leftHit.transform.position = auxPos;
            lookingRight = false;
        }
        
    }

    //Vida en el update
    //Dash
    public void StartDashingBool()
    {
        pAnimator.SetBool("inputDash", true);
    }

    public void StopDashingBool()
    {
        pAnimator.SetBool("inputDash", false);
    }

    //Running animation
    public void StartRunningAnimation()
    {
        pAnimator.SetBool("inputCorrer", true);
    }

    public void StopRunningAnimation()
    {
        pAnimator.SetBool("inputCorrer", false);
    }
    //Input salto
    public void StartJumpingAnimation()
    {
        pAnimator.SetBool("inputSalto", true);
        Invoke("StopJumpingAnimation", 0.2f);
    }

    public void StopJumpingAnimation()
    {
        pAnimator.SetBool("inputSalto", false);
    }

    //Velocidad vertical
    private void SetVerticalHorizontalSpeed()
    {
        pAnimator.SetFloat("velocidadVertical", rb.velocity.y);
        pAnimator.SetFloat("velocidadHorizontal", Mathf.Abs(rb.velocity.x));
    }



    //Input ataque
    public void StartAttackingAnimation()
    {
        pAnimator.SetBool("inputAtaque", true);
        if (pAnimator.GetInteger("ataquesNormales") == 0)
        {
            lastAttack = Time.time;
        }
        pAnimator.SetInteger("ataquesNormales", pAnimator.GetInteger("ataquesNormales") + 1);
    }

    public void StopAttackingAnimation()
    {
        pAnimator.SetBool("inputAtaque", false);
    }

    //Velocidad horizontal
    private void SetHorizontalSpeed()
    {
        pAnimator.SetFloat("velocidadVertical", rb.velocity.x);
    }

    //Time to idle 2
    private void timeToIdle2()
    {
        AnimatorStateInfo currState = pAnimator.GetCurrentAnimatorStateInfo(0);

        if (currState.IsName("EvaIdle1"))
        {
            float currentTime = pAnimator.GetFloat("timeToIdle2");
            pAnimator.SetFloat("timeToIdle2", currentTime + Time.deltaTime);
        }
        else
        {
            pAnimator.SetFloat("timeToIdle2", 0);
        }
    }

    //Distance to ground
    private void DistanceToGround()
    {
        float distancia = Physics2D.Raycast(transform.position, Vector2.down, 200f, layerGround).distance;
        pAnimator.SetFloat("distanceToGround", distancia);
    }
    
    //AtaquesNormales
    private void ataquesNormalesContador()
    {
        if(Time.time > lastAttack + attackComboEnd)
        {
            pAnimator.SetInteger("ataquesNormales", 0);
        }
    }

    public void resetContadorAtaques()
    {
        pAnimator.SetInteger("ataquesNormales", 0);
    }


}
