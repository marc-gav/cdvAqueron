using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stats))]
public abstract class Enemy : MonoBehaviour {

	public GameObject enemyCenter;
	public LayerMask playerMask;
	public float detectRadius;
	public float speed;
	public Stats enemyStats;
	public bool flyingEnemy;
	protected Rigidbody2D rb;
	public Animator enemyAnimator;
    protected virtual void AnimationBehaviour() { }
    protected abstract void Behaviour();
    protected virtual void StartConfiguration() { }

    void Start()
	{     
		enemyAnimator = GetComponent<Animator>();
		enemyStats = GetComponent<Stats>();  
		rb = GetComponent<Rigidbody2D>();
		
		if(flyingEnemy && GetComponent<Rigidbody2D>() != null) {
			GetComponent<Rigidbody2D>().gravityScale = 0;
		}

        StartConfiguration();
    }
	void Update()
	{
		enemyAnimator.SetFloat("health", enemyStats.Health);

		if (CheckDeath()) {
            if(GetComponent<Rigidbody2D>() != null) GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			Destroy(gameObject, 0.6f);
		}

        //Do things
        Behaviour();
        //Update animation variables after doing things
        AnimationBehaviour();
    }
	
	private bool CheckDeath() {
		return GetComponent<Stats>().Health <= 0;
	}
}
