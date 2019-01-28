using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbejaReinaScript : MonoBehaviour {

    public GameObject abejaReina, abejas, capulloAbeja;
    [Range(0, 0.1f)]
    public float lerpSpeed;
    public Transform poisicion1, poisicion2, poisicion3;
    private Transform[] positions = new Transform[3]; 
    private Vector3 lastPosition;
    private Animator anim;
    private Stats stats;

    private void Start()
    {
        stats = GetComponentInChildren<Stats>();
        anim = GetComponent<Animator>();
        positions[0] = poisicion1;
        positions[1] = poisicion2;
        positions[2] = poisicion3;

        lastPosition = poisicion1.position;
    }

    private void Update()
    {
        anim.SetFloat("health", stats.Health);
    }

    public void FlyFromPoint2Point()
    {
        Transform nextPos =  null;
        //Seleccionamos una posicion distinta
        //a la ultima
        do
        {
            nextPos = positions[Random.Range(0, 3)];
        } while (nextPos.position == lastPosition);
        

        //Calculamos el vector que une los dos puntos
        Vector3 destination = nextPos.position - lastPosition;

        //Ponemos el sprite mirando a su objetivo
        //La posicion natural es mirar hacia (-1,0,0)
        //Si destination tiene componente x positiva
        //Rotamos el sprite
        abejaReina.transform.localScale = (destination.x > 0) ? new Vector3(-1,1,1) : Vector3.one;

        StartCoroutine("Move", nextPos.position);
    }

    private IEnumerator Move(Vector3 nextPosition)
    {
        //Colocamos inicialmente en la posicion correcta
        abejaReina.transform.position = lastPosition;

        //Mientras no llegue a su objetivo
        while(Vector3.Distance(abejaReina.transform.position, nextPosition) > 0.4f)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Muerte") && anim.IsInTransition(0))
            {
                break;
            }
            //Movemos la posicion hacia el objetivo
            abejaReina.transform.position = Vector3.Lerp(abejaReina.transform.position, nextPosition, lerpSpeed);
            yield return new WaitForEndOfFrame();
        }

        anim.SetBool("goTo1", true);
        anim.SetBool("goTo2", false);

        //Se actualiza la last position
        lastPosition = nextPosition;
    }

    public void Kill()
    {
        Destroy(gameObject, 5f);
    }

    public void AddFallDown()
    {
        abejaReina.AddComponent<Rigidbody2D>();
    }

}
