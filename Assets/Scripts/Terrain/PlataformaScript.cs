using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaScript : MonoBehaviour {

    private EdgeCollider2D edgeCol;
    private PlatformEffector2D platEff;
    private void Awake() {
        platEff = GetComponent<PlatformEffector2D>();
        edgeCol = GetComponent<EdgeCollider2D>();
    }
    private void OnCollisionStay2D(Collision2D collision) {
        if (Input.GetAxis("Vertical") < 0) {
            platEff.surfaceArc = 0;
            Invoke("resetPlatform", 0.5f);
        }
    }

    private void resetPlatform() {
            platEff.surfaceArc = 180;
    }
}
