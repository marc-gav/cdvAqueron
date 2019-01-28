using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldInfo : MonoBehaviour {

    public Transform leftHit, rightHit;
    public Transform feetRayCenter;
    public LayerMask ignorePlayer;
    private bool ground = false;
    void OnTriggerStay2D(Collider2D other)
    {
        ground = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ground = false;
    }

    public bool IsGrounded()
    {
        return ground;
    }

    public float DistanceToGround()
    {
        RaycastHit2D coll = Physics2D.Raycast(feetRayCenter.position, Vector2.down, 200f, ignorePlayer);
        return coll.distance;
    }

    private bool DraggingRight()
    {
        if (Input.GetAxis("Horizontal") < 0f)
            return Physics2D.OverlapCircle(rightHit.position, 0.5f, ignorePlayer);
        else return false;
    }

    private bool DraggingLeft()
    {
        if (Input.GetAxis("Horizontal") > 0f)
            return Physics2D.OverlapCircle(leftHit.position, 0.5f, ignorePlayer);
        else return false;
    }
}
