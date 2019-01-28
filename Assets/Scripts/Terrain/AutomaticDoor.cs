using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour {

    public AutomaticDoor otherDoor;
    protected bool detectedPlayer;
    protected Animator animator;
    public LayerMask playerFilter;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        detectedPlayer = Physics2D.OverlapCircle(transform.position, 5f, playerFilter);
    }

    private void LateUpdate()
    {
        if(otherDoor != null) { detectedPlayer = detectedPlayer || otherDoor.detectedPlayer; }
        
        animator.SetBool("playerDetected", detectedPlayer);
    }
}
