using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

	public DoorScript otherDoor;
	private Animator doorAnimation;
    protected Transform teleportPosition;

    void Start()
    {
        teleportPosition = gameObject.GetComponentInChildren<Transform>();
    }

    
	void OnTriggerStay2D(Collider2D col)
	{
        try
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                GameObject player = GameObject.Find("Player");
                TeleportPlayer(player); //Temporal deberia llamarse al final de la animacion de abrir la puerta
                doorAnimation.SetBool("open", true);
            }
        }
        catch (System.Exception e) { } //col es null pero me da bastante igual
		
	}

	//Called at the end of the teleport animation
	public void TeleportPlayer(GameObject player) {
         player.transform.position = otherDoor.teleportPosition.position;
	}
}
