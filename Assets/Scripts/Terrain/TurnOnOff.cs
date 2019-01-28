using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnOff : MonoBehaviour {
    public float radiusDistance;
    public LayerMask layerMask;
    public Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        Collider2D playerDetected = Physics2D.OverlapCircle(transform.position, radiusDistance, layerMask);
        animator.SetBool("playerCerca", playerDetected != null);
        
    }
}
