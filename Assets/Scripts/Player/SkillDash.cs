using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDash : Skill {
	// Run tryies to enable active effect
	// ActiveEffect makes the dash force
	// OnExitActive unlocks the PlayerMovement and resets velocity
	// OnFinishCast locks PlayerMovement and starts cooldown
    protected override bool Run()
    {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		PlayerAnimations pAnim = GetComponent<PlayerAnimations>();
		if(rb != null && pAnim != null) {
			EnterActive();
            Debug.Log("Dashed");
			return true;
		}
		return false;
	}

	protected override void ActiveEffect() {
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		PlayerAnimations pAnim = GetComponent<PlayerAnimations>();
		float orientation = pAnim.LookingRight() ? 1 : -1;
		rb.velocity = new Vector2(orientation * range, 0);
	}

	protected override void OnExitActive(float time) {
		PlayerMovement pMovem = GetComponent<PlayerMovement>();
		PlayerAnimations pAnim = GetComponent<PlayerAnimations>();
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		if(pMovem != null && rb != null && pAnim != null) {
			pAnim.StopDashingBool();
			rb.velocity = Vector2.zero;
		}
	}
	protected override void OnFinishCast(bool success) {
		PlayerMovement pMovem = GetComponent<PlayerMovement>();
		PlayerAnimations pAnim = GetComponent<PlayerAnimations>();
		if(pMovem != null && success && pAnim != null) {
			EnterCooldown();
			pAnim.StartDashingBool();
		}
    }
 }
