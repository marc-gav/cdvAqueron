using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AccionesUI : MonoBehaviour {

    private GameManager gameManager;

    private void Start()
    {
        GameObject gManager = GameObject.Find("GameManager");
        gameManager = gManager.GetComponent<GameManager>();
    }

    public void SalirdelJuego()
    {
        Application.Quit();
    }

    public void ReiniciarJuego()
    {
        gameManager.ExitSlowTime();
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void Continuar()
    {
        gameManager.ExitSlowTime();
        SceneManager.UnloadSceneAsync("EscenaPausa");
    }
}
