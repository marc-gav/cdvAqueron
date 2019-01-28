using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldosaHead : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si es un jugador quien ha colisionado
        if (collision.gameObject.name == "Player")
        {
            //Vector velocity del jugador en el momento de la colision 
            Vector3 velocity = collision.attachedRigidbody.velocity;
            
            float angulo = Vector3.Angle(velocity, Vector3.right);

            //Si el angulo es el de un salto hacia abajo
            if (angulo > 80 || angulo < 100)
            {
                Debug.Log("Instakill");
                //La balodsa sufre un instakill
                GetComponentInParent<Stats>().TakeDamage(5000);
            }
        }
    }
}
