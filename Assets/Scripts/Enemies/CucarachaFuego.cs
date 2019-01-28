using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucarachaFuego : Enemy {

	private float nextAttack = 0f;

    protected override void AnimationBehaviour() { }
    protected override void Behaviour() {
		if(Time.time - nextAttack > 0)
            {
                enemyAnimator.SetBool("attack", true);
                nextAttack = Time.time + Random.Range(3, 5);
        }

        else enemyAnimator.SetBool("attack", false);
		Collider2D playerDetected = Physics2D.OverlapCircle(enemyCenter.transform.position, detectRadius, playerMask);
        
        if(playerDetected != null) {
            float playerDirection = (playerDetected.transform.position - transform.position).x;
            if(Mathf.Abs(playerDirection) > 1)
            {
                if (transform.rotation.y != 0 && playerDirection < 0
                || transform.rotation.y == 0 && playerDirection > 0)
                    transform.RotateAround(enemyCenter.transform.position, new Vector3(0, 1, 0), 180);
            }
            

            enemyAnimator.SetBool("closeToPlayer", true);
            Vector3 direction = playerDetected.transform.position - transform.position;
            
            Vector3 velocityTemp = rb.velocity;
            velocityTemp.x = speed * direction.x;
            Mathf.Clamp(velocityTemp.x, -speed, speed);
            rb.velocity = velocityTemp;
        }
		else enemyAnimator.SetBool("closeToPlayer", false);
    }
}
