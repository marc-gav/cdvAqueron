using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {

    public float damage;
    public bool hasStats = true; 

    void Start()
    {
        if(hasStats)
        {
            Stats stadist = GetComponentInParent<Stats>();
            if (stadist == null) stadist = GetComponent<Stats>();
            damage = stadist.attack.Value;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(gameObject.name == "Guadaña" || collision.name == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
        
    }
}
