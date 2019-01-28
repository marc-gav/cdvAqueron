using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy {

	public LayerMask layerGround;
	private Vector3 currentDirection;
	private float nextChange = 0f;

    protected override void AnimationBehaviour()
    {
        
    }

    protected override void Behaviour() {
		Collider2D playerDetected = Physics2D.OverlapCircle(enemyCenter.transform.position, detectRadius, playerMask);
        //Player detected
        if (playerDetected != null)
        {
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
            if (!Physics2D.Raycast(transform.position, Vector2.down, 1f, layerGround))
            {
                velocityTemp.y = 2 * speed * direction.y;
            }
            rb.velocity = velocityTemp;

        }

        //Not detected
        else
        {
            enemyAnimator.SetBool("closeToPlayer", false);
            RaycastHit2D downhit = Physics2D.Raycast(transform.position, Vector2.down, 400, layerGround);

            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 3f, layerGround);
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 3f, layerGround);
         
            //Up down code
            //
            float distance = Vector2.Distance(transform.position, downhit.point);
            if (distance < 1) transform.position += Vector3.up * Time.deltaTime;
            else transform.position += new Vector3(0, Mathf.Sin(Time.time)) * Time.deltaTime;
            //

            
            //Left right code
            //
            if (rightHit)
            {
                transform.position += Vector3.left * Time.deltaTime;
                currentDirection = Vector3.left;
            }
            else if (leftHit)
            {
                transform.position += Vector3.right * Time.deltaTime;
                currentDirection = Vector3.right;
            }
            else
            {
                if (Time.time - nextChange > 0)
                {
                    
                    do {
                        currentDirection = ((int)Random.Range(-1.1f, 1.1f)) * Vector3.right;
                    } while (currentDirection == Vector3.zero);
                    nextChange = Time.time + 3;
                }
                transform.position += currentDirection * Time.deltaTime;
            }
            //

            Vector3 velocityTemp = rb.velocity;
            velocityTemp.y = Mathf.Clamp(rb.velocity.y, -speed, speed);
        }
	}
}
