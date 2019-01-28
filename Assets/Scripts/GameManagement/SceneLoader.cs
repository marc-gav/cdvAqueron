using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	public void LoadMainScene() {
        GameObject gameManager = GameObject.Find("GameManager");
        if(gameManager != null) gameManager.GetComponent<GameManager>().LoadScene("EscenaEvento", LoadSceneMode.Single);
    }
}
