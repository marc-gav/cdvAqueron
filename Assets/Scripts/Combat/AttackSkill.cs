using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSkill : Skill {

	private PlayerAnimations pAnim;
	private Rigidbody2D rb;
	private PlayerMovement pMove;

	protected override void OnAttemptCast() {
		pMove = GetComponent<PlayerMovement>();
		pAnim = GetComponent<PlayerAnimations>();
		rb = GetComponent<Rigidbody2D>();
	}

	protected override bool Run() 
	{
		rb.velocity = Vector2.zero;
		pAnim.StartAttackingAnimation();
		EnterCooldown();
		return true;
	}
}
