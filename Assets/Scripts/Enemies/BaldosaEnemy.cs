using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldosaEnemy : Enemy {

	public LayerMask layerGround;
	public float jumpCooldown;
	private bool grounded = false;
	private bool playerDetected = false;
	private float timeToNextJump = 0f;
    private bool playerSteppingOver = false;
    public float tiempoRepActiv;

    protected override void AnimationBehaviour()
    {
        //Tiempo salto
        enemyAnimator.SetFloat("tiempoSalto", enemyAnimator.GetFloat("tiempoSalto") - Time.deltaTime);

        //Distancia suelo
        enemyAnimator.SetFloat("distanciaSuelo", DistanciaSuelo());

        //Jugador detectado
        if (Physics2D.OverlapCircle(enemyCenter.transform.position, detectRadius, playerMask))
        {
            enemyAnimator.SetBool("playerClose", true);
            //tiempo hasta reposo activa decrementado
            float temp = enemyAnimator.GetFloat("segundosHastaReposoActiva");
            enemyAnimator.SetFloat("segundosHastaReposoActiva", temp - Time.deltaTime);
        }
        else
        {
            enemyAnimator.SetBool("playerClose", false);
            //tiempo hasta reposo activa reseteado
            enemyAnimator.SetFloat("segundosHastaReposoActiva", tiempoRepActiv);
        }

        //health
        enemyAnimator.SetFloat("health", enemyStats.Health);
    }
    
    protected override void Behaviour()
    {
    }

    public float DistanciaSuelo()
    {
        return Physics2D.Raycast(enemyCenter.transform.position, Vector2.down, 100f, layerGround).distance;
    }

    public void Salto()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 vectorPlayer = player.transform.position - transform.position;
        vectorPlayer = new Vector3(vectorPlayer.x, 0, 0).normalized;
        vectorPlayer += new Vector3(vectorPlayer.x * speed/2, speed, 0);
        rb.AddForce(vectorPlayer);
        enemyAnimator.SetFloat("tiempoSalto", jumpCooldown);
    }

    public void SaltoActivacion()
    {
        rb.velocity = Vector2.up * 10f;
    }
}
