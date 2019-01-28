using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeSceneScript : MonoBehaviour {

    public Button initialButton;
	void Start ()
    {
        initialButton.Select();
    }
}
