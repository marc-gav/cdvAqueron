using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDamage : MonoBehaviour {

    private float nextHit = 0;
    public float damagePerSecond;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player" && Time.time > nextHit)
        {
            collision.gameObject.SendMessage("TakeDamage", damagePerSecond / 10, SendMessageOptions.DontRequireReceiver);
            nextHit = Time.time + 0.1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
