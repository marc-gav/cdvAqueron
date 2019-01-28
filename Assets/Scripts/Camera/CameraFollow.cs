using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

    //Valor para determinar la distancia que va a llegar a separarse la camara del personaje
    //Valor recomendado 0.95
    [Range(0f,1f)]
    public float lerpValue = 0.95f;

    private Vector3 offset;

    void Start() {
        offset = transform.position - player.transform.position;
    }

    private void FixedUpdate() {
        LerpingMovement();
    }

    void RawMovement() {
        transform.position = player.transform.position + offset;
    }

    void LerpingMovement() {
        //Se obtiene la distancia entre la posicion del personaje y la camara 
        //y se hace una transicion. El lerp es un valor entre [0,1] donde 0 es la pos de la
        //camara y 1 es el personaje. Si la distancia es grande el valor de lerp estara mas 
        //cerca de 0. Si es pequeño mas cerca de 1.
        Vector3 lerpVector = transform.position - (player.transform.position + offset);
        // Sumo 0.01 para evitar 1/0
        float lerp = 1 - (1f /( (lerpVector.magnitude + 0.01f) + lerpValue));
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, lerp);
    }
}

